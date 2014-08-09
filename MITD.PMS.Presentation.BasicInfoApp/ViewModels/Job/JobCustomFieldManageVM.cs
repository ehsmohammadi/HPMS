using System.Linq;
using MITD.PMS.Presentation.Contracts;
using MITD.Presentation;
using MITD.PMS.Presentation.BasicInfoApp;
using System.Collections.Generic;


namespace MITD.PMS.Presentation.Logic
{
    public class JobCustomFieldManageVM : BasicInfoWorkSpaceViewModel
    {
        #region Fields

        private readonly IPMSController appController;
        private readonly IJobServiceWrapper jobService;
        private readonly ICustomFieldServiceWrapper customFieldService;
        private ActionType actionType;

        #endregion

        #region Properties

        private JobDTO job ;
        public  JobDTO Job
        {
            get { return job; }
            set { this.SetField(vm => vm.Job, ref job, value); }
        }

        private List<AbstractCustomFieldDescriptionDTO> jobCustomFieldDescriptionList;
        public List<AbstractCustomFieldDescriptionDTO> JobCustomFieldDescriptionList
        {
            get { return jobCustomFieldDescriptionList; }
            set { this.SetField(vm => vm.JobCustomFieldDescriptionList, ref jobCustomFieldDescriptionList, value); }
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

        public JobCustomFieldManageVM()
        {
            BasicInfoAppLocalizedResources=new BasicInfoAppLocalizedResources();
            init();
            Job = new JobDTO { Name = "شغل یک" };
        }


        public JobCustomFieldManageVM(IPMSController appController, 
            IJobServiceWrapper jobService,
            IBasicInfoAppLocalizedResources basicInfoAppLocalizedResources,
            ICustomFieldServiceWrapper customFieldService)
        {
            this.appController = appController;
            this.jobService = jobService;
            this.customFieldService = customFieldService;
            BasicInfoAppLocalizedResources = basicInfoAppLocalizedResources;
            init();

        } 

        #endregion

        #region Methods

        private void init()
        {
            Job = new JobDTO();
            DisplayName = BasicInfoAppLocalizedResources.JobCustomFieldManageViewTitle;
            JobCustomFieldDescriptionList=new List<AbstractCustomFieldDescriptionDTO>();
            
        }


        public void Load(JobDTO jobParam, ActionType actionTypeParam)
        {
            actionType = actionTypeParam;
            Job = jobParam;
            ShowBusyIndicator();
            customFieldService.GetAllCustomFieldsDescription( (res, exp) => appController.BeginInvokeOnDispatcher(() =>
                {

                    if (exp == null)
                    {
                        JobCustomFieldDescriptionList = res;
                        if (actionType == ActionType.ManageJobCustomFields)
                            setCurrentJobCustomFields();
                        HideBusyIndicator();
                    }
                    else
                    {
                        HideBusyIndicator();
                        appController.HandleException(exp);
                    }
                }),"Job");

        }

        private void setCurrentJobCustomFields()
        {
            jobCustomFieldDescriptionList.Where(allFields => Job.CustomFields.Select(f => f.Id).Contains(allFields.Id))
                .ToList()
                .ForEach(field => field.IsChecked = true);
        }

        private void save()
        {
            var selectedFields = JobCustomFieldDescriptionList.Where(f => f.IsChecked).ToList();
            appController.Publish(new UpdateJobCustomFieldListArgs(selectedFields));
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

