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
    public class CalculationExceptionListVM : PeriodMgtWorkSpaceViewModel
        , IEventHandler<UpdateCalculationExceptionListArgs>
    {
        #region Fields

        private readonly IPMSController appController;
        private readonly ICalculationServiceWrapper calculationService;
        private CalculationDTO calculation;

        #endregion

        #region Properties

        public CalculationDTO Calculation
        {
            get
            {
                return calculation;
            }
        }

        private PagedSortableCollectionView<CalculationExceptionBriefDTOWithAction> calculationExceptions;
        public PagedSortableCollectionView<CalculationExceptionBriefDTOWithAction> CalculationExceptions
        {
            get { return calculationExceptions; }
            set { this.SetField(p => p.CalculationExceptions, ref calculationExceptions, value); }
        }

        private CalculationExceptionBriefDTOWithAction selectedCalculationException;
        public CalculationExceptionBriefDTOWithAction SelectedCalculationException
        {
            get { return selectedCalculationException; }
            set
            {
                this.SetField(p => p.SelectedCalculationException, ref selectedCalculationException, value);
                if (SelectedCalculationException == null) return;
                CalculationExceptionCommands = createCommands();
                if (View != null)
                    ((ICalculationExceptionListView)View).CreateContextMenu(new ReadOnlyCollection<DataGridCommandViewModel>(CalculationExceptionCommands));
            }
        }

        private List<DataGridCommandViewModel> calculationExceptionCommands;
        public List<DataGridCommandViewModel> CalculationExceptionCommands
        {
            get { return calculationExceptionCommands; }
            private set
            {
                this.SetField(p => p.CalculationExceptionCommands, ref calculationExceptionCommands, value);
                if (CalculationExceptionCommands.Count > 0) SelectedCommand = CalculationExceptionCommands[0];
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

        public CalculationExceptionListVM()
        {
            PeriodMgtAppLocalizedResources=new PeriodMgtAppLocalizedResources();
            init();
            CalculationExceptions.Add(new CalculationExceptionBriefDTOWithAction { Id = 4, EmployeeFullName = "Test" });           
        }

        public CalculationExceptionListVM(IPMSController appController,
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
            DisplayName = PeriodMgtAppLocalizedResources.CalculationExceptionListViewTitle;
            calculationExceptions = new PagedSortableCollectionView<CalculationExceptionBriefDTOWithAction>();
            calculationExceptions.OnRefresh += (s, args) => refresh();
        }

        private List<DataGridCommandViewModel> createCommands()
        {
            return CommandHelper.GetControlCommands(this, appController, SelectedCalculationException.ActionCodes);
        }

        public void Load(CalculationDTO  calculationParam)
        {
            calculation = calculationParam;
            DisplayName = PeriodMgtAppLocalizedResources.CalculationExceptionListViewTitle;
            refresh();
        }

        private void refresh()
        {
            ShowBusyIndicator("در حال دریافت اطلاعات...");
            calculationService.GetAllCalculationException(
                (res, exp) => appController.BeginInvokeOnDispatcher(() =>
                {
                    HideBusyIndicator();
                    if (exp == null)
                    {
                        calculationExceptions.SourceCollection = res.Result;
                        calculationExceptions.TotalItemCount = res.TotalCount;
                        calculationExceptions.PageIndex = Math.Max(0, res.CurrentPage - 1);
                    }
                    else appController.HandleException(exp);
                }),Calculation.PeriodId,calculation.Id, calculationExceptions.PageSize, calculationExceptions.PageIndex + 1);
        }

        protected override void OnRequestClose()
        {
            base.OnRequestClose();
            appController.Close(this);
        }

        
        public void Handle(UpdateCalculationExceptionListArgs eventData)
        {
            refresh();
        }

        #endregion
    }

}
