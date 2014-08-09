using Castle.MicroKernel.Registration;
using Castle.Windsor;
using MITD.Core.Config;
using MITD.Core;
using MITD.Core.RuleEngine;
using MITD.Core.RuleEngine.NH;
using MITD.DataAccess.Config;
using MITD.Domain.Repository;
using MITD.PMS.ACL.PMSAdmin;
using MITD.PMS.Application;
using MITD.PMS.Application.Contracts;
using MITD.PMS.Domain.Model.Calculations;
using MITD.PMS.Domain.Service;
using MITD.PMS.Persistence.NH;
using MITD.PMSAdmin.Application;
using MITD.PMSAdmin.Application.Contracts;
using MITD.PMSAdmin.Domain.Model.CustomFieldTypes;
using MITD.PMSAdmin.Persistence.NH;
using MITD.PMSSecurity.Application;
using MITD.PMSSecurity.Application.Contracts;
using NHibernate;
using MITD.Domain.Model;
using System.Collections;
using MITD.Data.NH;
using System;
using Castle.MicroKernel.Releasers;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Domain.Model.JobIndexPoints;
using MITD.PMSAdmin.Domain.Model.Policies;

namespace MITD.PMS.Test
{
    public static class LocatorCreator
    {
        public static void Execute()
        {

            ConfigeLocator();
        }

        public static void ConfigeLocator()
        {
            var container = new WindsorContainer();
            //container.Kernel.ReleasePolicy = new NoTrackingReleasePolicy();


            RegisterDataAccess(container);

            container.Register
                (
                    Classes.FromAssemblyContaining<MITD.PMS.Application.JobService>()
                        .BasedOn<IService>().If(c => c.Namespace == typeof (MITD.PMS.Application.JobService).Namespace
                                                     && c.Name != typeof (MITD.PMS.Application.CalculatorEngine).Name
                                                     &&
                                                     c.Name !=
                                                     typeof (MITD.PMS.Application.JobIndexPointCalculator).Name)
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
                Component.For<IJobIndexPointCalculatorProvider>()
                    .ImplementedBy<JobIndexPointCalculatorProvider>()
                    .LifeStyle.Singleton.IsDefault());

            container.Register(
              Component.For<IInquiryConfiguratorService>()
                  .ImplementedBy<InquiryConfiguratorService>()
                  .LifeStyle.Singleton.IsDefault());

            container.Register(
                Component.For<ICalculatorEngine>().ImplementedBy<CalculatorEngine>()
                    .LifeStyle.Transient);

            container.Register(
                Component.For<IJobIndexPointCalculator>().ImplementedBy<JobIndexPointCalculator>().LifeStyle.Transient);


            var locator = new WindsorServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => locator);
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