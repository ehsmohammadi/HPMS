using System;
using System.Collections.Generic;
using Castle.Components.DictionaryAdapter.Xml;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using MITD.Core.Config;
using MITD.Core;
using MITD.Core.RuleEngine;
using MITD.Core.RuleEngine.NH;
using MITD.DataAccess.Config;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using MITD.PMS.ACL.PMSAdmin;
using MITD.PMS.ACL.PMSSecurity;
using MITD.PMS.Application;
using MITD.PMS.Application.Contracts;
using MITD.PMS.Calculation.Contracts;
using MITD.PMS.Domain.Service;
using MITD.PMS.Persistence.NH;
using MITD.PMS.Interface;
using MITD.PMS.Presentation.Contracts;
using MITD.PMSAdmin.Application;
using MITD.PMSAdmin.Persistence.NH;
using MITD.PMSSecurity.Application;
using MITD.PMSSecurity.Application.Contracts;
using MITD.Domain.Model;
using System.Collections;
using MITD.Data.NH;
using System.Linq;
using System.Data.SqlClient;
using System.Web.Http.Dispatcher;
using MITD.PMSSecurity.Domain;
using MITD.PMSSecurity.Domain.Model.AccessPermissions;
using MITD.PMSSecurity.Persistence.NH;
using MITD.PMSSecurity.Domain.Service;
using Castle.Core;
using Castle.MicroKernel.ModelBuilder.Inspectors;
using MITD.PMS.ACL.SSO;
using MITD.PMS.ACL.CalculationEngine;
using MITD.Services;
using NHibernate.Linq.InnerJoinFetch;

namespace MITD.PMS.Service.Host
{
    public static class BootStrapper
    {
        public static void Execute()
        {
            

            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalConfiguration.Configuration.Filters.Add(new ModelValidationFilterAttribute());

            ConfigeLocator();

            GlobalConfiguration.Configuration.Services.Replace(
                typeof(IHttpControllerActivator),
                new IocControllerActivator());
        }

