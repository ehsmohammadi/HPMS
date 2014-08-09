using System.ComponentModel;
using MITD.Core;
using MITD.PMS.Presentation.Contracts;
using MITD.Presentation;
using MITD.PMS.Presentation.BasicInfoApp;
using MITD.PMS.Presentation.BasicInfoApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MITD.PMS.Presentation.Logic
{
    public class UserGroupListVM : BasicInfoWorkSpaceViewModel, IEventHandler<UpdateUserGroupListArgs>
    {
        #region Fields

        private readonly IPMSController appController;
        private readonly IUserServiceWrapper userService;
        
        #endregion

        #region Properties & Back fields
         
        private List<UserGroupDTOWithActions> userGroups;
        public List<UserGroupDTOWithActions> UserGroups
        {
            get { return userGroups; }
            set { this.SetField(p => p.UserGroups, ref userGroups, value); }
        }

        private UserGroupDTOWithActions selectedUserGroup;
        public UserGroupDTOWithActions SelectedUserGroup
        {
            get { return selectedUserGroup; }
            set
            {
                this.SetField(p => p.SelectedUserGroup, ref selectedUserGroup, value);
                if (selectedUserGroup == null) return;
                UserGroupCommands = createCommands();
                if (View != null)
                    ((IUserGroupListView)View).CreateContextMenu(new ReadOnlyCollection<DataGridCommandViewModel>(UserGroupCommands));
            }
        }

        private DataGridCommandViewModel selectedCommand;
        public DataGridCommandViewModel SelectedCommand
        {
            get { return selectedCommand; }
            set { this.SetField(p => p.SelectedCommand, ref selectedCommand, value); }
        }

        
        private CommandViewModel filterCommand;
        public CommandViewModel FilterCommand
        {
            get
            {
                if (filterCommand == null)
                {
                    filterCommand = new CommandViewModel(BasicInfoAppLocalizedResources.UserGroupListViewTitle,new DelegateCommand(refresh));
                }
                return filterCommand;
            }
        }

        private List<DataGridCommandViewModel> userCommands;
        public List<DataGridCommandViewModel> UserGroupCommands
        {
            get { return userCommands; }
            private set { 
                this.SetField(p => p.UserGroupCommands, ref userCommands, value);
                if (UserGroupCommands.Count > 0) SelectedCommand = UserGroupCommands[0];
            }

        }


        #endregion

        #region Constructors

        public UserGroupListVM()
        {
            init();
            UserGroups.Add(new UserGroupDTOWithActions{PartyName = "ehsan"});
            BasicInfoAppLocalizedResources=new BasicInfoAppLocalizedResources();
        }

        public UserGroupListVM(IBasicInfoController basicInfoController, IPMSController appController,
            IUserServiceWrapper userService, IBasicInfoAppLocalizedResources localizedResources)
        {
            this.appController = appController;
            this.userService = userService;
            BasicInfoAppLocalizedResources = localizedResources;
            DisplayName = BasicInfoAppLocalizedResources.UserGroupListViewTitle;
            init();
        }

        #endregion

        #region Methods

        void init()
        {
            UserGroups = new List<UserGroupDTOWithActions>();
            UserGroupCommands = new List<DataGridCommandViewModel>
            {
                   CommandHelper.GetControlCommands(this, appController, new List<int>{ (int) ActionType.AddUserGroup }).FirstOrDefault()
            };
        }
       
        private List<DataGridCommandViewModel> createCommands()
        {
            return CommandHelper.GetControlCommands(this, appController, SelectedUserGroup.ActionCodes);
        }

        public void Load()
        {
            refresh();
        }

        private void refresh()
        {
           ShowBusyIndicator("در حال دریافت اطلاعات...");
           userService.GetAllUserGroups(
                  (res, exp) => appController.BeginInvokeOnDispatcher(() =>
                  {
                      HideBusyIndicator();
                      if (exp == null)
                        UserGroups = res;
                      else appController.HandleException(exp);
                  }));
        }


        protected override void OnRequestClose()
        {
            base.OnRequestClose();
            appController.Close(this);
        }
     
        public void Handle(UpdateUserGroupListArgs eventData)
        {
            refresh();
        }

        #endregion


    }
}
