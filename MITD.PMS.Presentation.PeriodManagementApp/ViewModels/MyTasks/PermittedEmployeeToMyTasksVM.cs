using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using MITD.PMS.Presentation.Contracts;
using MITD.Presentation;
using MITD.PMS.Presentation.PeriodManagementApp;
using MITD.PMS.Presentation.PeriodManagementApp.Views;
using System.Collections.ObjectModel;

namespace MITD.PMS.Presentation.Logic
{
    public class PermittedUserToMyTasksVM : PeriodMgtWorkSpaceViewModel
    { 
        #region Fields

        private readonly IPMSController appController;
        private readonly IMyTasksServiceWrapper myTasksService;
        private string employeeNo;
        private ActionType actionType;

        #endregion

        #region Properties & BackField

        private UserStateDTO userState;
        public UserStateDTO UserState
        {
            get { return userState; }
            set { this.SetField(vm => vm.UserState, ref userState, value); }
        }


        private ObservableCollection<CheckBoxListViewModel<UserDTO>> users;
        public ObservableCollection<CheckBoxListViewModel<UserDTO>> Users
        {
            get { return users; }
            set { this.SetField(p => p.Users, ref users, value); }
        }

        private CommandViewModel saveCommand;
        public CommandViewModel SaveCommand
        {
            get
            {
                if (saveCommand == null)
                {
                    saveCommand = new CommandViewModel(" تایید ", new DelegateCommand(save));
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

        public PermittedUserToMyTasksVM()
        {

            
        }

        public PermittedUserToMyTasksVM(IPMSController appController,
                            IMyTasksServiceWrapper myTasksService,
                            IPeriodMgtAppLocalizedResources periodMgtAppLocalizedResources)
        {

            this.appController = appController;
            this.myTasksService = myTasksService;
            PeriodMgtAppLocalizedResources = periodMgtAppLocalizedResources;
            DisplayName = PeriodMgtAppLocalizedResources.PermittedUserToMyTasksViewTitle;
        } 

        #endregion

        #region Methods

        public void Load( UserStateDTO userState)
        {

            UserState = userState;
            employeeNo = userState.EmployeeNo;
            preload();
        }

        private void preload()
        {

            //myTasksService.GetAllAcceptableUsersToPermitOnMyTasks((res, exp) => appController.BeginInvokeOnDispatcher(() =>
            //    {
            //        if (exp == null)
            //        {
            //           Users = CheckBoxListViewHelper<UserDTO>.PrepareListForCheckBoxListView(res.Result.ToList());
            //        }
            //        else
            //            appController.HandleException(exp);
            //    }) , userState.Username);
        }
       
        private void save()
        {
           
            //ShowBusyIndicator();
            //var selectedEmpList = Users.Where(e => e.IsChecked).Select(e => e.Data).ToList();
            //myTasksService.PermitUsersToMyTasks((res, exp) =>  appController.BeginInvokeOnDispatcher(() =>
            //    {
            //        if (exp == null)
            //            appController.ShowMessage("افراد انتخاب شده به فهرست افراد مجاز به دیدن کارتابل اضافه شدند");
            //        else
            //            appController.HandleException(exp);

            //    }),UserState.UserId,selectedEmpList.Select(e=>e.Id).ToList());
            
            //appController.Publish(new UpdatePermittedUserListArgs(UserState.UserId));
            //FinalizeAction();
                    
        }

        private void FinalizeAction()
        {
            
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

