using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using MITD.Core;
using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;
using MITD.PMS.Presentation.Logic.Wrapper;

namespace MITD.PMS.Presentation.UI.SL
{
    public partial class App : Application
    {
        public App()
        {
            this.Startup += this.Application_Startup;
            this.Exit += this.Application_Exit;
            this.UnhandledException += this.Application_UnhandledException;

            InitializeComponent();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            PMSClientConfig.BaseApiSiteAddress = e.InitParams["WebApiSite"];

            var culture = new CultureInfo("fa-IR");//new CultureInfo("en-US");// new CultureInfo("fa-IR");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            new BootStrapper().Execute();
            var controller = ServiceLocator.Current.GetInstance<IPMSController>();

#if(DEBUG)
            // Uncomment NOT to use SSO
            controller.getLogonUser();
            //controller.Login(() => { });
#else
            // Uncomment to use SSO
            controller.Login(() => { });
#endif

        }

        private void Application_Exit(object sender, EventArgs e)
        {

        }

        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {


            // If the app is running outside of the debugger then report the exception using
            // the browser's exception mechanism. On IE this will display it a yellow alert 
            // icon in the status bar and Firefox will display a script error.
            if (!System.Diagnostics.Debugger.IsAttached)
            {

                // NOTE: This will allow the application to continue running after an exception has been thrown
                // but not handled. 
                // For production applications this error handling should be replaced with something that will 
                // report the error to the website and stop the application.

                
                var controller = ServiceLocator.Current.GetInstance<IPMSController>();
                if (controller != null)
                {
                    controller.ShowMessage("خطای داخلی در سطح کاربری رخ داده است لطفا با راهبران سیستم تماس حاصل فرمایید ");
                    //controller.ShowMessage(e.ExceptionObject.ToString());
                }
                e.Handled = true;
                Deployment.Current.Dispatcher.BeginInvoke(delegate { ReportErrorToDOM(e); });
            }
            else
            {
                var controller = ServiceLocator.Current.GetInstance<IPMSController>();
                if (controller != null)
                {
                    controller.HandleException(e.ExceptionObject);
                }
                e.Handled = true;
            }
        }

        

        private void ReportErrorToDOM(ApplicationUnhandledExceptionEventArgs e)
        {
            try
            {
                string errorMsg = e.ExceptionObject.Message + e.ExceptionObject.StackTrace;
                errorMsg = errorMsg.Replace('"', '\'').Replace("\r\n", @"\n");

                System.Windows.Browser.HtmlPage.Window.Eval("throw new Error(\"Unhandled Error in Silverlight Application " + errorMsg + "\");");
            }
            catch (Exception)
            {
            }
        }
    }
}
