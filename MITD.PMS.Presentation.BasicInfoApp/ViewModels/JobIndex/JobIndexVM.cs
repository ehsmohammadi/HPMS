using System;
using System.Collections.Generic;
using System.Linq;
using MITD.Core;
using MITD.PMS.Presentation.Contracts;
using MITD.Presentation;
using MITD.PMS.Presentation.BasicInfoApp;
using System.Collections.ObjectModel;

namespace MITD.PMS.Presentation.Logic
{
    public class JobIndexVM : BasicInfoWorkSpaceViewModel, IEventHandler<UpdateJobIndexCustomFieldListArgs>
    {
        #region Fields

        private readonly IPMSController appController;
        private readonly IJobIndexServiceWrapper jobIndexService;
        private readonly ICustomFieldServiceWrapper customFieldService;
        private ActionType actionType;

        #endregion

        #region Properties & BackField

        private IBasicInfoAppLocalizedResources basicInfoAppLocalizedResources;
        public IBasicInfoAppLocalizedResources BasicInfoAppLocalizedResources
        {
            get { return basicInfoAppLocalizedResources; }
            set { this.SetField(vm => vm.BasicInfoAppLocalizedResources, ref basicInfoAppLocalizedResources, value); }
        }

        private JobIndexDTO jobIndex;
        public JobIndexDTO JobIndex
        {
            get { return jobIndex; }
            set { this.SetField(vm => vm.JobIndex, ref jobIndex, value); }
        }

        private CommandViewModel manageJobFieldsCommand;
        public CommandViewModel ManageJobFieldsCommand
        {
            get
            {
                if (manageJobFieldsCommand == null)
                    manageJobFieldsCommand = CommandHelper.GetControlCommands(this, appController, (int)ActionType.ManageJobIndexCustomFields);
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
                    cancelCommand = new CommandViewModel("انصراف",new DelegateCommand(OnRequestClose));
                }
                return cancelCommand;
            }
        }

        #endregion

        #region Constructors

        public JobIndexVM()
        {

            JobIndex = new JobIndexDTO { Name = "شاخص یک", DictionaryName="JobIndex1" };
        }

        public JobIndexVM( IJobIndexServiceWrapper jobIndexService, 
                           ICustomFieldServiceWrapper customFieldService,
                           IPMSController appController,
                           IBasicInfoAppLocalizedResources basicInfoAppLocalizedResources)
        {
           
            this.jobIndexService = jobIndexService;
            this.customFieldService = customFieldService;
            this.appController = appController;
            BasicInfoAppLocalizedResources = basicInfoAppLocalizedResources;
            JobIndex = new JobIndexDTO();
            DisplayName = BasicInfoAppLocalizedResources.JobIndexViewTitle;
        } 

        #endregion

        #region Methods

        public void Load(JobIndexDTO jobIndexParam,ActionType actionTypeParam)
        {
            actionType = actionTypeParam;
            JobIndex = jobIndexParam;
        }

       
        private void save()
        {
            if (!jobIndex.Validate()) return;

            ShowBusyIndicator();

            jobIndex.TransferId = Guid.NewGuid();
            if (actionType==ActionType.AddJobIndex)
            {
                jobIndexService.AddJobIndex((res, exp) => appController.BeginInvokeOnDispatcher(() =>
                    {
                        HideBusyIndicator();
                        if (exp != null)
                            appController.HandleException(exp);
                        else
                            FinalizeAction();
                    }), jobIndex);
            }
            else if (actionType == ActionType.ModifyJobIndex)
            {
                jobIndexService.UpdateJobIndex((res, exp) => appController.BeginInvokeOnDispatcher(() =>
                    {
                        HideBusyIndicator();
                        if (exp != null)
                            appController.HandleException(exp);
                        else
                            FinalizeAction();
                    }), jobIndex);
            }
        }

        private void FinalizeAction()
        {
            appController.Publish(new UpdateJobIndexTreeArgs());
            OnRequestClose();
        }

        protected override void OnRequestClose()
        {
            base.OnRequestClose();
            appController.Close(this);
        }

        public void Handle(UpdateJobIndexCustomFieldListArgs eventData)
        {
            JobIndex.CustomFields = new List<CustomFieldDTO>();
            var fieldIdList = eventData.JobIndexCustomFieldDescriptionList.Select(f => f.Id).ToList();
            customFieldService.GetAllCustomFields( (res, exp) => appController.BeginInvokeOnDispatcher(() =>
                {
                    if (exp == null)
                        JobIndex.CustomFields = res.Where(r => fieldIdList.Contains(r.Id)).ToList();
                else
                    appController.HandleException(exp);
            }),"JobIndex");
        }

        #endregion
    }
}

