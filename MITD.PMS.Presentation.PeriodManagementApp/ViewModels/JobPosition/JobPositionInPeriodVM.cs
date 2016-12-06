
using System.Collections.Generic;
using System.Linq;
using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.PeriodManagementApp;
using MITD.Presentation;
using System.Collections.ObjectModel;

namespace MITD.PMS.Presentation.Logic
{
    public class JobPositionInPeriodVM : PeriodMgtWorkSpaceViewModel
    {
        #region Fields

        private readonly IPMSController appController;
        private readonly IJobPositionServiceWrapper jobPositionService;
        private readonly IUnitInPeriodServiceWrapper unitInPeriodService;
        private readonly IJobInPeriodServiceWrapper jobInPeriodService;
        private readonly IJobPositionInPeriodServiceWrapper jobPositionInPeriodService;
        private ActionType actionType;

        #endregion

        #region Properties & BackField

        private List<JobPositionDTO> jobPositions;
        public List<JobPositionDTO> JobPositions
        {
            get { return jobPositions; }
            set { this.SetField(vm => vm.JobPositions, ref jobPositions, value); }
        }

        private ObservableCollection<JobPositionInPeriodDTO> parentJobPositions;
        public ObservableCollection<JobPositionInPeriodDTO> ParentJobPositions
        {
            get { return parentJobPositions; }
            set { this.SetField(vm => vm.ParentJobPositions, ref parentJobPositions, value); }
        }

        private List<UnitInPeriodDTO> unitInPeriods;
        public List<UnitInPeriodDTO> UnitInPeriods
        {
            get { return unitInPeriods; }
            set { this.SetField(vm => vm.UnitInPeriods, ref unitInPeriods, value); }
        }

        private List<JobInPeriodDTO> jobInPeriods;
        public List<JobInPeriodDTO> JobInPeriods
        {
            get { return jobInPeriods; }
            set { this.SetField(vm => vm.JobInPeriods, ref jobInPeriods, value); }
        }



        private JobPositionInPeriodAssignmentDTO jobPositionInPeriod;
        public JobPositionInPeriodAssignmentDTO JobPositionInPeriod
        {
            get { return jobPositionInPeriod; }
            set { this.SetField(vm => vm.JobPositionInPeriod, ref jobPositionInPeriod, value); }
        }

        private CommandViewModel saveCommand;
        public CommandViewModel SaveCommand
        {
            get
            {
                if (saveCommand == null)
                {
                    saveCommand = new CommandViewModel("تایید", new DelegateCommand(save));
                }
                return saveCommand;
            }
        }

        private CommandViewModel cancelCommand;
        public CommandViewModel CancelCommand
        {
            get
            {
                if (cancelCommand == null)
                {
                    cancelCommand = new CommandViewModel("انصراف", new DelegateCommand(OnRequestClose));
                }
                return cancelCommand;
            }
        }



        #endregion

        #region Constructors

        public JobPositionInPeriodVM()
        {
            PeriodMgtAppLocalizedResources = new PeriodMgtAppLocalizedResources();
            init();
        }

        public JobPositionInPeriodVM(IJobPositionInPeriodServiceWrapper jobPositionInPeriodService,
                           IPMSController appController,
                           IPeriodMgtAppLocalizedResources periodMgtAppLocalizedResources,
                           IJobPositionServiceWrapper jobPositionService,
                           IUnitInPeriodServiceWrapper unitInPeriodService,
            IJobInPeriodServiceWrapper jobInPeriodService)
        {

            this.jobPositionInPeriodService = jobPositionInPeriodService;
            this.appController = appController;
            this.jobPositionService = jobPositionService;
            this.unitInPeriodService = unitInPeriodService;
            this.jobInPeriodService = jobInPeriodService;
            PeriodMgtAppLocalizedResources = periodMgtAppLocalizedResources;
            init();
        }

        #endregion

        #region Methods

        private void init()
        {
            JobPositionInPeriod = new JobPositionInPeriodAssignmentDTO { };
            ParentJobPositions = new ObservableCollection<JobPositionInPeriodDTO>();
            DisplayName = PeriodMgtAppLocalizedResources.JobPositionInPeriodViewTitle;
        }

