using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.PeriodManagementApp;
using MITD.Presentation;
 
namespace MITD.PMS.Presentation.Logic
{
    public sealed class JobInPeriodJobIndicesVM : WorkspaceViewModel
    {
        #region Fields

        private readonly IPMSController appController;
        private readonly IJobInPeriodServiceWrapper jobInPeriodService;
        private readonly IJobServiceWrapper jobService;
        private readonly IPeriodServiceWrapper periodService;
        private readonly IJobIndexInPeriodServiceWrapper jobIndexInPeriodService;
        private ActionType actionType;

        #endregion

        #region Properties

        private JobInPeriodDTO jobInPeriod;
        public JobInPeriodDTO JobInPeriod
        {
            get { return jobInPeriod; }
            set { this.SetField(vm => vm.JobInPeriod, ref jobInPeriod, value); }
        }

        private PeriodDTO period;
        public PeriodDTO Period
        {
            get { return period; }
            set { this.SetField(vm => vm.Period, ref period, value); }
        }


        private ObservableCollection<JobInPeriodJobIndexDTO> jobIndexInPeriodList = new ObservableCollection<JobInPeriodJobIndexDTO>();
        public ObservableCollection<JobInPeriodJobIndexDTO> JobIndexInPeriodList
        {
            get { return jobIndexInPeriodList; }
            set { this.SetField(vm => vm.JobIndexInPeriodList, ref jobIndexInPeriodList, value); }
        }


        private JobInPrdField jobInPrdField;
        public JobInPrdField JobInPrdField
        {
            get { return jobInPrdField; }
            set { this.SetField(vm => vm.JobInPrdField, ref jobInPrdField, value); }
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

        public JobInPeriodJobIndicesVM()
        {
        }
        public JobInPeriodJobIndicesVM(IPMSController appController, 
            IJobInPeriodServiceWrapper jobInPeriodService,
            IJobServiceWrapper jobService,IPeriodServiceWrapper periodService,
            IJobIndexInPeriodServiceWrapper jobIndexInPeriodService
            )
        {
            this.appController = appController;
            this.jobInPeriodService = jobInPeriodService;
            this.jobService = jobService;
            this.periodService = periodService;
            this.jobIndexInPeriodService = jobIndexInPeriodService;
            DisplayName = " مدیریت شاخص های مرتبط با شغل ";
        }

        #endregion

        #region Methods

        public void Load(long periodId,JobInPeriodDTO jobInPeriodParam, ActionType actionTypeParam)
        {
            if (jobInPeriodParam == null)
                return;
            JobInPeriod = jobInPeriodParam;
            actionType = actionTypeParam;
            preLoad(periodId);

            jobIndexInPeriodService.GetAllPeriodJobIndexes((res, exp) =>  appController.BeginInvokeOnDispatcher(() =>
            {

                if (exp == null)
                {
                    foreach (var jobIndex in res)
                    {
                        var jobInPeriodJobIndex = JobInPeriod.JobIndices.SingleOrDefault(j => j.Id == jobIndex.Id);
                        if (jobInPeriodJobIndex != null)
                        {
                            jobInPeriodJobIndex.IsChecked = true;
                            JobIndexInPeriodList.Add(jobInPeriodJobIndex);
                        }                           
                        else
                        {
                            JobIndexInPeriodList.Add(new JobInPeriodJobIndexDTO
                            {
                                Id = jobIndex.Id,
                                Name = jobIndex.Name,
                                IsInquireable = jobIndex.IsInquireable,
                                ShowforLowLevel = true,
                                ShowforSameLevel = true,
                                ShowforTopLevel = true
                            });
                        }
                    }
                    HideBusyIndicator();
                }
                else
                {
                    HideBusyIndicator();
                    appController.HandleException(exp);
                    
                }
                   

            }),periodId);
        }


        private void preLoad(long periodId)
        {
            ShowBusyIndicator();

            periodService.GetPeriod((res, exp) =>appController.BeginInvokeOnDispatcher(() =>
                {
                    if (exp == null)
                        Period = res;
                    else
                        appController.HandleException(exp);
                }),periodId);

           
            
        }

        private void save()
        {
            var selectedJobIndices = jobIndexInPeriodList.Where(f => f.IsChecked).ToList();
            if (period != null && JobInPeriod != null)
                appController.Publish(new UpdateJobInPeriodJobIndexListArgs(selectedJobIndices, Period.Id, JobInPeriod.JobId));
            OnRequestClose();
        }

        private void FinalizeAction(JobInPrdField res)
        {
            //appController.Publish(new UpdateJobInPeriodArgs(res, actionType));
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
