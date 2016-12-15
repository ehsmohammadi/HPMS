using System;
using System.Collections.ObjectModel;
using MITD.Core;
using MITD.Presentation;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Presentation.Logic
{

    public sealed class LoginVM : WorkspaceViewModel, IEventHandler<HideBusyIndicatorArgs>
    {

        #region Fields

        private readonly IPMSController controller;

        #endregion

        #region Properties

        public IMainAppLocalizedResources LocalizedResources { get; set; }

        public DateTime TimeToLogOut
        {
            get { return DateTime.Now; }
        }

        private string userName;
        public string UserName
        {
            get { return userName; }
            set { this.SetField(c => c.UserName, ref userName, value); }
        }

        private string password;
        private IUserServiceWrapper service;
        public string Password
        {
            get { return password; }
            set { this.SetField(c => c.Password, ref password, value); }

        }

        #endregion

        #region Constructor

        public LoginVM()
        {
            DisplayName = "سامانه بررسی عملکرد";
            UserName = "نادر";
            Password = "محمدی";

        }

        public LoginVM(IPMSController controller,
                            IUserServiceWrapper service,
                            IMainAppLocalizedResources localizedResources)
        {
            this.controller = controller;
            LocalizedResources = localizedResources;
            this.service = service;
            DisplayName = LocalizedResources.PMSTitle;
        }

        #endregion


        #region Command Methods

        private CommandViewModel loginCommand;
        public CommandViewModel LoginCommand
        {
            get
            {
                if (loginCommand == null)
                {
                    loginCommand = new CommandViewModel("ورود", new DelegateCommand(() =>
                    {

                        if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                            controller.ShowMessage("نام کاربری یا کلمه عبور نباید خالی باشد");
                        else
                        {
                            ShowBusyIndicator("در حال دریافت اطلاعات...");
                            //controller.Login(userName, password,
                            //() =>
                            //{
                            //    controller.BeginInvokeOnDispatcher(() =>
                            //        {
                            //            HideBusyIndicator();
                            //            controller.Close(this);
                            //        });
                            //});
                        }

                    }));
                }
                return loginCommand;
            }
        }
        #endregion

        public void Handle(HideBusyIndicatorArgs eventData)
        {
            HideBusyIndicator();
        }
    }
}


