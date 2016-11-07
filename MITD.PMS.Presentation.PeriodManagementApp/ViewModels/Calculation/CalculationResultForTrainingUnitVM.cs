using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MITD.Core;
using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.PeriodManagementApp;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public class CalculationResultForTrainingUnitVM : PeriodMgtWorkSpaceViewModel, IEventHandler<UpdateCalculationResultListArgs>
    {
        #region Fields

        private readonly IPMSController appController;
        private readonly ICalculationServiceWrapper calculationService;
        private readonly IPeriodServiceWrapper periodService;
        private string trainerEmployeeNo;

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
                getTrainingEmployeeIndices();
            }
        }

        private List<JobIndexValueDTO> trainingEmployeeIndices;
        public List<JobIndexValueDTO> TrainingEmployeeIndices
        {
            get { return trainingEmployeeIndices; }
            set { this.SetField(p => p.TrainingEmployeeIndices, ref trainingEmployeeIndices, value); }
        }


        private JobIndexValueDTO selectedTrainingEmployeeIndex;
        public JobIndexValueDTO SelectedTrainingEmployeeIndex
        {
            get { return selectedTrainingEmployeeIndex; }
            set
            {
                this.SetField(p => p.SelectedTrainingEmployeeIndex, ref selectedTrainingEmployeeIndex, value);
                getEmployeeResult();
            }
        }

        private SubordinatesResultDTO trainingNeedEmployeeDTO;
        public SubordinatesResultDTO TrainingNeedEmployeeDTO
        {
            get { return trainingNeedEmployeeDTO; }
            set { this.SetField(p => p.TrainingNeedEmployeeDTO, ref trainingNeedEmployeeDTO, value); }
        }

        #endregion

        #region Constructors

        public CalculationResultForTrainingUnitVM()
        {
            PeriodMgtAppLocalizedResources = new PeriodMgtAppLocalizedResources();
            init();
        }

        public CalculationResultForTrainingUnitVM(IPMSController appController,
                            ICalculationServiceWrapper calculationService,
                            IPeriodMgtAppLocalizedResources periodMgtAppLocalizedResources, IPeriodServiceWrapper periodService)
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
            DisplayName = "نتیجه عملکرد افراد زیر مجموعه";
        }

        public void Load(string employeeNo)
        {

            this.trainerEmployeeNo = employeeNo;
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
                         {
                             SelectedPeriod = PeriodsWithConfirmedResult.First();
                             getTrainingEmployeeIndices();
                         }

                     }
                     else
                         appController.HandleException(exp);

                 }));
        }

        private void getTrainingEmployeeIndices()
        {
            periodService.GetTrainingEmployeeIndicesInPeriod(
                     (res, exp) => appController.BeginInvokeOnDispatcher(() =>
                     {
                         if (exp != null)
                             appController.HandleException(exp);
                         else
                             TrainingEmployeeIndices = res;
                     }), SelectedPeriod.Id, trainerEmployeeNo
                     );
        }



        private void getEmployeeResult()
        {
            periodService.GetTrainingNeedEmployeeInPeriod(
                    (res, exp) => appController.BeginInvokeOnDispatcher(() =>
                    {
                        if (exp != null)
                            appController.HandleException(exp);
                        else
                            TrainingNeedEmployeeDTO = res;
                    }), SelectedPeriod.Id, trainerEmployeeNo, SelectedTrainingEmployeeIndex.JobIndexId
                    );

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
