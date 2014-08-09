using Castle.MicroKernel.Registration;
using Castle.Windsor;
using MITD.PMS.Presentation.Logic;
using Microsoft.Practices.ServiceLocation;
using MITD.Core;
using MITD.Core.Config;
using MITD.Presentation.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Resources;
using System.Windows.Shapes;

namespace MITD.Presentation.Config
{
    public static class ApplicationConfigHelperPMS
    {
        public static void Configure<T1,T2>(Dictionary<string, List<Type>> modules) 
            where T1:WorkspaceViewModel
            where T2:UserControl, IMainView
        {
            var container = new WindsorContainer();
            container.AddFacility<EventAggregatorFacility>();
            container.Register(
                Component.For<IWindsorContainer>().Instance(container),
                Component.For<IViewManager>().ImplementedBy<ViewManager>().LifestyleSingleton(),
                Component.For<IDeploymentServiceWrapper>().ImplementedBy<DeploymentServiceWrapper>().LifestyleSingleton(),
                Classes.FromAssemblyContaining<T2>().BasedOn<ILocalizedResources>().WithService.FromInterface().LifestyleTransient(),
                Classes.FromAssemblyContaining<T2>().BasedOn<IView>().WithService.FromInterface().LifestyleTransient(),
                Classes.FromAssemblyContaining<T2>().BasedOn<IDialogView>().WithService.FromInterface().LifestyleTransient(),
                Classes.FromAssemblyContaining<T1>().BasedOn<IApplicationController>().WithService.FromInterface().LifestyleSingleton(),
                Classes.FromAssemblyContaining<T1>().BasedOn<IServiceWrapper>().WithService.FromInterface().LifestyleSingleton(),
                Classes.FromAssemblyContaining<T1>().BasedOn<WorkspaceViewModel>().LifestyleTransient(),
                Component.For<DeploymentManagement>().LifestyleSingleton(),
                Component.For<IDeploymentManagement>().UsingFactoryMethod<DeploymentManagement>(
                    c =>
                    {
                        var deploymentManagement = c.Resolve<DeploymentManagement>();
                        prepareModules(modules, deploymentManagement);
                        return deploymentManagement;
                    }).LifestyleSingleton()
                );
            var locator = new WindsorServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => locator);

             var vm = ServiceLocator.Current.GetInstance<T1>();
            var page = Activator.CreateInstance<T2>();
            page.DataContext = vm;
            var viewManager = ServiceLocator.Current.GetInstance<IViewManager>();
            viewManager.TabControl = page.TabControl;
            viewManager.TabHeaderTemplate = page.TabHeaderTemplate;
            viewManager.BusyIndicatorObject = page.BusyIndicator;
            Application.Current.RootVisual = page;
            ViewManager.Initialize();
        }

        private static void prepareModules(Dictionary<string, List<Type>> modules, DeploymentManagement deploymentManagement)
        {
            foreach (var module in modules)
            {
                deploymentManagement.Modules.Add(new DeploymentModule
                {
                    FileUrl = "",
                    Name = module.Key,
                    Types = module.Value
                });
            }
        }

        public static void ConfigureModule<T1,T2>(StreamResourceInfo resourceDic)
            where T1:class
            where T2:class,T1
        {
            var container = ServiceLocator.Current.GetInstance<IWindsorContainer>();

            var sReader = new StreamReader(resourceDic.Stream);
            var sText = sReader.ReadToEnd();
            var rd = XamlReader.Load(sText);
            Application.Current.Resources.MergedDictionaries.Add(rd as ResourceDictionary);

            container.Register(
                Classes.FromAssemblyContaining<T2>().BasedOn<IView>().WithService.FromInterface().LifestyleTransient(),
                Classes.FromAssemblyContaining<T2>().BasedOn<WorkspaceViewModel>().LifestyleTransient(),
                Component.For<T1>().ImplementedBy<T2>().LifestyleSingleton()
                );
        }
    }
}
