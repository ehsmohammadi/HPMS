using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using MITD.Core;
using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.PeriodManagementApp;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public class CalculationResultForManagerVM : PeriodMgtWorkSpaceViewModel, IEventHandler<UpdateCalculationResultListArgs>
    {
        #region Fields

        private readonly IPMSController appController;
        private readonly ICalculationServiceWrapper calculationService;
        private readonly IPeriodServiceWrapper periodService;
        private string managerEmployeeNo;

        #endregion

        #region Properties


        private ObservableCollection<PeriodDescriptionDTO> periodsWithConfirmedResult;
        public ObservableCollection<PeriodDescriptionDTO> PeriodsWithConfirmedResult
        {
            get { return periodsWithConfirmedResult; }
            set { this.SetField(p => p.PeriodsWithConfirmedResult, ref periodsWithConfirmedResult, value); }
        }

        private PeriodDescriptionDTO selectedPeriod;
        public PeriodDescriptionDTO SelectedPeriod
        {
            get { return selectedPeriod; }
            set
            {
                this.SetField(p => p.SelectedPeriod, ref selectedPeriod, value);
                getEmployeeResult();
               
            }
        }

        private SubordinatesResultDTO subordinatesResultDTO;
        public SubordinatesResultDTO SubordinatesResultDTO
        {
            get { return subordinatesResultDTO; }
            set { this.SetField(p => p.SubordinatesResultDTO, ref subordinatesResultDTO, value); }
        }


        private string leveledUnitPoint;
        public string LeveledUnitPoint
        {
            get { return leveledUnitPoint; }
            set { this.SetField(p => p.LeveledUnitPoint, ref leveledUnitPoint, value); }

        }


        private string excellentPointEmployeePercent;
        public string ExcellentPointEmployeePercent
        {
            get { return excellentPointEmployeePercent; }
            set { this.SetField(p => p.ExcellentPointEmployeePercent, ref excellentPointEmployeePercent, value); }

        }

        private string goodPointEmployeePercent;
        public string GoodPointEmployeePercent
        {
            get { return goodPointEmployeePercent; }
            set { this.SetField(p => p.GoodPointEmployeePercent, ref goodPointEmployeePercent, value); }

        }

        private string expectedPointEmployeePercent;
        public string ExpectedPointEmployeePercent
        {
            get { return expectedPointEmployeePercent; }
            set { this.SetField(p => p.ExpectedPointEmployeePercent, ref expectedPointEmployeePercent, value); }

        }

        private string needForTrainingPointEmployeePercent;
        public string NeedForTrainingPointEmployeePercent
        {
            get { return needForTrainingPointEmployeePercent; }
            set { this.SetField(p => p.NeedForTrainingPointEmployeePercent, ref needForTrainingPointEmployeePercent, value); }

        }

        private string undesirablePointEmployeePercent;
        public string UndesirablePointEmployeePercent
        {
            get { return undesirablePointEmployeePercent; }
            set { this.SetField(p => p.UndesirablePointEmployeePercent, ref undesirablePointEmployeePercent, value); }

        }


        private ObservableCollection<JobIndexValueDTO> strengthEmployeeIndices;
        public ObservableCollection<JobIndexValueDTO> StrengthEmployeeIndices
        {
            get { return strengthEmployeeIndices; }
            set { this.SetField(p => p.StrengthEmployeeIndices, ref strengthEmployeeIndices, value); }
        }

        private ObservableCollection<JobIndexValueDTO> weakEmployeeIndices;
        public ObservableCollection<JobIndexValueDTO> WeakEmployeeIndices
        {
            get { return weakEmployeeIndices; }
            set { this.SetField(p => p.WeakEmployeeIndices, ref weakEmployeeIndices, value); }
        }

        //private ObservableCollection<JobIndexValueDTO> trainingEmployeeIndices;
        //public ObservableCollection<JobIndexValueDTO> TrainingEmployeeIndices
        //{
        //    get { return trainingEmployeeIndices; }
        //    set { this.SetField(p => p.TrainingEmployeeIndices, ref trainingEmployeeIndices, value); }
        //}

        #endregion

        #region Constructors

        public CalculationResultForManagerVM()
        {
            PeriodMgtAppLocalizedResources = new PeriodMgtAppLocalizedResources();
            init();
            // EmployeeCalculationResults.Add(new EmployeeCalculationResultDTO { Id = 4, Name = "Test" });           
        }

        public CalculationResultForManagerVM(IPMSController appController,
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
            DisplayName = "نتیجه عملکرد زیر مجموعه";
        }

        public void Load(string employeeNo)
        {
            
            this.managerEmployeeNo = employeeNo;
            preLoad();


        }

        private void preLoad()
        {
           ShowBusyIndicator("در حال دریافت اطلاعات...");
           periodService.GetPeriodsWithConfirmedResult((res, exp) => appController.BeginInvokeOnDispatcher(() =>
                {
                    HideBusyIndicator();
                    if (exp == null)
                    {
                        PeriodsWithConfirmedResult = res;
                        if (PeriodsWithConfirmedResult.Any())
                            SelectedPeriod = PeriodsWithConfirmedResult.First();
                    }
                    else
                        appController.HandleException(exp);

                }));  
        }

        private void getEmployeeResult()
        {
            periodService.GetSubordinatesResultInPeriod(
                    (res, exp) => appController.BeginInvokeOnDispatcher(() =>
                    {
                        if (exp != null)
                            appController.HandleException(exp);
                        else
                        {
                            SubordinatesResultDTO = res;
                            calculateSubordinatesPointPercent(SubordinatesResultDTO);

                            //StrengthEmployeeIndices = new ObservableCollection<JobIndexValueDTO>(SubordinatesResultDTO.JobIndexValues.Where(j => Decimal.Parse(j.IndexValue, CultureInfo.InvariantCulture) >= 90));
                            //WeakEmployeeIndices = new ObservableCollection<JobIndexValueDTO>(SubordinatesResultDTO.JobIndexValues.Where(j => Decimal.Parse(j.IndexValue, CultureInfo.InvariantCulture) < 30));
                            //TrainingEmployeeIndices = new ObservableCollection<JobIndexValueDTO>(SubordinatesResultDTO.JobIndexValues.Where(j => Decimal.Parse(j.IndexValue, CultureInfo.InvariantCulture) <= 50));
                           
                            LeveledUnitPoint = levelPoint(SubordinatesResultDTO.TotalUnitPoint);
                        }
                    }), SelectedPeriod.Id,managerEmployeeNo
                    );

        }

        private void calculateSubordinatesPointPercent(SubordinatesResultDTO subordinatesResultDTOParam)
        {
            var subordinatesCount = subordinatesResultDTOParam.Subordinates.Count;
            var subordinatesWithExcellentPoint =
                subordinatesResultDTOParam.Subordinates.Count(s => Decimal.Parse(s.TotalPoint) >= 90);
            var subordinatesWithGoodPoint =
                subordinatesResultDTOParam.Subordinates.Count(s => 70 <= Decimal.Parse(s.TotalPoint) && Decimal.Parse(s.TotalPoint) < 90);
            var subordinatesWithExpectedPoint =
                subordinatesResultDTOParam.Subordinates.Count(s => 50 <= Decimal.Parse(s.TotalPoint) && Decimal.Parse(s.TotalPoint) < 70);
            var subordinatesWithNeedTrainingPoint =
                subordinatesResultDTOParam.Subordinates.Count(s => 30 <= Decimal.Parse(s.TotalPoint) && Decimal.Parse(s.TotalPoint) < 50);
            var subordinatesWithUndesirablePoint =
                subordinatesResultDTOParam.Subordinates.Count(s => 0 < Decimal.Parse(s.TotalPoint) && Decimal.Parse(s.TotalPoint) < 30);

            ExcellentPointEmployeePercent = (subordinatesWithExcellentPoint/subordinatesCount*100).ToString();
            GoodPointEmployeePercent = (subordinatesWithGoodPoint / subordinatesCount * 100).ToString();
            ExpectedPointEmployeePercent = (subordinatesWithExpectedPoint / subordinatesCount * 100).ToString();
            NeedForTrainingPointEmployeePercent = (subordinatesWithNeedTrainingPoint / subordinatesCount * 100).ToString();
            UndesirablePointEmployeePercent = (subordinatesWithUndesirablePoint / subordinatesCount * 100).ToString();


        }

        private string levelPoint(string totalPoint)
        {
            var point = Decimal.Parse(totalPoint, CultureInfo.InvariantCulture);
            if (point >= 90)
                return "عالی";
            if (point < 90 && point >= 70)
                return "خوب";
            if (point < 70 && point >= 50)
                return "قابل قبول";
            if (point < 50 && point >= 30)
                return "نیاز به آموزش";
            if (point < 30 && point > 0)
                return "نا مطلوب";
            return "-";

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
