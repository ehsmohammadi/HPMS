using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.PeriodManagementApp;
using MITD.Presentation;
 
namespace MITD.PMS.Presentation.Logic
{
    public sealed class JobInPrdFieldsVM : WorkspaceViewModel
    {
        #region Fields

        private readonly IPMSController appController;
        private readonly IJobInPeriodServiceWrapper jobInPeriodService;
        private readonly IJobServiceWrapper jobService;
        private readonly IPeriodServiceWrapper periodService;
        private ActionType actionType;

        #endregion

        #region Properties

        private JobDTO job;
        public JobDTO Job
        {
            get { return job; }
            set { this.SetField(vm => vm.Job, ref job, value); }
        }

        private PeriodDTO period;
        public PeriodDTO Period
        {
            get { return period; }
            set { this.SetField(vm => vm.Period, ref period, value); }
        }


        private List<AbstractCustomFieldDescriptionDTO> jobCustomFieldDescriptionList = new List<AbstractCustomFieldDescriptionDTO>();
        public List<AbstractCustomFieldDescriptionDTO> JobCustomFieldDescriptionList
        {
            get { return jobCustomFieldDescriptionList; }
            set { this.SetField(vm => vm.JobCustomFieldDescriptionList, ref jobCustomFieldDescriptionList, value); }
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

        public JobInPrdFieldsVM()
        {
            //Period = new Period { Name = "دوره اول", StartDate = DateTime.Now, EndDate = DateTime.Now };
        }
        public JobInPrdFieldsVM(IPMSController appController, 
            IJobInPeriodServiceWrapper jobInPeriodService,
            IJobServiceWrapper jobService,IPeriodServiceWrapper periodService)
        {
            this.appController = appController;
            this.jobInPeriodService = jobInPeriodService;
            this.jobService = jobService;
            this.periodService = periodService;
            DisplayName = " مدیریت فیلدهای مرتبط با شغل ";
        }

        #endregion

        #region Methods

        //public void Load(JobInPrdField jobInPrdFieldParam, ActionType actionTypeParam)
        public void Load(long periodId,JobInPeriodDTO jobInPeriod, ActionType actionTypeParam)
        {
            if (jobInPeriod == null)
                return;

            actionType = actionTypeParam;
            preLoad(periodId);
            ShowBusyIndicator("در حال دریافت اطلاعات...");
            jobService.GetJob((res, exp) =>  appController.BeginInvokeOnDispatcher(() =>
            {
                HideBusyIndicator();
                if (exp == null)
                {
                    Job = res;
                    JobCustomFieldDescriptionList = new List<AbstractCustomFieldDescriptionDTO>(Job.CustomFields
                        .Select(f => new AbstractCustomFieldDescriptionDTO() { Name = f.Name, Id = f.Id }).ToList());

                    jobCustomFieldDescriptionList.Where(allFields => jobInPeriod.CustomFields.Select(f => f.Id).Contains(allFields.Id))
                                   .ToList()
                                   .ForEach(field => field.IsChecked = true);
                }
                else
                    appController.HandleException(exp);

            }), jobInPeriod.JobId);
        }


        private void preLoad(long periodId)
        {
            ShowBusyIndicator();

            periodService.GetPeriod((res, exp) => appController.BeginInvokeOnDispatcher(() =>
                {
                    if (exp == null)
                        Period = res;
                    else
                        appController.HandleException(exp);
                }),periodId);

           
            
        }

        private void save()
        {
            var selectedFields = JobCustomFieldDescriptionList.Where(f => f.IsChecked).ToList();
            if(period != null && Job != null)
                appController.Publish(new UpdateJobInPeriodCustomFieldListArgs(selectedFields,Period.Id,Job.Id));
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
