using System.Collections;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using MITD.Core;
using MITD.PMS.Presentation.Contracts;
using MITD.Presentation;
using MITD.PMS.Presentation.BasicInfoApp;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System;
using System.Text;

namespace MITD.PMS.Presentation.Logic
{
    public class ManageWorkListUsersVM : BasicInfoWorkSpaceViewModel
    { 
         #region Fields

        private readonly IPMSController appController;
        private readonly IUserServiceWrapper userService;

        #endregion

        #region Properties & Back Field

        private UserDTO user ;
        public UserDTO User
        {
            get { return user; }
            set { this.SetField(vm => vm.User, ref user, value); }
        }

        private UserCriteria userCriteria;
        public UserCriteria UserCriteria
        {
            get { return userCriteria; }
            set { this.SetField(p => p.UserCriteria, ref userCriteria, value); }
        }

        private bool selectionVisible;
        public bool SelectionVisible
        {
            get { return selectionVisible; }
            set { this.SetField(p => p.SelectionVisible, ref selectionVisible, value); }
        }
        
        

        private PagedSortableCollectionView<UserDescriptionDTO> users;
        public PagedSortableCollectionView<UserDescriptionDTO> Users
        {
            get { return users; }
            set { this.SetField(p => p.Users, ref users, value); }
        }

        private ObservableCollection<UserDescriptionDTO> permittedWorkListUsers;
        public ObservableCollection<UserDescriptionDTO> PermittedWorkListUsers
        {
            get { return permittedWorkListUsers; }
            set { this.SetField(p => p.PermittedWorkListUsers, ref permittedWorkListUsers, value); }
        }


        private List<UserDescriptionDTO> selectedPermittedWorkListUsers = new List<UserDescriptionDTO>();
        public List<UserDescriptionDTO> SelectedPermittedWorkListUsers
        {
            get { return selectedPermittedWorkListUsers; }
            set
            {
                this.SetField(p => p.SelectedPermittedWorkListUsers, ref selectedPermittedWorkListUsers, value);
                (RemoveUserCommand.Command as DelegateCommand).RaiseCanExecuteChanged();
            }
        }

        private List<UserDescriptionDTO> selectedUsers = new List<UserDescriptionDTO>();
        public List<UserDescriptionDTO> SelectedUsers
        {
            get { return selectedUsers; }
            set
            {
                this.SetField(p => p.SelectedUsers, ref selectedUsers, value);
                (addUserCommand.Command as DelegateCommand).RaiseCanExecuteChanged();
            }
        }

        private CommandViewModel searchCommand;
        public CommandViewModel SearchCommand
        {
            get
            {
                if (searchCommand == null)
                {
                    searchCommand = new CommandViewModel("جستجو", new DelegateCommand(searchUser));
                }
                return searchCommand;
            }
        }

        private CommandViewModel commitWorkListUsersCommand;
        public CommandViewModel CommitWorkListUsersCommand
        {
            get
            {
                if (commitWorkListUsersCommand == null)
                {
                    commitWorkListUsersCommand = new CommandViewModel("تاييد", new DelegateCommand(save));
                }
                return commitWorkListUsersCommand;
            }
        }

        private CommandViewModel addUserCommand;
        public CommandViewModel AddUserCommand
        {
            get
            {
                if (addUserCommand == null)
                {
                    //addUserCommand = new CommandViewModel("اضافه کردن کاربر", new DelegateCommand(addUser,
                    //    () => !AllResultSelected));
                    addUserCommand = new CommandViewModel("اضافه کردن کاربر", new DelegateCommand(addUser));
                }
                return addUserCommand;
            }
        }
        
