using MITD.Core;
using MITD.PMS.Presentation.Contracts;
using MITD.Presentation;
using MITD.PMS.Presentation.BasicInfoApp;
using System.Collections.ObjectModel;

namespace MITD.PMS.Presentation.Logic
{
    public class UserGroupVM : BasicInfoWorkSpaceViewModel, IEventHandler<UpdatePartyCustomActionsArgs>
    { 
        #region Fields

        private readonly IPMSController appController;
        private readonly IUserServiceWrapper userService;
        private ActionType actionType;

        #endregion

        #region Properties & BackField

        private UserGroupDTO userGroup;
        public UserGroupDTO UserGroup
        {
            get { return userGroup; }
            set { this.SetField(vm => vm.UserGroup, ref userGroup, value); }
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

        private CommandViewModel customActionsCommand;
        public CommandViewModel CustomActionsCommand
        {
            get
            {
                if (customActionsCommand == null)
                    customActionsCommand = CommandHelper.GetControlCommands(this, appController, (int)ActionType.ManageGroupCustomActions);
                return customActionsCommand;
            }
        }

        public bool IsModifyMode
        {
            get { return (actionType == ActionType.ModifyUserGroup); }
        }

        public bool IsCreateMode
        {
            get { return (actionType == ActionType.AddUserGroup); }
        }

        #endregion

        #region Constructors

        public UserGroupVM()
        {

            UserGroup = new UserGroupDTO { PartyName = "ugroupname1",Description= "UserGroup1" };
        }

        public UserGroupVM( IUserServiceWrapper userService, 
                           IPMSController appController,
                           IBasicInfoAppLocalizedResources basicInfoAppLocalizedResources)
        {
           
            this.userService = userService;
            this.appController = appController;
            BasicInfoAppLocalizedResources = basicInfoAppLocalizedResources;
            UserGroup = new UserGroupDTO();
            DisplayName = BasicInfoAppLocalizedResources.UserGroupViewTitle;
        } 

        #endregion

        #region Methods

        public void Load(UserGroupDTO customFieldParam,ActionType actionTypeParam)
        {
            actionType = actionTypeParam;
            UserGroup = customFieldParam;
        }

        private void save()
        {
            //user.TypeId = "string";
            if (!userGroup.Validate()) return;
            ShowBusyIndicator();
            if (actionType==ActionType.AddUserGroup)
            {
                userService.AddUserGroup((res, exp) => appController.BeginInvokeOnDispatcher(() =>
                    {
                        HideBusyIndicator();
                        if (exp != null)
                            appController.HandleException(exp);
                        else
                            FinalizeAction();
                    }), userGroup);
            }
            else if (actionType == ActionType.ModifyUserGroup)
            {
                userService.UpdateUserGroup((res, exp) => appController.BeginInvokeOnDispatcher(() =>
                    {
                        HideBusyIndicator();
                        if (exp != null)
                            appController.HandleException(exp);
                        else
                            FinalizeAction();
                    }), userGroup);
            }
        }

        private void FinalizeAction()
        {
            appController.Publish(new UpdateUserGroupListArgs());
            OnRequestClose();
        }

        protected override void OnRequestClose()
        {
            base.OnRequestClose();
            appController.Close(this);
        }


        public void Handle(UpdatePartyCustomActionsArgs eventData)
        {
            if (eventData.PartyName == userGroup.PartyName)
                userGroup.CustomActions = eventData.CustomActions;
        }

        #endregion
    }
}