        public void Load(JobPositionInPeriodAssignmentDTO jobPositionInPeriodParam, ActionType actionTypeParam)
        {
            actionType = actionTypeParam;
            JobPositionInPeriod = jobPositionInPeriodParam;
            preLoad();
        }

        private void preLoad()
        {
            loadJobPositions();
            loadUnitInPeriod();
            loadJobInPeriod();
        }

        private void loadJobPositions()
        {
            ShowBusyIndicator("در حال دریافت اطلاعات");
            jobPositionService.GetAllJobPositions((res, exp) => appController.BeginInvokeOnDispatcher(() =>
                {
                    HideBusyIndicator();
                    if (exp == null)
                        jobPositionInPeriodService.GetAllJobPositions((jobPositionInPeriods, exp1) => appController.BeginInvokeOnDispatcher(() =>
                        {
                            ParentJobPositions = new ObservableCollection<JobPositionInPeriodDTO>(jobPositionInPeriods);
                            //foreach (var jobPositionInPeriodDTO in jobPositionInPeriods)
                            //{
                            //    //if (jobPositionInPeriodDTO.Unitid == JobPositionInPeriod.UnitId)
                            //    //{
                            //        var modifiedJobPosition = jobPositionInPeriodDTO;
                            //        modifiedJobPosition.Name = jobPositionInPeriodDTO.Name + "-" + jobPositionInPeriodDTO.UnitName;
                            //        ParentJobPositions.Add(modifiedJobPosition);
                            //    //}

                            //}
                            if (actionType == ActionType.AddJobPositionInPeriod)
                                JobPositions =
                                    res.Where(r => !jobPositionInPeriods.Select(j => j.JobPositionId).Contains(r.Id))
                                        .ToList();
                            else
                                JobPositions = res;
                        }), jobPositionInPeriod.PeriodId);

                    else
                    {
                        appController.HandleException(exp);
                    }
                }));
        }

        private void loadJobInPeriod()
        {
            ShowBusyIndicator("در حال دریافت اطلاعات");
            jobInPeriodService.GetAllJobInPeriod((res, exp) => appController.BeginInvokeOnDispatcher(() =>
                {
                    HideBusyIndicator();
                    if (exp == null)
                        JobInPeriods = res.ToList();
                    else
                        appController.HandleException(exp);
                }), JobPositionInPeriod.PeriodId);
        }

        private void loadUnitInPeriod()
        {
            ShowBusyIndicator("در حال دریافت اطلاعات");
            unitInPeriodService.GetAllUnits((res, exp) => appController.BeginInvokeOnDispatcher(() =>
                {
                    HideBusyIndicator();
                    if (exp == null)
                        UnitInPeriods = res;
                    else
                        appController.HandleException(exp);
                }), JobPositionInPeriod.PeriodId);
        }

        private void save()
        {
            if (!jobPositionInPeriod.Validate()) return;

            ShowBusyIndicator();
            if (actionType == ActionType.AddJobPositionInPeriod)
            {
                jobPositionInPeriodService.AddJobPositionInPeriod((res, exp) => appController.BeginInvokeOnDispatcher(() =>
                    {
                        HideBusyIndicator();
                        if (exp != null)
                            appController.HandleException(exp);
                        else
                            FinalizeAction();
                    }), jobPositionInPeriod);
            }
            else if (actionType == ActionType.ModifyJobPositionInPeriod)
            {
                jobPositionInPeriodService.UpdateJobPositionInPeriod((res, exp) => appController.BeginInvokeOnDispatcher(() =>
                    {
                        HideBusyIndicator();
                        if (exp != null)
                            appController.HandleException(exp);
                        else
                            FinalizeAction();
                    }), jobPositionInPeriod);
            }
        }

        private void FinalizeAction()
        {
            appController.Publish(new UpdateJobPositionInPeriodTreeArgs());
            OnRequestClose();
        }

        protected override void OnRequestClose()
        {
            base.OnRequestClose();
            appController.Close(this);
        }

        #endregion
    }
}

