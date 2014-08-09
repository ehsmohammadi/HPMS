using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MITD.Core;
using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.PeriodManagementApp;
using MITD.PMS.Presentation.PeriodManagementApp.Views;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public class ManagerClaimListVM : PeriodMgtWorkSpaceViewModel
        , IEventHandler<UpdateClaimListArgs>
    {
        #region Fields

        private readonly IPMSController appController;
        private readonly IClaimServiceWrapper claimService;

        #endregion

        #region Properties

        private PagedSortableCollectionView<ClaimDTOWithAction> claims;
        public PagedSortableCollectionView<ClaimDTOWithAction> Claims
        {
            get { return claims; }
            set { this.SetField(p => p.Claims, ref claims, value); }
        }

        
        private PeriodDTO period;
        public PeriodDTO Period
        {
            get { return period; }
            set { this.SetField(p => p.Period, ref period, value); }
        }


        private ClaimDTOWithAction selectedClaim;
        public ClaimDTOWithAction SelectedClaim
        {
            get { return selectedClaim; }
            set
            {
                this.SetField(p => p.SelectedClaim, ref selectedClaim, value);
                if (selectedClaim == null) return; 
                ClaimCommands = createCommands();
                if (View != null)
                    ((IManagerClaimListView)View).CreateContextMenu(new ReadOnlyCollection<DataGridCommandViewModel>(ClaimCommands));
            }
        }

        private List<DataGridCommandViewModel> claimCommands;
        public List<DataGridCommandViewModel> ClaimCommands
        {
            get { return claimCommands; }
            private set
            {
                this.SetField(p => p.ClaimCommands, ref claimCommands, value);
                if (ClaimCommands.Count > 0) SelectedCommand = ClaimCommands[0];
            }

        }

        private DataGridCommandViewModel selectedCommand;
        public DataGridCommandViewModel SelectedCommand
        {
            get { return selectedCommand; }
            set { this.SetField(p => p.SelectedCommand, ref selectedCommand, value); }
        }

        #endregion

        #region Constructors

        public ManagerClaimListVM()
        {
            PeriodMgtAppLocalizedResources=new PeriodMgtAppLocalizedResources();
            init();
            Claims.Add(new ClaimDTOWithAction());          
        }

        public ManagerClaimListVM(IPMSController appController,
                            IClaimServiceWrapper claimService, 
                            IPeriodMgtAppLocalizedResources periodMgtAppLocalizedResources)
        {
            
            this.appController = appController;
            this.claimService = claimService;
            PeriodMgtAppLocalizedResources = periodMgtAppLocalizedResources;
            init();
            

        }

        #endregion

        #region Methods

        void init()
        {
            DisplayName = PeriodMgtAppLocalizedResources.ClaimListViewTitle;
            claims = new PagedSortableCollectionView<ClaimDTOWithAction>();
            claims.OnRefresh += (s, args) => refresh();
            ClaimCommands = new List<DataGridCommandViewModel>
            {
                   CommandHelper.GetControlCommands(this, appController, new List<int>{ (int) ActionType.AddClaim}).FirstOrDefault()
            };
        }

        private List<DataGridCommandViewModel> createCommands()
        {
            return CommandHelper.GetControlCommands(this, appController, SelectedClaim.ActionCodes);
        }

        public void Load(PeriodDTO periodParam)
        {
            period = periodParam;
            refresh();
        }

        private void refresh()
        {
            ShowBusyIndicator("در حال دریافت اطلاعات...");
            claimService.GetAllClaim(
                (res, exp) => appController.BeginInvokeOnDispatcher(() =>
                {
                    HideBusyIndicator();
                    if (exp == null)
                    {
                        claims.SourceCollection = res.Result;
                        claims.TotalItemCount = res.TotalCount;
                        claims.PageIndex = Math.Max(0, res.CurrentPage - 1);
                    }
                    else appController.HandleException(exp);
                }), period.Id, claims.PageSize, claims.PageIndex + 1);
        }

        protected override void OnRequestClose()
        {
            base.OnRequestClose();
            appController.Close(this);
        }


        public void Handle(UpdateClaimListArgs eventData)
        {
           refresh();
        }

        #endregion

       
    }

}
