using System.Linq;
using System.Windows;
using MITD.Core;
using MITD.PMS.Presentation.Contracts;
using MITD.Presentation;
using MITD.PMS.Presentation.BasicInfoApp;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace MITD.PMS.Presentation.Logic
{
    public class UserVM : BasicInfoWorkSpaceViewModel, IEventHandler<UpdatePartyCustomActionsArgs>, IEventHandler<UpdateWorkListUsersArgs>
    { 
        #region Fields

        private readonly IPMSController appController;
        private readonly IUserServiceWrapper userService;
        private ActionType actionType;

        #endregion

        #region Properties & BackField

        private UserDTO user;
        public UserDTO User
        {
            get { return user; }
            set { this.SetField(vm => vm.User, ref user, value); }
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

        private CommandViewModel customActionsCommand;
        public CommandViewModel CustomActionsCommand
        {
            get
            {
                if (customActionsCommand == null)
                    customActionsCommand = CommandHelper.GetControlCommands(this, appController, (int)ActionType.ManageUserCustomActions);
                return customActionsCommand;
            }
        }

        private CommandViewModel workListUsersCommand;
        public CommandViewModel WorkListUsersCommand
        {
            get
            {
                if (workListUsersCommand == null)
                    workListUsersCommand = CommandHelper.GetControlCommands(this, appController, (int)ActionType.ManageUserWorkListUsers);
                return workListUsersCommand;
            }
        }

        private List<UserGroupDescriptionDTO> userGroupList;
        public List<UserGroupDescriptionDTO> UserGroupList
        {
            get { return userGroupList; }
            set { this.SetField(vm => vm.UserGroupList, ref userGroupList, value); }
        }

        public bool IsModifyMode
        {
            get { return (actionType == ActionType.ModifyUser); }
        }

        public bool IsCreateMode
        {
            get { return (actionType == ActionType.AddUser); }
        }

        #endregion

        #region Constructors

        public UserVM()
        {

            User = new UserDTO { PartyName = "uname1",FirstName= "User1" };
        }

        public UserVM( IUserServiceWrapper userService, 
                           IPMSController appController,
                           IBasicInfoAppLocalizedResources basicInfoAppLocalizedResources)
        {
           
            this.userService = userService;
            this.appController = appController;
            BasicInfoAppLocalizedResources = basicInfoAppLocalizedResources;
            User = new UserDTO();
            DisplayName = BasicInfoAppLocalizedResources.UserViewTitle;
        } 

        #endregion

        #region Methods

        public void Load(UserDTO customFieldParam,ActionType actionTypeParam)
        {
            actionType = actionTypeParam;
            User = customFieldParam;
            preload();
            
        }

        private void preload()
        {
            ShowBusyIndicator();
            userService.GetAllUserGroupsDescriptions((res, exp) => appController.BeginInvokeOnDispatcher(() =>
            {
                HideBusyIndicator();
                if (exp == null)
                {
                    UserGroupList = res;
                    if (actionType == ActionType.ModifyUser)
                        loadUser();
                }
                else
                    appController.HandleException(exp);

            }));
        }

        private void loadUser()
        {
            ShowBusyIndicator();
            userService.GetUser((res, exp) => appController.BeginInvokeOnDispatcher(() =>
            {
                HideBusyIndicator();
                if (exp == null)
                {
                    User = res;
                    UserGroupList.Where(allGroups => User.Groups.Select(g => g.PartyName).Contains(allGroups.PartyName))
                                 .ToList().ForEach(g => g.IsChecked = true);
                }
                else
                    appController.HandleException(exp);

            }), User.PartyName);

        }

       
        private void save()
        {
            //user.TypeId = "string";
            if (!user.Validate()) return;
            ShowBusyIndicator();
            User.Groups = userGroupList.Where(f => f.IsChecked).ToList();
            
            if (actionType==ActionType.AddUser)
            {
                userService.AddUser((res, exp) => appController.BeginInvokeOnDispatcher(() =>
                    {
                        HideBusyIndicator();
                        if (exp != null)
                            appController.HandleException(exp);
                        else
                            FinalizeAction();
                    }), user);
            }
            else if (actionType == ActionType.ModifyUser)
            {
                userService.UpdateUser((res, exp) => appController.BeginInvokeOnDispatcher(() =>
                    {
                        HideBusyIndicator();
                        if (exp != null)
                            appController.HandleException(exp);
                        else
                            FinalizeAction();
                    }), user);
            }
        }

        private void FinalizeAction()
        {
            appController.Publish(new UpdateUserListArgs());
            OnRequestClose();
        }


        protected override void OnRequestClose()
        {
            base.OnRequestClose();
            appController.Close(this);
        }


        public void Handle(UpdatePartyCustomActionsArgs eventData)
        {
            if (eventData.PartyName == User.PartyName)
                User.CustomActions = eventData.CustomActions;

        }
        
        public void Handle(UpdateWorkListUsersArgs eventData)
        {
            if (eventData.PartyName == User.PartyName)
                User.WorkListPermittedUsers = eventData.WorkListUsers;

        }

        #endregion

        
    }
}