        private CommandViewModel removeUserCommand;
        public CommandViewModel RemoveUserCommand
        {
            get
            {
                if (removeUserCommand == null)
                {
                    removeUserCommand = new CommandViewModel("حذف کاربر", new DelegateCommand(removeUser, () => SelectedPermittedWorkListUsers != null));
                }
                return removeUserCommand;
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

        private ICommand selectionChangedCommand;
        public ICommand SelectionChangedCommand
        {
            get
            {
                if (selectionChangedCommand == null)
                {
                    selectionChangedCommand = new DelegateCommand<IList>(
                        list=>
                        {
                            selectedUsers.Clear();
                            foreach (var item in list)
                            {
                                selectedUsers.Add(item as UserDescriptionDTO);
                            }
                            OnPropertyChanged("SelectedUsers");
                        });
                }
                return selectionChangedCommand;
            }
        }

        private ICommand permittedUsersSelectionChanged;
        public ICommand PermittedUsersSelectionChanged
        {
            get
            {
                if (permittedUsersSelectionChanged == null)
                {
                    permittedUsersSelectionChanged = new DelegateCommand<IList>(
                        list =>
                        {
                            selectedPermittedWorkListUsers.Clear();
                            foreach (var item in list)
                            {
                                selectedPermittedWorkListUsers.Add(item as UserDescriptionDTO);
                            }
                            OnPropertyChanged("SelectedPermittedWorkListUsers");
                        });
                }
                return permittedUsersSelectionChanged;
            }
        }
        #endregion

        #region Constructors

        public ManageWorkListUsersVM()
        {
            init();
        }

        public ManageWorkListUsersVM(IPMSController appController,
                               IUserServiceWrapper userService)
        {
            this.appController = appController;
            this.userService = userService;
            init();
        }

        #endregion

        #region Methods

        void init()
        {
            DisplayName = "تعيين  دسترسی های کارتابل کاربر";
            Users = new PagedSortableCollectionView<UserDescriptionDTO>();
            PermittedWorkListUsers = new ObservableCollection<UserDescriptionDTO>();
            UserCriteria = new UserCriteria();
            Users.OnRefresh += (s, args) => getUsers(null);
        }

        public void Load(UserDTO userDtoParam)
        {
            preLoad();
            User = userDtoParam;
            PermittedWorkListUsers = new ObservableCollection<UserDescriptionDTO>(User.WorkListPermittedUsers); 
        }

        private void searchUser()
        {
            ShowBusyIndicator("در حال دریافت اطلاعات...");
            userService.GetAllUserDescriptions(
                (res, exp) => appController.BeginInvokeOnDispatcher(() =>
                {
                    HideBusyIndicator();
                    if (exp == null)
                    {
                        appController.BeginInvokeOnDispatcher(() =>
                        {
                            Users.TotalItemCount = res.TotalCount;
                            Users.PageIndex = Math.Max(0, res.CurrentPage - 1);

                            Users.SourceCollection = res.Result.ToList();
                        });
                    }
                    else appController.HandleException(exp);
                }), users.PageSize, users.PageIndex + 1, new Dictionary<string, string>(), UserCriteria);
        }

        private void preLoad()
        {
            var trigger2 = new AutoResetEvent(false);
            ThreadPool.QueueUserWorkItem(s =>
            {
                appController.BeginInvokeOnDispatcher(() => ShowBusyIndicator("در حال دریافت اطلاعات..."));
                getUsers(trigger2);
                appController.BeginInvokeOnDispatcher(HideBusyIndicator);
            });
        }

        private void getUsers(AutoResetEvent trigger)
        {
            if (trigger == null)
                ShowBusyIndicator("در حال دریافت اطلاعات...");
            userService.GetAllUserDescriptions(
               (res, exp) => appController.BeginInvokeOnDispatcher(() =>
                {
                    if (trigger != null)
                        trigger.Set();
                    else
                        HideBusyIndicator();
                    if (exp == null)
                    {
                        appController.BeginInvokeOnDispatcher(() =>
                            {
                                Users.TotalItemCount = res.TotalCount;
                                Users.PageIndex = Math.Max(0, res.CurrentPage - 1);

                                Users.SourceCollection = res.Result.ToList();
                            });
                    }
                    else appController.HandleException(exp);
                }), users.PageSize, users.PageIndex + 1,new Dictionary<string, string>(), null);
        }
        

        private void addUser()
        {
            var message = string.Empty;
            foreach (var u in selectedUsers)
            {
                if (u.PartyName == User.PartyName)
                {
                    message += " خود کاربر نمی تواند به لیست کاربران مجاز به کارتابل خود اضافه شود " ;
                } 
                else if (!permittedWorkListUsers.Contains(u))
                {   
                    PermittedWorkListUsers.Add(u);
                    OnPropertyChanged("PermittedWorkListUsers");
                }
                else
                {
                    message += "کاربر" + u.LastName + "  قبلا انتخاب شده است" + "//";

                }
            }
            if (String.IsNullOrEmpty(message))
                return;
            appController.ShowMessage(message);
        }

        private void removeUser()
        {
            if (selectedPermittedWorkListUsers.Count > 0)
            {
                var lst = new List<UserDescriptionDTO>();
                lst.AddRange(selectedPermittedWorkListUsers);
                lst.ForEach(e => permittedWorkListUsers.Remove(e));
                OnPropertyChanged("PermittedWorkListUsers");
            }
            else
            {
                appController.ShowMessage("کاربری انتخاب نشده است");
            }
        }

        private void save()
        {
            ShowBusyIndicator("در حال دریافت اطلاعات...");
            appController.Publish(new UpdateWorkListUsersArgs(PermittedWorkListUsers.ToList(),User.PartyName));
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

