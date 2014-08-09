using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Practices.ServiceLocation;
using MITD.Core;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public partial class PMController : ApplicationController, IPMController
    {

        public PMController(IViewManager viewManager,
                                     IEventPublisher eventPublisher,
                                     IDeploymentManagement deploymentManagement)
            :base(viewManager,eventPublisher,deploymentManagement)
        {
        }

        public void HandleException(Exception exp)
        {
            viewManager.ShowMessage(exp.Message);
        }


        public void GetRemoteInstance<T>(Action<T, Exception> action) where T : class
        {
            deploymentManagement.AddModule(typeof(T),
                res =>
                {
                    action(ServiceLocator.Current.GetInstance(typeof(T)) as T, null);
                },
                exp => { action(null, exp); });
        }
    }
}
