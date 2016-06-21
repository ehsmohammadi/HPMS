using System.Windows;
using MITD.PMS.Presentation.Contracts;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public class ChangePasswordVM : WorkspaceViewModel
    {
        #region Fields

        private readonly IPMSController appController;
        private readonly IUserServiceWrapper userService;

        #endregion

        #region Properties & BackField

        private ChangePasswordDTO  changePassword;
        public ChangePasswordDTO ChangePassword
        {
            get { return changePassword; }
            set { this.SetField(vm => vm.ChangePassword, ref changePassword, value); }
        }

        private string reEnterNewPassword;
        public string ReEnterNewPassword
        {
            get { return reEnterNewPassword; }
            set { this.SetField(vm => vm.ReEnterNewPassword, ref reEnterNewPassword, value); }
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

        public ChangePasswordVM()
        {
            init();
        }

        public ChangePasswordVM(IUserServiceWrapper userService,
            IPMSController appController)
        {

            this.userService = userService;
            this.appController = appController;
            init();

        }

        #endregion

        #region Methods

        private void init()
        {
            ChangePassword=new ChangePasswordDTO();
            ReEnterNewPassword=string.Empty;
        }

        private void save()
        {
            if(!ChangePassword.Validate())
                return;
            if (ChangePassword.NewPassword != ReEnterNewPassword)
            {
                appController.ShowMessage("رمز عبور و تکرار رمزعبور می بایست یکسان باشد");
                return;
            }
            ShowBusyIndicator();
            userService.ChangePassword((res, exp) => appController.BeginInvokeOnDispatcher(() =>
            {
                HideBusyIndicator();
                if (exp != null)
                {
                    appController.HandleException(exp);
                }
                else
                {
                    if (appController.ShowMessage("تغییر رمز با موفقیت انجام شد", "پیام", MessageBoxButton.OK) == MessageBoxResult.OK)
                    {
                        OnRequestClose();
                    }

                }

            }), ChangePassword);
        }
        
        protected override void OnRequestClose()
        {
            base.OnRequestClose();
            appController.Close(this);
        }
        
        #endregion
    }
}

