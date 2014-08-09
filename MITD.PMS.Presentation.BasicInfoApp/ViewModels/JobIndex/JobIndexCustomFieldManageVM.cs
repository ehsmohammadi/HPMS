using System.Linq;
using MITD.PMS.Presentation.Contracts;
using MITD.Presentation;
using MITD.PMS.Presentation.BasicInfoApp;
using System;
using System.Collections.Generic;


namespace MITD.PMS.Presentation.Logic
{
    public class JobIndexCustomFieldManageVM : BasicInfoWorkSpaceViewModel
    {
        #region Fields

        private readonly IPMSController appController;
        private readonly IJobIndexServiceWrapper jobIndexService;
        private readonly ICustomFieldServiceWrapper customFieldService;
        private ActionType actionType;

        #endregion

        #region Properties

        private JobIndexDTO jobIndex ;
        public  JobIndexDTO JobIndex
        {
            get { return jobIndex; }
            set { this.SetField(vm => vm.JobIndex, ref jobIndex, value); }
        }

        private List<AbstractCustomFieldDescriptionDTO> jobIndexCustomFieldDescriptionList;
        public List<AbstractCustomFieldDescriptionDTO> JobIndexCustomFieldDescriptionList
        {
            get { return jobIndexCustomFieldDescriptionList; }
            set { this.SetField(vm => vm.JobIndexCustomFieldDescriptionList, ref jobIndexCustomFieldDescriptionList, value); }
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

        public JobIndexCustomFieldManageVM()
        {
            BasicInfoAppLocalizedResources=new BasicInfoAppLocalizedResources();
            init();
            JobIndex = new JobIndexDTO { Name = "شغل یک" };
        }


        public JobIndexCustomFieldManageVM(IPMSController appController, 
            IJobIndexServiceWrapper jobIndexService,
            ICustomFieldServiceWrapper customFieldService,
            IBasicInfoAppLocalizedResources basicInfoAppLocalizedResources)
        {
            this.appController = appController;
            this.jobIndexService = jobIndexService;
            this.customFieldService = customFieldService;
            BasicInfoAppLocalizedResources = basicInfoAppLocalizedResources;
            init();

        } 

        #endregion

        #region Methods

        private void init()
        {
            JobIndex = new JobIndexDTO();
            DisplayName = BasicInfoAppLocalizedResources.JobIndexCustomFieldManageViewTitle;
            
        }


        public void Load(JobIndexDTO jobIndexParam, ActionType actionTypeParam)
        {
            actionType = actionTypeParam;
            JobIndex = jobIndexParam;
            ShowBusyIndicator();
            customFieldService.GetAllCustomFieldsDescription( (res, exp) => appController.BeginInvokeOnDispatcher(() =>
                {

                    if (exp == null)
                    {
                        JobIndexCustomFieldDescriptionList = res;
                        if (actionType == ActionType.ManageJobIndexCustomFields)
                            setCurrentJobCustomFields();
                        HideBusyIndicator();
                    }
                    else
                    {
                        HideBusyIndicator();
                        appController.HandleException(exp);
                    }
                }),"JobIndex");

        }



        private void setCurrentJobCustomFields()
        {
            jobIndexCustomFieldDescriptionList.Where(allFields => jobIndex.CustomFields.Select(f => f.Id).Contains(allFields.Id))
                                         .ToList()
                                         .ForEach(field => field.IsChecked = true);
        }

        private void save()
        {
            var selectedFields = JobIndexCustomFieldDescriptionList.Where(f => f.IsChecked).ToList();
            appController.Publish(new UpdateJobIndexCustomFieldListArgs(selectedFields));
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

