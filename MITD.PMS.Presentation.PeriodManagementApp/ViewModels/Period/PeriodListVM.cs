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
    public class PeriodListVM : PeriodMgtWorkSpaceViewModel, IEventHandler<UpdatePeriodListArgs>
    {
        #region Fields

        private readonly IPMSController appController;
        private readonly IPeriodServiceWrapper periodService;

        #endregion

        #region Properties & Back field

        private PagedSortableCollectionView<PeriodDTOWithAction> periods;
        public PagedSortableCollectionView<PeriodDTOWithAction> Periods
        {
            get { return periods; }
            set { this.SetField(p => p.Periods, ref periods, value); }
        }

        private PeriodDTOWithAction selectedPeriod;
        public PeriodDTOWithAction SelectedPeriod
        {
            get { return selectedPeriod; }
            set
            {
                this.SetField(p => p.SelectedPeriod, ref selectedPeriod, value);
                if (selectedPeriod == null) return;
                PeriodCommands = createCommands();
                if (View != null)
                    ((IPeriodListView)View).CreateContextMenu(new ReadOnlyCollection<DataGridCommandViewModel>(PeriodCommands));
            }
        }

        private List<DataGridCommandViewModel> periodCommands;
        public List<DataGridCommandViewModel> PeriodCommands
        {
            get { return periodCommands; }
            private set
            {
                this.SetField(p => p.PeriodCommands, ref periodCommands, value);
                if (PeriodCommands.Count > 0) SelectedCommand = PeriodCommands[0];
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

        public PeriodListVM()
        {
            PeriodMgtAppLocalizedResources=new PeriodMgtAppLocalizedResources();
            init();
            Periods.Add(new PeriodDTOWithAction { Id = 4, Name = "Test" });           
        }

        public PeriodListVM(IPMSController appController,
                            IPeriodServiceWrapper periodService, 
                            IPeriodMgtAppLocalizedResources periodMgtAppLocalizedResources)
        {
            
            this.appController = appController;
            this.periodService = periodService;
            PeriodMgtAppLocalizedResources = periodMgtAppLocalizedResources;
            init();
            

        }

        #endregion

        #region Methods

        void init()
        {
            DisplayName = PeriodMgtAppLocalizedResources.PeriodListViewTitle;
            periods = new PagedSortableCollectionView<PeriodDTOWithAction>();
            periods.OnRefresh += (s, args) => refresh();
            PeriodCommands = new List<DataGridCommandViewModel>
            {
                   CommandHelper.GetControlCommands(this, appController, new List<int>{ (int) ActionType.AddPeriod}).FirstOrDefault()
            };
        }

        private List<DataGridCommandViewModel> createCommands()
        {
            return CommandHelper.GetControlCommands(this, appController, SelectedPeriod.ActionCodes);
        }

        public void Load()
        {
            refresh();
        }

        private void refresh()
        {
            ShowBusyIndicator("در حال دریافت اطلاعات...");
            periodService.GetAllPeriods(
                (res, exp) =>
                {
                    appController.BeginInvokeOnDispatcher(() =>
                        {
                            HideBusyIndicator();
                            if (exp == null)
                            {
                                periods.SourceCollection = res.Result;
                                periods.TotalItemCount = res.TotalCount;
                                periods.PageIndex = Math.Max(0, res.CurrentPage - 1);
                            }
                            else
                                appController.HandleException(exp);
                        });
                }, periods.PageSize, periods.PageIndex + 1);
        }

        protected override void OnRequestClose()
        {
            base.OnRequestClose();
            appController.Close(this);
        }

        
        public void Handle(UpdatePeriodListArgs eventData)
        {
            refresh();
        }

        #endregion
    }

}
