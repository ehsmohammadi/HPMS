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
    public class UserListVM : BasicInfoWorkSpaceViewModel, IEventHandler<UpdateUserListArgs>
    {
        #region Fields

        private readonly IPMSController appController;
        private readonly IUserServiceWrapper userService;
        
        #endregion

        #region Properties & Back fields
         
        private PagedSortableCollectionView<UserDTOWithActions> users;
        public PagedSortableCollectionView<UserDTOWithActions> Users
        {
            get { return users; }
            set { this.SetField(p => p.Users, ref users, value); }
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
                    ((IUserListView)View).CreateContextMenu(new ReadOnlyCollection<DataGridCommandViewModel>(UserCommands));
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
                    filterCommand = new CommandViewModel(BasicInfoAppLocalizedResources.UserListViewCommandTitle,new DelegateCommand(refresh));
                }
                return filterCommand;
            }
        }

        private List<DataGridCommandViewModel> userCommands;
        public List<DataGridCommandViewModel> UserCommands
        {
            get { return userCommands; }
            private set { 
                this.SetField(p => p.UserCommands, ref userCommands, value);
                if (UserCommands.Count > 0) SelectedCommand = UserCommands[0];
            }

        }

        private UserCriteria userCriteria;
        public UserCriteria UserCriteria
        {
            get { return userCriteria; }
            set { this.SetField(p => p.UserCriteria, ref userCriteria, value); }
        }


        #endregion

        #region Constructors

        public UserListVM()
        {
            init();
            Users.Add(new UserDTOWithActions{PartyName = "ehsan"});
            BasicInfoAppLocalizedResources=new BasicInfoAppLocalizedResources();
        }

        public UserListVM(IBasicInfoController basicInfoController, IPMSController appController,
            IUserServiceWrapper userService, IBasicInfoAppLocalizedResources localizedResources)
        {
            this.appController = appController;
            this.userService = userService;
            BasicInfoAppLocalizedResources = localizedResources;
            DisplayName = BasicInfoAppLocalizedResources.UserListViewTitle;
            init();
        }

        #endregion

        #region Methods

        void init()
        {
            UserCriteria = new UserCriteria();
            Users = new PagedSortableCollectionView<UserDTOWithActions> { PageSize = 20 };
            Users.OnRefresh += (s, args) => refresh();
            UserCommands = new List<DataGridCommandViewModel>
            {
                   CommandHelper.GetControlCommands(this, appController, new List<int>{ (int) ActionType.AddUser }).FirstOrDefault()
            };
            

        }
       
        private List<DataGridCommandViewModel> createCommands()
        {
            return CommandHelper.GetControlCommands(this, appController, SelectedUser.ActionCodes);
        }

        public void Load()
        {
            refresh();
        }

       
        private void refresh()
        {
            var sortBy = Users.SortDescriptions.ToDictionary(sortDesc => sortDesc.PropertyName, sortDesc =>
                (sortDesc.Direction == ListSortDirection.Ascending ? "ASC" : "DESC"));
            ShowBusyIndicator("در حال دریافت اطلاعات...");

            userService.GetAllUsers(
                   (res, exp) => appController.BeginInvokeOnDispatcher(() =>
                  {
                      HideBusyIndicator();
                      if (exp == null)
                      {
                          if(res.Result!=null)
                            Users.SourceCollection = res.Result;
                          else
                              Users.SourceCollection = new Collection<UserDTOWithActions>();
                          Users.TotalItemCount = res.TotalCount;
                          Users.PageIndex = Math.Max(0, res.CurrentPage - 1);
                      }
                      else appController.HandleException(exp);
                  }), Users.PageSize, Users.PageIndex + 1,sortBy,UserCriteria);
        }


        protected override void OnRequestClose()
        {
            base.OnRequestClose();
            appController.Close(this);
        }
     
        public void Handle(UpdateUserListArgs eventData)
        {
            refresh();
        }

        #endregion


    }
}