        public static void ConfigeLocator()
        {
            var container = new WindsorContainer();
            container.Kernel.ComponentModelBuilder.RemoveContributor(
                container.Kernel.ComponentModelBuilder.Contributors.OfType<PropertiesDependenciesModelInspector>().Single());
            container.Register(
  Component.For<IFacadeService>().Interceptors(InterceptorReference.ForType<Interception>()).Last,
  Component.For<Interception>());

            RegisterDataAccess(container);




            container.Register(
                Classes.FromAssemblyContaining<CalculationMapper>()
                    .BasedOn<IMapper>()
                    .WithService.FromInterface()
                    .LifestyleBoundToNearest<IService>(),
                Classes.FromAssemblyContaining<CalculationServiceFacade>()
                    .BasedOn<IFacadeService>()
                    .WithService.FromInterface()
                    .LifestyleBoundTo<ApiController>(),
                Classes.FromThisAssembly().BasedOn<ApiController>().LifestyleTransient(),
                Classes.FromThisAssembly().BasedOn<Controller>().LifestyleTransient()
                );

            //container.Register(Component.For<IFacadeService>()
            //    .Interceptors(InterceptorReference.ForType<Interception>()).Anywhere,
            //    Component.For<Interception>()
            //    );

          

            container.Register(
                Component.For<ILoggerService>().ImplementedBy<DbLoggerService>().Named("DB").LifeStyle.BoundTo<IService>(),
                Component.For<ILoggerService>().ImplementedBy<FileLoggerService>().Named("File").LifeStyle.BoundTo<IService>(),
                Component.For<ILoggerService>().ImplementedBy<WindowsEventsLoggerService>().Named("WindowsEvent").LifeStyle.BoundTo<IService>()
                );
           

            container.Register
                (
                    Classes.FromAssemblyContaining<MITD.PMS.Application.JobService>()
                        .BasedOn<IService>().If(c => c.Namespace == typeof(MITD.PMS.Application.JobService).Namespace
                                                     && c.Name != typeof(MITD.PMS.Application.CalculatorEngine).Name
                                                     &&
                                                     c.Name != typeof(MITD.PMS.Application.JobIndexPointCalculator).Name
                                                     &&
                                                     c.Name != typeof(MITD.PMS.Application.CalculationDataProvider).Name
                                                     )
                        .WithService.FromInterface()
                        .LifestyleBoundToNearest<IService>(),
                    Classes.FromAssemblyContaining<FunctionService>()
                        .BasedOn<IService>().If(c => c.Namespace == typeof (FunctionService).Namespace)
                        .WithService.FromInterface()
                        .LifestyleBoundToNearest<IService>(),
                    Classes.FromAssemblyContaining<MITD.PMS.Domain.Service.RuleBasedPolicyEngineService>()
                        .BasedOn<IService>().OrBasedOn(typeof (IConfigurator))
                        .WithService.FromInterface()
                        .LifestyleBoundToNearest<IService>(),

                    
                    Component.For<IRuleService>().ImplementedBy<RuleEngineService>()
                        .LifestyleBoundToNearest<IService>(),
                    Component.For<IServiceLocatorProvider>().ImplementedBy<LocatorProvider>()
                        .DependsOn(new Hashtable {{"connectionName", "PMSDBConnection"}})
                        .LifeStyle.BoundTo<IService>(),
                    Component.For<IEventPublisher>().ImplementedBy<EventPublisher>()
                        .LifestyleBoundTo<IService>(),
                    Component.For<ISecurityService>().ImplementedBy<SecurityService>()
                        .LifestyleBoundToNearest<IService>(),
                    Component.For<ISecurityCheckerService>().ImplementedBy<SecurityCheckerService>()
                        .LifestyleBoundToNearest<IService>(),
                    Component.For<ILogService>().ImplementedBy<LogService>()
                        .LifestyleBoundToNearest<IService>(),
                    Component.For<ILogManagerService>().ImplementedBy<LogManagerService>()
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
                Component.For<IPMSSecurityService>().ImplementedBy<PMSSecurityService>().LifestyleBoundTo<IService>());

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

            //container.Register(
            //    Component.For<IEmployeePointCopierService>()
            //        .ImplementedBy<EmployeePointCopierService>()
            //        .LifestyleBoundToNearest<IService>());

            container.Register(
                Component.For<ICalculatorEngine>().ImplementedBy<CalculatorEngine>()
                    .LifeStyle.Transient);

            container.Register(
                Component.For<ISecurityServiceFacade>().ImplementedBy<SecurityServiceFacade>()
                    .LifeStyle.Transient);

            container.Register(
               Component.For(typeof(IServiceLifeCycleManager<>)).ImplementedBy(typeof(ServiceLifeCycleManager<>))
                   .LifeStyle.Transient);

            //container.Register(
            //    Component.For<IPeriodServiceFactory>().ImplementedBy<PeriodServiceFactory>()
            //        .LifeStyle.Transient);

            container.Register(
                Component.For<ICalculationDataProvider>().ImplementedBy<CalculationDataProvider>()
                    .LifeStyle.Transient);

            container.Register(
                Component.For<IJobIndexPointCalculator>().ImplementedBy<JobIndexPointCalculator>().LifeStyle.Transient);

            //container.Register(
            //    Component.For<Castle.DynamicProxy.IInterceptor>().ImplementedBy<Interception>().LifestyleBoundTo<IService>());

            container.Register(
                Component.For<IUserManagementService>().ImplementedBy<UserManagementService>().LifeStyle.Singleton);

            container.Register(
                Component.For<ICalculationEngineService>().ImplementedBy<CalculationEngineService>().LifestyleBoundTo<IService>());

            container.Register(
               Component.For<IPeriodEngineService>().ImplementedBy<PeriodEngineService>().LifestyleBoundTo<IService>());

            container.Register(
                Component.For<IFaultExceptionAdapter>().ImplementedBy<CalculationFaultExceptionAdapter>().LifeStyle.Singleton);

            container.Register(
            Component.For<AccessPermission>().LifeStyle.Singleton);


            var locator = new WindsorServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => locator);
            

            //var accessPermissionSetup=new AccessPermissionSetup();
            //accessPermissionSetup.Execute(ServiceLocator.Current.GetInstance<AccessPermission>(),
            //    container.Kernel.GetAssignableHandlers(typeof (IFacadeService)).Count());

         


            NHibernateInnerJoinSupport.Enable();
            PMSAdminSession.GetSession();
            PMSSession.GetSession();
            PMSSecuritySession.GetSession();
            RuleEngineSession.GetSession();
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

            DataAccessConfigHelper.ConfigureContainer<UserRepository>(container,
              () =>
              {
                  var session = PMSSecuritySession.GetSession(container.Resolve<IConnectionProvider>().GetConnection());
                  return session;
              }, "PMSSecurity");

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
    }
}