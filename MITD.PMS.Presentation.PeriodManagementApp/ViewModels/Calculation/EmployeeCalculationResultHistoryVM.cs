using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MITD.Core;
using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.PeriodManagementApp;
using MITD.PMS.Presentation.PeriodManagementApp.Views;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public class EmployeeCalculationResultHistoryVM : PeriodMgtWorkSpaceViewModel
    {
        #region Fields

        private readonly IPMSController appController;
        private readonly ICalculationServiceWrapper calculationService;
        private long periodId;

        #endregion

        #region Properties

        private EmployeeFinalizeCalculationHistoryDTO calculationHistory;
        public EmployeeFinalizeCalculationHistoryDTO CalculationHistory
        {
            get { return calculationHistory; }
            set { this.SetField(p => p.CalculationHistory, ref calculationHistory, value); }
        }

        

        private ReadOnlyCollection<DataGridCommandViewModel> calculationCommands;


        public ReadOnlyCollection<DataGridCommandViewModel> CalculationCommands
        {
            get
            {
                if (calculationCommands == null)
                {
                    var cmds = createCommands();
                    calculationCommands = new ReadOnlyCollection<DataGridCommandViewModel>(cmds);
                }
                return calculationCommands;
            }
            private set
            {
                if (calculationCommands == value) return;
                calculationCommands = value;
                OnPropertyChanged("CalculationCommands");
            }

        }

        #endregion

        #region Constructors

        public EmployeeCalculationResultHistoryVM()
        {
            //PeriodMgtAppLocalizedResources=new PeriodMgtAppLocalizedResources();
            //init();
            //CalculationHistory.Add(new EmployeeFinalizeCalculationInPeriodDTOWithAction { Id = 4, Name = "Test" });           
        }

        public EmployeeCalculationResultHistoryVM(IPMSController appController,
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
            calculationHistory = new EmployeeFinalizeCalculationHistoryDTO();
            //calculationHistory.OnRefresh += (s, args) => refresh();
        }

        private List<DataGridCommandViewModel> createCommands()
        {
            //return CommandHelper.GetControlCommands(this, appController, SelectedCalculation.ActionCodes);
            return new List<DataGridCommandViewModel>();
        }

        public void Load(long periodParam)
        {
            periodId = periodParam;
            refresh();
        }

        private void refresh()
        {
            //ShowBusyIndicator("در حال دریافت اطلاعات...");
            //calculationService.GetAllCalculationExec(
            //    (res, exp) =>
            //    {
            //        HideBusyIndicator();
            //        if (exp == null)
            //        {
            //            calculationHistory.SourceCollection = res.Result;
            //            calculationHistory.TotalItemCount = res.TotalCount;
            //            calculationHistory.PageIndex = Math.Max(0, res.CurrentPage - 1);
            //        }
            //        else appController.HandleException(exp);
            //    },periodId, calculationHistory.PageSize, calculationHistory.PageIndex + 1);
        }

        protected override void OnPropertyChanged(string propertyName)
        {
            //base.OnPropertyChanged(propertyName);
            //if (propertyName == "SelectedCalculation" && CalculationHistory.Count > 0)
            //{
            //    CalculationCommands = new ReadOnlyCollection<DataGridCommandViewModel>(createCommands());
            //    if (View != null)
            //        ((CalculationListView)View).CreateContextMenu(CalculationCommands);
            //}
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
