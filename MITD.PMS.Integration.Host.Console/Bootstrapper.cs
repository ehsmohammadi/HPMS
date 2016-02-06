using Castle.MicroKernel.Registration;
using Castle.Windsor;
using MITD.Core;
using MITD.Core.Config;
using MITD.PMS.Integration.Core;
using MITD.PMS.Integration.Domain;
using MITD.PMS.Integration.PMS.API;
using MITD.Presentation;

namespace MITD.PMS.Integration.Host.Console
{
    public class Bootstrapper
    {
        public void Execute()
        {
            var container = new WindsorContainer();
            container.Register(Classes.FromAssemblyNamed("MITD.PMS.Integration.Domain")
                .BasedOn<IConverter>().WithService.FromInterface().LifestyleTransient());

            container.Register(Classes.FromAssemblyNamed("MITD.PMS.Integration.Data.EF")
                    .BasedOn<IDataProvider>().WithService.FromInterface().LifestyleTransient());

            container.Register(Classes.FromAssemblyNamed("MITD.PMS.Integration.PMS.API")
                    .BasedOn<IServiceWrapper>().WithService.FromInterface().LifestyleTransient());

            container.Register(Component.For<IUserProvider>().ImplementedBy<UserProvider>().LifestyleSingleton());

            container.Register(Component.For<IEventPublisher>().ImplementedBy<EventPublisher>().LifestyleSingleton());

            container.Register(Component.For<ConverterManager>().LifestyleSingleton());

            var locator = new WindsorServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => locator);
        }
    }
}
