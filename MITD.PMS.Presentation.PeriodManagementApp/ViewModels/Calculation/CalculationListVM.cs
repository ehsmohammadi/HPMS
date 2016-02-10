using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MITD.Core;
using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.PeriodManagementApp;
using MITD.PMS.Presentation.PeriodManagementApp.Views;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public class CalculationListVM : PeriodMgtWorkSpaceViewModel
        , IEventHandler<UpdateCalculationListArgs>
    {
        #region Fields

        private readonly IPMSController appController;
        private readonly ICalculationServiceWrapper calculationService;
        private PeriodDTO period;

        #endregion

        #region Properties

        public PeriodDTO Period
        {
            get
            {
                return period;
            }
        }

        private PagedSortableCollectionView<CalculationBriefDTOWithAction> calculations;
        public PagedSortableCollectionView<CalculationBriefDTOWithAction> Calculations
        {
            get { return calculations; }
            set { this.SetField(p => p.Calculations, ref calculations, value); }
        }

        private CalculationBriefDTOWithAction selectedCalculation;
        public CalculationBriefDTOWithAction SelectedCalculation
        {
            get { return selectedCalculation; }
            set
            {
                this.SetField(p => p.SelectedCalculation, ref selectedCalculation, value);
                if (SelectedCalculation == null) return;
                CalculationCommands = createCommands();
                if (View != null)
                    ((ICalculationListView)View).CreateContextMenu(new ReadOnlyCollection<DataGridCommandViewModel>(CalculationCommands));
            }
        }

        private List<DataGridCommandViewModel> calculationCommands;
        public List<DataGridCommandViewModel> CalculationCommands
        {
            get { return calculationCommands; }
            private set
            {
                this.SetField(p => p.CalculationCommands, ref calculationCommands, value);
                if (CalculationCommands.Count > 0) SelectedCommand = CalculationCommands[0];
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

        public CalculationListVM()
        {
            PeriodMgtAppLocalizedResources=new PeriodMgtAppLocalizedResources();
            init();
            Calculations.Add(new CalculationBriefDTOWithAction { Id = 4, Name = "Test" });           
        }

        public CalculationListVM(IPMSController appController,
                            ICalculationServiceWrapper calculationService, 
                            IPeriodMgtAppLocalizedResources periodMgtAppLocalizedResources)
        {
            
            this.appController = appController;
            this.calculationService = calculationService;
            PeriodMgtAppLocalizedResources = periodMgtAppLocalizedResources;
            init();
        }

        #endregion

        #region Methods

        void init()
        {
            DisplayName = PeriodMgtAppLocalizedResources.CalculationListViewTitle;
            calculations = new PagedSortableCollectionView<CalculationBriefDTOWithAction>{PageSize = 20};
            calculations.OnRefresh += (s, args) => refresh();
            CalculationCommands = new List<DataGridCommandViewModel>
            {
                   CommandHelper.GetControlCommands(this, appController, new List<int>{ (int) ActionType.AddCalculation}).FirstOrDefault()
            };
        }

        private List<DataGridCommandViewModel> createCommands()
        {
            return CommandHelper.GetControlCommands(this, appController, SelectedCalculation.ActionCodes);
        }

        public void Load(PeriodDTOWithAction periodParam)
        {
            period = periodParam;
            DisplayName = PeriodMgtAppLocalizedResources.CalculationListViewTitle+" "+period.Name;
            refresh();
        }

        private void refresh()
        {
            ShowBusyIndicator("در حال دریافت اطلاعات...");
            calculationService.GetAllCalculation(
                (res, exp) => appController.BeginInvokeOnDispatcher(() =>
                {
                    HideBusyIndicator();
                    if (exp == null)
                    {
                        calculations.SourceCollection = res.Result;
                        calculations.TotalItemCount = res.TotalCount;
                        calculations.PageIndex = Math.Max(0, res.CurrentPage - 1);
                    }
                    else appController.HandleException(exp);
                }),period.Id, calculations.PageSize, calculations.PageIndex + 1);
        }

        protected override void OnRequestClose()
        {
            base.OnRequestClose();
            appController.Close(this);
        }

        
        public void Handle(UpdateCalculationListArgs eventData)
        {
            refresh();
        }

        #endregion
    }

}
