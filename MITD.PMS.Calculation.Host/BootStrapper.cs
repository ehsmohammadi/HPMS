using Castle.MicroKernel.Registration;
using Castle.Windsor;
using MITD.Core.Config;
using MITD.Core;
using MITD.Core.RuleEngine;
using MITD.Core.RuleEngine.NH;
using MITD.DataAccess.Config;
using MITD.PMS.ACL.PMSAdmin;
using MITD.PMS.Application;
using MITD.PMS.Application.Contracts;
using MITD.PMS.Calculation.Contracts;
using MITD.PMS.Domain.Service;
using MITD.PMS.Persistence.NH;
using MITD.PMSAdmin.Application;
using MITD.PMSAdmin.Persistence.NH;
using MITD.Domain.Model;
using System.Collections;
using MITD.Data.NH;
using System.Linq;
using System.Data.SqlClient;
using Castle.MicroKernel.ModelBuilder.Inspectors;
using System.ServiceModel;
using MITD.Services;
using Castle.Facilities.WcfIntegration;
using MITD.PMSSecurity.Application;
using MITD.PMSSecurity.Application.Contracts;
using MITD.PMSSecurity.Domain.Service;
using System.Threading.Tasks;
using System.Transactions;
using NHibernate.Linq.InnerJoinFetch;

namespace MITD.PMS.Calculation.Host
{
    public static class BootStrapper
    {
        private static ServiceHost host;

        public static void Execute()
        {
            configeLocator();
        }

        public static void Close()
        {
            if (host != null)
                if (host.State == CommunicationState.Opened)
                    host.Close();
        }

        private static void configeLocator()
        {
            var container = new WindsorContainer();
            container.AddFacility<WcfFacility>();
            container.Kernel.ComponentModelBuilder.RemoveContributor(
                container.Kernel.ComponentModelBuilder.Contributors.OfType<PropertiesDependenciesModelInspector>()
                    .Single());

            RegisterDataAccess(container);

            container.Register(
                Component.For<ILoggerService>()
                    .ImplementedBy<DbLoggerService>()
                    .Named("DB")
                    .LifeStyle.BoundTo<IService>(),
                Component.For<ILoggerService>()
                    .ImplementedBy<FileLoggerService>()
                    .Named("File")
                    .LifeStyle.BoundTo<IService>(),
                Component.For<ILoggerService>()
                    .ImplementedBy<WindowsEventsLoggerService>()
                    .Named("WindowsEvent")
                    .LifeStyle.BoundTo<IService>()
                );


            container.Register
                (
                    Classes.FromAssemblyContaining<MITD.PMS.Application.JobService>()
                        .BasedOn<IService>().If(c => c.Namespace == typeof(MITD.PMS.Application.JobService).Namespace
                                                     && c.Name != typeof(MITD.PMS.Application.CalculatorEngine).Name
                                                     &&
                                                     c.Name !=
                                                     typeof(MITD.PMS.Application.JobIndexPointCalculator).Name
                                                     &&
                                                     c.Name !=
                                                     typeof(MITD.PMS.Application.CalculationDataProvider).Name
                        )
                        .WithService.FromInterface()
                        .LifestyleBoundToNearest<IService>(),
                    Classes.FromAssemblyContaining<FunctionService>()
                        .BasedOn<IService>().If(c => c.Namespace == typeof(FunctionService).Namespace)
                        .WithService.FromInterface()
                        .LifestyleBoundToNearest<IService>(),
                    Classes.FromAssemblyContaining<MITD.PMS.Domain.Service.RuleBasedPolicyEngineService>()
                        .BasedOn<IService>().OrBasedOn(typeof(IConfigurator))
                        .WithService.FromInterface()
                        .LifestyleBoundToNearest<IService>(),


                    Component.For<IRuleService>().ImplementedBy<RuleEngineService>()
                        .LifestyleBoundToNearest<IService>(),
                    Component.For<IServiceLocatorProvider>().ImplementedBy<LocatorProvider>()
                        .DependsOn(new Hashtable { { "connectionName", "PMSDBConnection" } })
                        .LifeStyle.BoundTo<IService>()
                    ,
                    Component.For<IEventPublisher>().ImplementedBy<EventPublisher>()
                        .LifestyleBoundTo<IService>(),
                    Component.For<ILogManagerService>().ImplementedBy<LogManagerService>()
                        .LifestyleBoundToNearest<IService>(),
                        Component.For<ILogService>().ImplementedBy<LogService>()
                        .LifestyleBoundToNearest<IService>(),
                    Component.For<ILoggerServiceFactory>().ImplementedBy<LoggerServiceFactory>()
                        .LifestyleBoundToNearest<IService>()
                );

            container.Register(
                Component.For<IInquiryJobIndexPointCreatorService>()
                    .ImplementedBy<InquiryJobIndexPointCreatorService>()
                    .LifeStyle.Transient);
            container.Register(
                Component.For<IPMSAdminService>().ImplementedBy<PMSAdminService>().LifestyleBoundTo<IService>());

            container.Register(
                Component.For<IJobPositionInquiryConfiguratorService>()
                    .ImplementedBy<JobPositionInquiryConfiguratorService>()
                    .LifestyleBoundTo<IService>());

            container.Register(
                Component.For<IUnitInquiryConfiguratorService>()
                    .ImplementedBy<UnitInquiryConfiguratorService>()
                    .LifestyleBoundTo<IService>());

            container.Register(
                Component.For<IJobIndexPointCalculatorProvider>()
                    .ImplementedBy<JobIndexPointCalculatorProvider>()
                    .LifeStyle.Singleton.IsDefault());

            container.Register(
                Component.For<IInquiryConfiguratorService>()
                    .ImplementedBy<InquiryConfiguratorService>()
                    .LifeStyle.Singleton.IsDefault());


            container.Register(
                Component.For<IPeriodBasicDataCopierService>()
                    .ImplementedBy<PeriodBasicDataCopierService>()
                    .LifeStyle.Singleton.IsDefault());

            container.Register(
                Component.For<ICalculatorEngine>().ImplementedBy<CalculatorEngine>()
                    .LifeStyle.Transient);


            container.Register(
                Component.For(typeof(IServiceLifeCycleManager<>)).ImplementedBy(typeof(ServiceLifeCycleManager<>))
                    .LifeStyle.Transient);

            container.Register(
                Component.For<ICalculationDataProvider>().ImplementedBy<CalculationDataProvider>()
                    .LifeStyle.Transient);

            container.Register(
                Component.For<IJobIndexPointCalculator>().ImplementedBy<JobIndexPointCalculator>().LifeStyle.Transient);

            container.Register(
                Component.For<IFaultExceptionAdapter>()
                    .ImplementedBy<CalculationFaultExceptionAdapter>()
                    .LifeStyle.Singleton);

            container.Register(
                Component.For<CalculationEngineService>().LifeStyle.PerWcfOperation());

            var locator = new WindsorServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => locator);

