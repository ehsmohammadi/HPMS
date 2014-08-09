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
    public class CalculationResultVM : PeriodMgtWorkSpaceViewModel, IEventHandler<UpdateCalculationResultListArgs>
    {
        #region Fields

        private readonly IPMSController appController;
        private readonly ICalculationServiceWrapper calculationService;
        private readonly IPeriodServiceWrapper periodService;
        private string employeeNo;

        #endregion

        #region Properties

        private JobIndexPointSummaryDTOWithAction employeeCalcTotalScore;
        public JobIndexPointSummaryDTOWithAction EmployeeCalcTotalScore
        {
            get { return employeeCalcTotalScore; }
            set { this.SetField(p => p.EmployeeCalcTotalScore, ref employeeCalcTotalScore, value); }
        }

        private bool visibleRequestClaim;
        public bool VisibleRequestClaim
        {
            get { return visibleRequestClaim; }
            set { this.SetField(p => p.VisibleRequestClaim, ref visibleRequestClaim, value); }
        }

        private CalculationDTO calculation;
        public CalculationDTO Calculation
        {
            get { return calculation; }
            set { this.SetField(p => p.Calculation, ref calculation, value); }
        }

        private PeriodDescriptionDTO selectedPeriod;
        public PeriodDescriptionDTO SelectedPeriod
        {
            get { return selectedPeriod; }
            set
            {
                this.SetField(p => p.SelectedPeriod, ref selectedPeriod, value);
                getEmployeePoint();
                VisibleRequestClaim = SelectedPeriod.Id == appController.CurrentPriod.Id;
            }
        }


        private ObservableCollection<PeriodDescriptionDTO> periodsWithDeteministicCalculation;
        public ObservableCollection<PeriodDescriptionDTO> PeriodsWithDeteministicCalculation
        {
            get { return periodsWithDeteministicCalculation; }
            set { this.SetField(p => p.PeriodsWithDeteministicCalculation, ref periodsWithDeteministicCalculation, value); }
        }

        private CommandViewModel claimRequest;
        public CommandViewModel ClaimRequest
        {
            get
            {
                if (claimRequest == null)
                {
                    claimRequest = CommandHelper.GetControlCommands(this, appController,(int) ActionType.AddClaim);
                }
                return claimRequest;
            }
        }



        #endregion

        #region Constructors

        public CalculationResultVM()
        {
            PeriodMgtAppLocalizedResources = new PeriodMgtAppLocalizedResources();
            init();
            // EmployeeCalculationResults.Add(new EmployeeCalculationResultDTO { Id = 4, Name = "Test" });           
        }

        public CalculationResultVM(IPMSController appController,
                            ICalculationServiceWrapper calculationService,
                            IPeriodMgtAppLocalizedResources periodMgtAppLocalizedResources,IPeriodServiceWrapper periodService)
        {

            this.appController = appController;
            this.calculationService = calculationService;
            this.periodService = periodService;
            PeriodMgtAppLocalizedResources = periodMgtAppLocalizedResources;
            init();


        }

        #endregion

        #region Methods

        void init()
        {
            DisplayName = "نتیجه عملکرد من";
        }

        public void Load(string employeeNo)
        {
            
            this.employeeNo = employeeNo;
            preLoad();


        }

        private void preLoad()
        {
           ShowBusyIndicator("در حال دریافت اطلاعات...");
           periodService.GetPeriodsWithDeterministicCalculation((res, exp) => appController.BeginInvokeOnDispatcher(() =>
                {
                    HideBusyIndicator();
                    if (exp == null)
                    {
                        PeriodsWithDeteministicCalculation = res;
                    }
                    else
                        appController.HandleException(exp);

                }));  
        }

        private void AddClaim()
        {
           
        }

        private void getEmployeePoint()
        {

            ShowBusyIndicator("در حال دریافت اطلاعات...");
            if (SelectedPeriod != null)
            {
                calculationService.GetDeterministicCalculation(
                    (res, exp) => appController.BeginInvokeOnDispatcher(() =>
                    {
                        if (exp != null)
                            appController.HandleException(exp);
                        else
                        {
                            Calculation = res;
                            loadEmployeeTotalScore();

                        }
                    }), SelectedPeriod.Id
                    );
        
            }

        }

        private void loadEmployeeTotalScore()
        {
            calculationService.GetEmployeeSummaryCalculationResult(
                    (res, exp) => appController.BeginInvokeOnDispatcher(() =>
                    {
                        HideBusyIndicator();
                        if (exp == null)
                        {
                            EmployeeCalcTotalScore = res;
                            if (EmployeeCalcTotalScore.JobPositionValues == null)
                                loadEmployeeJobPoints();

                        }
                        else appController.HandleException(exp);
                    }), SelectedPeriod.Id, Calculation.Id, employeeNo);
            
        }

        private void loadEmployeeJobPoints()
        {

            ShowBusyIndicator("در حال دریافت اطلاعات...");
            calculationService.GetEmployeeJobPositionsCalculationResult((res, exp) => appController.BeginInvokeOnDispatcher(() =>
            {
                HideBusyIndicator();
                if (exp == null)
                {
                    EmployeeCalcTotalScore.JobPositionValues = res;
                    OnPropertyChanged("EmployeeCalcTotalScore");
                }
                else
                    appController.HandleException(exp);
            })
                , SelectedPeriod.Id, Calculation.Id, EmployeeCalcTotalScore.EmployeeNo);
        }

        protected override void OnRequestClose()
        {
            base.OnRequestClose();
            appController.Close(this);
        }

        public void Handle(UpdateCalculationResultListArgs eventData)
        {
           
        }

        #endregion
    }

}
