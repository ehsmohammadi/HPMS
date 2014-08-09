using MITD.PMS.Presentation.Contracts;
using MITD.Presentation;
using MITD.PMS.Presentation.BasicInfoApp;
using System;


namespace MITD.PMS.Presentation.Logic
{
    public class JobIndexCategoryVM : BasicInfoWorkSpaceViewModel
    {
        #region Fields

        private readonly IPMSController appController;
        private readonly IJobIndexServiceWrapper jobIndexCategoryService;
        private ActionType actionType;

        #endregion

        #region Properties

        private string parentCategoryName = "ریشه";
        public string ParentCategoryName
        {
            get { return parentCategoryName; }
            set { this.SetField(vm => vm.ParentCategoryName, ref parentCategoryName, value); }
        }

        private JobIndexCategoryDTO jobIndexCategory;
        public JobIndexCategoryDTO JobIndexCategory
        {
            get { return jobIndexCategory; }
            set { this.SetField(vm => vm.JobIndexCategory, ref jobIndexCategory, value); }
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
                    cancelCommand = new CommandViewModel("انصراف", new DelegateCommand(OnRequestClose));
                }
                return cancelCommand;
            }
        }

        #endregion

        #region Constructors

        public JobIndexCategoryVM()
        {
            JobIndexCategory = new JobIndexCategoryDTO { Name = "شغل یک", DictionaryName = "JobIndexCategory1" };
        }
        public JobIndexCategoryVM(IPMSController appController, IJobIndexServiceWrapper jobIndexCategoryService)
        {
            this.appController = appController;
            this.jobIndexCategoryService = jobIndexCategoryService;
            JobIndexCategory = new JobIndexCategoryDTO();
            DisplayName = "دسته شاخص ";
        }

        #endregion

        #region Methods

        public void Load(JobIndexCategoryDTO jobIndexCategoryParam, ActionType actionTypeParam)
        {
            actionType = actionTypeParam;
            JobIndexCategory = jobIndexCategoryParam;

            if (JobIndexCategory != null && jobIndexCategory.ParentId.HasValue)
            {
                ShowBusyIndicator();
                jobIndexCategoryService.GetJobIndexCategory((res, exp) => appController.BeginInvokeOnDispatcher(() =>
                {
                    HideBusyIndicator();
                    ParentCategoryName = res.Name;
                }), jobIndexCategory.ParentId.Value);
            }

        }


        private void save()
        {
            if (!jobIndexCategory.Validate()) return;

            ShowBusyIndicator();
            if (actionType == ActionType.AddJobIndexCategory)
            {
                jobIndexCategoryService.AddJobIndexCategory((res, exp) => appController.BeginInvokeOnDispatcher(() =>
                {
                    HideBusyIndicator();
                    if (exp != null)
                        appController.HandleException(exp);
                    else
                        finalizeAction();
                }), jobIndexCategory);
            }
            else if (actionType == ActionType.ModifyJobIndexCategory)
            {
                jobIndexCategoryService.UpdateJobIndexCategory((res, exp) => appController.BeginInvokeOnDispatcher(() =>
                {
                    HideBusyIndicator();
                    if (exp != null)
                        appController.HandleException(exp);
                    else
                        finalizeAction();
                }), jobIndexCategory);
            }
        }

        private void finalizeAction()
        {
            appController.Publish(new UpdateJobIndexTreeArgs());
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