            //NHibernateInnerJoinSupport.Enable();
            PMSAdminSession.GetSession();
            PMSSession.GetSession();
            RuleEngineSession.GetSession();

            registerExceptionConvertors();

        }

        private static void registerExceptionConvertors()
        {
            ExceptionConvertorService.RegisterExceptionConvertor(new ArgumentExceptionConvertor());
            ExceptionConvertorService.RegisterExceptionConvertor(new DuplicateExceptionConvertor());
            ExceptionConvertorService.RegisterExceptionConvertor(new ModifyExceptionConvertor());
            ExceptionConvertorService.RegisterExceptionConvertor(new DeleteExceptionConvertor());
            ExceptionConvertorService.RegisterExceptionConvertor(new InvalidStateOperationExceptionConvertor());
            ExceptionConvertorService.RegisterExceptionConvertor(new CompareExceptionConvertor());
        }

        private static void RegisterDataAccess(WindsorContainer container)
        {
            container.Register(Component.For<IConnectionProvider>()
                .UsingFactoryMethod(c => new ConnectionProvider(() =>
                {
                    var s = System.Configuration.ConfigurationManager.
                         ConnectionStrings["PMSDBConnection"].ConnectionString;
                    var res = new SqlConnection(s);
                    res.Open();
                    return res;
                })).LifestyleBoundTo<IService>());

            DataAccessConfigHelper.ConfigureContainer<CustomFieldRepository>(container,
                () =>
                {
                    var session = PMSAdminSession.GetSession(container.Resolve<IConnectionProvider>().GetConnection());
                    return session;
                }, "PMSAdmin");

            DataAccessConfigHelper.ConfigureContainer<PeriodRepository>(container,
                () =>
                {
                    var session = PMSSession.GetSession(container.Resolve<IConnectionProvider>().GetConnection());
                    return session;
                }, "PMS");

            DataAccessConfigHelper.ConfigureContainer<RuleRepository>(container,
                () =>
                {
                    var session = RuleEngineSession.GetSession(container.Resolve<IConnectionProvider>().GetConnection());
                    return session;
                }, "RuleEngine");
        }
    }
}