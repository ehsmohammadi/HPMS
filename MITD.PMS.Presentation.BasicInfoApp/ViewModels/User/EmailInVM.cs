using System.Windows;
using MITD.PMS.Common;
using MITD.PMS.Presentation.Contracts;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public class EmailInVM : WorkspaceViewModel
    {
        #region Fields

        private readonly IPMSController appController;
        private readonly IUserServiceWrapper userService;

        #endregion

        #region Properties & BackField

        private EmailDTO  emailDTO;
        public EmailDTO EmailDTO
        {
            get { return emailDTO; }
            set { this.SetField(vm => vm.EmailDTO, ref emailDTO, value); }
        }

        private string reEnterEmail;
        public string ReEnterEmail
        {
            get { return reEnterEmail; }
            set { this.SetField(vm => vm.ReEnterEmail, ref reEnterEmail, value); }
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



        #endregion

        #region Constructors

        public EmailInVM()
        {
            init();
        }

        public EmailInVM(IUserServiceWrapper userService,
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
            EmailDTO=new EmailDTO();
            ReEnterEmail=string.Empty;
        }

        public void Load(UserStateDTO user)
        {
            if (user.Email != null && user.Email.Status == (int) EmailStatusEnum.Unverified)
                EmailDTO = user.Email;
        }

        private void save()
        {
            if(!EmailDTO.Validate())
                return;
            if (EmailDTO.Email != ReEnterEmail)
            {
                appController.ShowMessage("پست الکترونیکی و تکرار پست الکترونیکی می بایست یکسان باشد");
                return;
            }
            //if (EmailDTO.Email.Length<6)
            //{
            //    appController.ShowMessage("رمز عبور باید حداقل 6 کارکتر باشد");
            //    return;
            //}
            ShowBusyIndicator();
            userService.UpdateEmail((res, exp) => appController.BeginInvokeOnDispatcher(() =>
            {
                HideBusyIndicator();
                if (exp != null)
                {
                    appController.HandleException(exp);
                }
                else
                {
                    if (appController.ShowMessage("برای فعال سازی پست الکترونیکی به آدرس پست الکترونیکی وارد شده مراجعه کنید ", "پیام", MessageBoxButton.OK) == MessageBoxResult.OK)
                    {
                        OnRequestClose();
                    }

                }

            }), EmailDTO);
        }
        
        protected override void OnRequestClose()
        {
            base.OnRequestClose();
            appController.Close(this);
        }
        
        #endregion


    }
}

