using System.Collections.Generic;
using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Resources;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using MITD.Core;
using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;

namespace MITD.Presentation.Config
{
    public static class ModuleConfigHelper
    {
        public static void ConfigureActionModule<T>(Dictionary<int, Type> dicActionSevices)
            where T:IActionService
        {
       
            var appController = ServiceLocator.Current.GetInstance<IPMSController>();
            foreach (var dic in dicActionSevices)
            {
                
                var container = ServiceLocator.Current.GetInstance<IWindsorContainer>();
                container.Register(
                   Component.For(dic.Value).LifeStyle.Singleton
                    );
                var actionService = ServiceLocator.Current.GetInstance(dic.Value); 
                var action=appController.PMSActions.Single(a => (int)a.ActionCode == dic.Key);
                action.ActionService = (IActionService)actionService;



            }

        }

    }
}
