using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MITD.Core;
using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.PeriodManagementApp;
using MITD.PMS.Presentation.PeriodManagementApp.Views;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public class CalculationResultListVM : PeriodMgtWorkSpaceViewModel, IEventHandler<UpdateCalculationResultListArgs>
    {
        #region Fields

        private readonly IPMSController appController;
        private readonly IPeriodServiceWrapper periodService;
        private readonly ICalculationServiceWrapper calculationService;
        private long calculationId;

        #endregion

        #region Properties

        private PagedSortableCollectionView<JobIndexPointSummaryDTOWithAction> employeeCalcTotalScores;
        public PagedSortableCollectionView<JobIndexPointSummaryDTOWithAction> EmployeeCalcTotalScores
        {
            get { return employeeCalcTotalScores; }
            set { this.SetField(p => p.EmployeeCalcTotalScores, ref employeeCalcTotalScores, value);}
        }

        private EmployeeCalculationResultDTO  employeeCalculationResult;
        public EmployeeCalculationResultDTO EmployeeCalculationResult
        {
            get { return employeeCalculationResult; }
            set { this.SetField(p => p.EmployeeCalculationResult, ref employeeCalculationResult, value); }
        }

        private JobIndexPointSummaryDTOWithAction selectedEmployeeCalculation;
        public JobIndexPointSummaryDTOWithAction SelectedEmployeeCalculation
        {
            get { return selectedEmployeeCalculation; }
            set { 
                this.SetField(p => p.SelectedEmployeeCalculation, ref selectedEmployeeCalculation, value);
                //if (value != null)
                //    loadEmployeeJobPoints();
            }
        }

        private ReadOnlyCollection<DataGridCommandViewModel> calculationResultCommands;
        public ReadOnlyCollection<DataGridCommandViewModel> CalculationResultCommands
        {
            get
            {
                if (calculationResultCommands == null)
                {
                    var cmds = createCommands();
                    calculationResultCommands = new ReadOnlyCollection<DataGridCommandViewModel>(cmds);
                }
                return calculationResultCommands;
            }
            private set
            {
                if (calculationResultCommands == value) return;
                calculationResultCommands = value;
                OnPropertyChanged("CalculationResultCommands");
            }

        }

        private ICommand rowDetailsVisibilityChangedCommand;
        public ICommand RowDetailsVisibilityChangedCommand
        {
            get
            {
                if (rowDetailsVisibilityChangedCommand == null)
                {
                    rowDetailsVisibilityChangedCommand = new DelegateCommand(
                        () =>
                        {
                            loadEmployeeJobPoints();
                        });
                }
                return rowDetailsVisibilityChangedCommand;
            }
        }
        #endregion

        #region Constructors

        public CalculationResultListVM()
        {
            PeriodMgtAppLocalizedResources=new PeriodMgtAppLocalizedResources();
            init();
            //EmployeeCalculationResults.Add(new EmployeeCalculationResultDTO { Id = 4, Name = "Test" });           
        }

        public CalculationResultListVM(IPMSController appController,
                            ICalculationServiceWrapper calculationService ,
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
            EmployeeCalculationResult = new EmployeeCalculationResultDTO();
            EmployeeCalcTotalScores = new PagedSortableCollectionView<JobIndexPointSummaryDTOWithAction> { PageSize = 20 };
            EmployeeCalcTotalScores.OnRefresh += (s, args) => refresh();
        }

        private List<DataGridCommandViewModel> createCommands()
        {
            return CommandHelper.GetControlCommands(this, appController, SelectedEmployeeCalculation.ActionCodes);
        }

        public void Load(long calculationIdParam)
        {
            DisplayName = "نمرات عملکرد کارکنان در دوره";
            calculationId = calculationIdParam;
            refresh();
        }


        private  void loadEmployeeJobPoints()
        {
            if (SelectedEmployeeCalculation.JobPositionValues != null)
                return;

            ShowBusyIndicator("در حال دریافت اطلاعات...");
            calculationService.GetEmployeeJobPositionsCalculationResult(  (res, exp) => appController.BeginInvokeOnDispatcher(() =>
                {
                    HideBusyIndicator();
                    if (exp == null)
                    {
                        SelectedEmployeeCalculation.JobPositionValues = res;
                    }
                    else
                        appController.HandleException(exp);
                })
                , appController.CurrentPriod.Id, calculationId, SelectedEmployeeCalculation.EmployeeNo);
        }

        private void refresh()
        {
            ShowBusyIndicator("در حال دریافت اطلاعات...");
            calculationService.GetAllCalculationResults(
                 (res, exp) => appController.BeginInvokeOnDispatcher(() =>
                {
                    HideBusyIndicator();
                    if (exp == null)
                    {
                      
                        EmployeeCalcTotalScores.SourceCollection = res.Result;
                        EmployeeCalcTotalScores.TotalItemCount = res.TotalCount;
                        EmployeeCalcTotalScores.PageIndex = Math.Max(0, res.CurrentPage - 1);
                    }
                    else appController.HandleException(exp);
                }), appController.CurrentPriod.Id, calculationId, EmployeeCalcTotalScores.PageSize, EmployeeCalcTotalScores.PageIndex + 1);
        }



        protected override void OnPropertyChanged(string propertyName)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == "SelectedEmployeeCalculation" && EmployeeCalcTotalScores.Count > 0)
            {
                CalculationResultCommands = new ReadOnlyCollection<DataGridCommandViewModel>(createCommands());
                if (View != null)
                    ((CalculationResultListView)View).CreateContextMenu(CalculationResultCommands);
            }
        }

        protected override void OnRequestClose()
        {
            base.OnRequestClose();
            appController.Close(this);
        }

        
        public void Handle(UpdateCalculationResultListArgs eventData)
        {
            refresh();
        }

        #endregion
    }

}
