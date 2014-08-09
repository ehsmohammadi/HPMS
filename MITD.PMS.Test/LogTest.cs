using System;
using System.Data.SqlClient;
using Castle.MicroKernel.Registration;
using MITD.PMS.Application;
using MITD.PMS.Domain.Model.JobIndices;
using MITD.PMS.Domain.Model.Jobs;
using MITD.PMSSecurity.Application;
using MITD.PMSSecurity.Domain;
using MITD.PMSSecurity.Domain.Logs;
using MITD.PMSSecurity.Domain.Service;
using MITD.PMSSecurity.Persistence.NH;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MITD.PMS.Domain.Model.Calculations;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Domain.Model.Policies;
using MITD.PMS.Domain.Service;
using MITD.Data.NH;
using MITD.PMS.Persistence.NH;
using MITD.Core.RuleEngine.NH;
using MITD.PMS.Domain.Model.Employees;
using System.Collections.Generic;
using MITD.Core.RuleEngine;
using MITD.Core;
using Castle.Windsor;
using MITD.Domain.Repository;
using MITD.DataAccess.Config;
using MITD.Core.Config;
using MITD.Core.RuleEngine.Model;
using MITD.PMSAdmin.Persistence.NH;
using System.Transactions;
using System.Threading.Tasks;
using System.Threading;
using MITD.PMSAdmin.Domain.Model.CustomFieldTypes;
using NHibernate.Linq;
using NHibernate;
using System.Linq;
using MITD.PMS.Domain.Model.JobIndexPoints;

namespace MITD.PMS.Test
{
    [TestClass]
	public class LogTest 
	{
		EventPublisher publisher = new EventPublisher();

        [TestMethod]
        public void LogServiceTest()
        {
            //LocatorCreator.Execute();
            


            var container = new WindsorContainer();
            container.Register(Component.For<ILoggerService>().ImplementedBy<DbLoggerService>().LifeStyle.BoundTo<IService>());
            //container.Register(Component.For<ILogRepository>().ImplementedBy<LogRepository>().LifeStyle.Transient);

            container.Register(Component.For<IConnectionProvider>()
               .UsingFactoryMethod(c => new ConnectionProvider(() =>
               {
                   var s = System.Configuration.ConfigurationManager.
                        ConnectionStrings["PMSDBConnection"].ConnectionString;
                   var res = new SqlConnection(s);
                   res.Open();
                   return res;
               })).LifestyleBoundTo<IService>());

            DataAccessConfigHelper.ConfigureContainer<UserRepository>(container,
              () =>
              {
                  var session = PMSSecuritySession.GetSession(container.Resolve<IConnectionProvider>().GetConnection());
                  return session;
              }, "PMSSecurity");

            var locator = new WindsorServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => locator);

            //var userRep = ServiceLocator.Current.GetInstance<UserRepository>();


            var uows = new MITD.Domain.Repository.UnitOfWorkScope(
              new Data.NH.NHUnitOfWorkFactory(() => PMSSecurity.Persistence.NH.PMSSecuritySession.GetSession()));



            using (var uow = new NHUnitOfWork(PMSSession.GetSession()))
            using (var uow2 = uows.CurrentUnitOfWork)
            {

                var logFactory = new LoggerServiceFactory();
                var logManager = new LogManagerService(logFactory);
                var logService = new LogService(logManager);

                var gid = Guid.NewGuid();
                Log log = new EventLog(new LogId(gid), "diddd", LogLevel.Information, null, "clll", "mett",  "ttttt", "mmmmmmm");

                logService.AddEventLog(log.Code,log.LogLevel,null,log.ClassName,log.MethodName,log.Title,log.Messages);

            }
        }

        

	}
}
