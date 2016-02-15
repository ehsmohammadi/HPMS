using MITD.PMS.Presentation.Contracts;
using MITD.Presentation;
using MITD.PMS.Presentation.BasicInfoApp;
using System.Collections.ObjectModel;

namespace MITD.PMS.Presentation.Logic
{
    public class CustomFieldVM : BasicInfoWorkSpaceViewModel
    { 
        #region Fields

        private readonly IPMSController appController;
        private readonly ICustomFieldServiceWrapper customFieldService;
        private ActionType actionType;

        #endregion

        #region Properties & BackField

        private CustomFieldDTO customField;
        public CustomFieldDTO CustomField
        {
            get { return customField; }
            set { this.SetField(vm => vm.CustomField, ref customField, value); }
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

        private ObservableCollection<CustomFieldEntity> customFieldEntities;
        public ObservableCollection<CustomFieldEntity> CustomFieldEntities
        {
            get { return customFieldEntities; }
            set { this.SetField(vm => vm.CustomFieldEntities, ref customFieldEntities, value); }
        }

        #endregion

        #region Constructors

        public CustomFieldVM()
        {

            CustomField = new CustomFieldDTO { Name = "شاخص یک", DictionaryName="CustomField1" };
        }

        public CustomFieldVM( ICustomFieldServiceWrapper customFieldService, 
                           IPMSController appController,
                           IBasicInfoAppLocalizedResources basicInfoAppLocalizedResources)
        {
           
            this.customFieldService = customFieldService;
            this.appController = appController;
            BasicInfoAppLocalizedResources = basicInfoAppLocalizedResources;
            CustomField = new CustomFieldDTO();
            DisplayName = BasicInfoAppLocalizedResources.CustomFieldViewTitle;
        } 

        #endregion

        #region Methods

        public void Load(CustomFieldDTO customFieldParam,ActionType actionTypeParam)
        {
            actionType = actionTypeParam;
            preload();
            CustomField = customFieldParam;
        }

        private void preload()
        {
            CustomFieldEntities = new ObservableCollection<CustomFieldEntity>(appController.CustomFieldEntityList);
        }

       
        private void save()
        {
            customField.TypeId = "string";
            if (!customField.Validate()) return;
            ShowBusyIndicator();
            if (actionType == ActionType.CreateCustomField)
            {
                customFieldService.AddCustomField((res, exp) => appController.BeginInvokeOnDispatcher(() =>
                    {
                        HideBusyIndicator();
                        if (exp != null)
                            appController.HandleException(exp);
                        else
                            FinalizeAction();
                    }), customField);
            }
            else if (actionType == ActionType.ModifyCustomField)
            {
                customFieldService.UpdateCustomField((res, exp) => appController.BeginInvokeOnDispatcher(() =>
                    {
                        HideBusyIndicator();
                        if (exp != null)
                            appController.HandleException(exp);
                        else
                            FinalizeAction();
                    }), customField);
            }
        }

        private void FinalizeAction()
        {
            appController.Publish(new UpdateCustomFieldListArgs());
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

