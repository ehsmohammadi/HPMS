using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MITD.Core;
using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.PeriodManagementApp;
using MITD.PMS.Presentation.PeriodManagementApp.Views;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public class PermittedUserListToMyTasksVM : PeriodMgtWorkSpaceViewModel
        , IEventHandler<UpdatePermittedUserListArgs>
    {
        #region Fields

        private readonly IPMSController appController;
        private readonly IMyTasksServiceWrapper myTasksService;
       

        #endregion

        #region Properties

        private PagedSortableCollectionView<UserDTOWithActions> users;
        public PagedSortableCollectionView<UserDTOWithActions> Users
        {
            get
            {
               
                return users;
            }
            set { this.SetField(p => p.Users, ref users, value); }
        }

        private UserStateDTO userState;
        public UserStateDTO UserState
        {
            get { return userState; }
            set { this.SetField(p => p.UserState, ref userState, value); }
        }


        private UserDTOWithActions selectedUser;
        public UserDTOWithActions SelectedUser
        {
            get { return selectedUser; }
            set
            {
                this.SetField(p => p.SelectedUser, ref selectedUser, value);
                if (selectedUser == null) return;
                UserCommands = createCommands();
                if (View != null)
                    ((IPermittedUserListToMyTasksView)View).CreateContextMenu(new ReadOnlyCollection<DataGridCommandViewModel>(UserCommands));
            }
        }

        private List<DataGridCommandViewModel> userCommands;
        public List<DataGridCommandViewModel> UserCommands
        {
            get { return userCommands; }
            private set
            {
                this.SetField(p => p.UserCommands, ref userCommands, value);
                if (UserCommands.Count > 0) SelectedCommand = UserCommands[0];
            }

        }

        private DataGridCommandViewModel selectedCommand;
        public DataGridCommandViewModel SelectedCommand
        {
            get { return selectedCommand; }
            set { this.SetField(p => p.SelectedCommand, ref selectedCommand, value); }
        }

        #endregion

        #region Constructors

        public PermittedUserListToMyTasksVM()
        {
            PeriodMgtAppLocalizedResources=new PeriodMgtAppLocalizedResources();
            UserState = new UserStateDTO();
            init();
            Users.Add(new UserDTOWithActions());          
        }

        public PermittedUserListToMyTasksVM(IPMSController appController,
                            IMyTasksServiceWrapper myTasksService, 
                            IPeriodMgtAppLocalizedResources periodMgtAppLocalizedResources)
        {
            
            this.appController = appController;
            this.myTasksService = myTasksService;
            PeriodMgtAppLocalizedResources = periodMgtAppLocalizedResources;
            init();
            

        }

        #endregion

        #region Methods

        void init()
        {
            DisplayName = PeriodMgtAppLocalizedResources.PermittedUserListToMyTasksViewTitle;
            Users = new PagedSortableCollectionView<UserDTOWithActions>();
            Users.OnRefresh += (s, args) => refresh();
            UserCommands = new List<DataGridCommandViewModel>
            {
                   CommandHelper.GetControlCommands(this, appController, new List<int>{ (int) ActionType.AddPermittedUserToMyTasks}).FirstOrDefault()
            };
        }

        private List<DataGridCommandViewModel> createCommands()
        {
            return CommandHelper.GetControlCommands(this, appController, SelectedUser.ActionCodes);
        }

        public void Load(UserStateDTO userState)
        {
            UserState = userState;
            refresh();
        }

        private void refresh()
        {
            //ShowBusyIndicator("در حال دریافت اطلاعات...");
            //myTasksService.GetAllPermittedUsersToMyTasks(
            //    (res, exp) =>  appController.BeginInvokeOnDispatcher(() =>
            //    {
            //        HideBusyIndicator();
            //        if (exp == null)
            //        {
            //            Users.SourceCollection = res.Result;
            //            Users.TotalItemCount = res.TotalCount;
            //            Users.PageIndex = Math.Max(0, res.CurrentPage - 1);
            //        }
            //        else appController.HandleException(exp);
            //    }), UserState.Username, Users.PageSize, Users.PageIndex + 1);
        }

        protected override void OnRequestClose()
        {
            base.OnRequestClose();
            appController.Close(this);
        }


        public void Handle(UpdatePermittedUserListArgs eventData)
        {
            if (eventData.UserId == userState.UserId)
                refresh();
        }

        #endregion

       
    }

}
