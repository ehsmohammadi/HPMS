using System;
using System.Collections.ObjectModel;
using System.Linq;
using MITD.PMS.Presentation.Contracts;
using MITD.Presentation;
using MITD.PMS.Presentation.BasicInfoApp;
using MITD.Core;
 

namespace MITD.PMS.Presentation.Logic
{
    public class JobVM : BasicInfoWorkSpaceViewModel,IEventHandler<UpdateJobCustomFieldListArgs>
    {
        #region Fields

        private readonly IPMSController appController;
        private readonly IJobServiceWrapper jobService;
        private readonly ICustomFieldServiceWrapper customFieldService;
        private ActionType actionType;

        #endregion

        #region Properties

        private JobDTO job;
        public JobDTO Job
        {
            get { return job; }
            set { this.SetField(vm => vm.Job, ref job, value); }
        }

        private CommandViewModel manageJobFieldsCommand;
        public CommandViewModel ManageJobFieldsCommand
        {
            get
            {
                if (manageJobFieldsCommand == null)
                    manageJobFieldsCommand = CommandHelper.GetControlCommands(this, appController, (int)ActionType.ManageJobCustomFields);
                return manageJobFieldsCommand;
            }
        }

        private CommandViewModel saveCommand;
        public CommandViewModel SaveCommand
        {
            get
            {
                if (saveCommand == null)
                {
                    saveCommand = new CommandViewModel("ذخیره", new DelegateCommand(save));
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
                    cancelCommand = new CommandViewModel("انصراف",new DelegateCommand(OnRequestClose));
                }
                return cancelCommand;
            }
        } 

        #endregion

        #region Constructors

        public JobVM()
        {
            Job = new JobDTO { Name = "شغل یک", DictionaryName="Job1" };
        }
        public JobVM(IPMSController appController, IJobServiceWrapper jobService,ICustomFieldServiceWrapper customFieldService)
        {
            this.appController = appController;
            this.jobService = jobService;
            this.customFieldService = customFieldService;
            Job = new JobDTO();
            DisplayName = "شغل ";
        } 

        #endregion

        #region Methods

        public void Load(JobDTO jobParam,ActionType actionTypeParam)
        {
            actionType = actionTypeParam;
            if (actionType == ActionType.ModifyJob)
            {
                ShowBusyIndicator();
                jobService.GetJob( (res, exp) => appController.BeginInvokeOnDispatcher(() =>
                    {
                        HideBusyIndicator();
                        if (exp == null)
                            Job = res;
                        else
                            appController.HandleException(exp);
                    }),
                                        jobParam.Id);
            }
        }

        private void save()
        {
            if (!job.Validate()) return;

            ShowBusyIndicator();

            job.TransferId = Guid.NewGuid();
            if (actionType == ActionType.CreateJob)
            {
                jobService.AddJob((res, exp) => appController.BeginInvokeOnDispatcher(() =>
                    {
                        HideBusyIndicator();
                        if (exp != null)
                            appController.HandleException(exp);
                        else
                            FinalizeAction();
                    }), job);
            }
            else if (actionType == ActionType.ModifyJob)
            {
                jobService.UpdateJob((res, exp) => appController.BeginInvokeOnDispatcher(() =>
                    {
                        HideBusyIndicator();
                        if (exp != null)
                            appController.HandleException(exp);
                        else
                            FinalizeAction();
                    }), job);
            }
        }

        private void FinalizeAction()
        {
            appController.Publish(new UpdateJobListArgs());
            OnRequestClose();
        }

        protected override void OnRequestClose()
        {
            base.OnRequestClose();
            appController.Close(this);
        }


        public void Handle(UpdateJobCustomFieldListArgs eventData)
        {
            Job.CustomFields=new ObservableCollection<CustomFieldDTO>();
            var fieldIdList = eventData.JobCustomFieldDescriptionList.Select(f => f.Id).ToList();

            customFieldService.GetAllCustomFields( (res, exp) => appController.BeginInvokeOnDispatcher(() =>
                {
                    if (exp == null)
                         
                        Job.CustomFields = new ObservableCollection<CustomFieldDTO>(res.Where(f=> fieldIdList.Contains(f.Id)) );
                    else
                        appController.HandleException(exp);
                }),"Job");
        }

        #endregion

    }
}

