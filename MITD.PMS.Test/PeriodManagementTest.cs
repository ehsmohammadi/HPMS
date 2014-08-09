using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MITD.Core;
using MITD.Data.NH;
using MITD.PMS.Application;
using MITD.PMS.Application.Contracts;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Domain.Service;
using MITD.PMS.Persistence.NH;
using MITD.PMS.ACL.PMSAdmin;
using MITD.PMSAdmin.Persistence.NH;
using JobIndexRepository = MITD.PMS.Persistence.NH.JobIndexRepository;
using JobPositionRepository = MITD.PMS.Persistence.NH.JobPositionRepository;
using JobRepository = MITD.PMS.Persistence.NH.JobRepository;
using UnitRepository = MITD.PMS.Persistence.NH.UnitRepository;

namespace MITD.PMS.Test
{
    [TestClass]
    public class PeriodManagementTest
    {
        private AutoResetEvent trigger = new AutoResetEvent(false);
        private void initialized()
        {
            using (var uow = new NHUnitOfWork(PMSSession.GetSession()))
            {

                var periodRep = new PeriodRepository(uow);
                for (int i = 0; i < 5; i++)
                {
                    var period = new Period(new PeriodId(periodRep.GetNextId()), "Test", DateTime.Now, DateTime.Now);
                    periodRep.Add(period);

                }
                uow.Commit();

            }
        }

        [TestMethod]
        public void PeriodActivateTest()
        {
            //initialized();
            //using (var uow = new NHUnitOfWork(PMSSession.GetSession()))
            //{
            //    using (var uowPMSAdmin = new NHUnitOfWork(PMSAdminSession.GetSession()))
            //    {

            //        var pmsAdminService = new PMS.ACL.PMSAdmin.PMSAdminService(
            //           new PMSAdmin.Application.UnitService(new PMSAdmin.Persistence.NH.UnitRepository(uowPMSAdmin)),
            //           new PMSAdmin.Application.JobService(new PMSAdmin.Persistence.NH.JobRepository(uowPMSAdmin),
            //               new PMSAdmin.Persistence.NH.CustomFieldRepository(uowPMSAdmin)),
            //           new PMSAdmin.Application.CustomFieldService(new PMSAdmin.Persistence.NH.CustomFieldRepository(uowPMSAdmin)),
            //           new PMSAdmin.Application.JobPositionService(new PMSAdmin.Persistence.NH.JobPositionRepository(uowPMSAdmin)),
            //           new PMSAdmin.Application.JobIndexService(new PMSAdmin.Persistence.NH.JobIndexRepository(uowPMSAdmin),
            //               new PMSAdmin.Persistence.NH.CustomFieldRepository(uowPMSAdmin))
            //           );

            //        var periodRep = new PeriodRepository(uow);
            //        var jobPositionRep = new JobPositionRepository(uow);
            //        var jobRep = new JobRepository(uow);
            //        var employeeRep = new EmployeeRepository(uow);
            //        var unitRep = new UnitRepository(uow);
            //        var jobIndexRep = new JobIndexRepository(uow);
            //        var inquiryJobIndexPointRep = new InquiryJobIndexPointRepository(uow);

            //        var inquiryConfigurator = new InquiryConfiguratorService(jobPositionRep);
            //        var inquiryJobIndexPointService = new InquiryJobIndexPointService(inquiryJobIndexPointRep);
            //        var jobPositionService = new JobPositionService(jobPositionRep, periodRep, jobRep, unitRep,
            //            pmsAdminService, inquiryConfigurator, employeeRep);
            //        var inquiryService = new InquiryService(inquiryConfigurator, employeeRep, inquiryJobIndexPointRep,
            //            jobPositionRep, jobRep, jobIndexRep, inquiryJobIndexPointService);
            //        var jobPositionFactory = new JobPositionServiceFactory();
            //        var inquiryServiceFactory = new InquiryServiceFactory();
            //        var periodManagerService = new PeriodManagerService(periodRep, jobPositionService, inquiryService,
            //            jobPositionFactory, inquiryServiceFactory);
            //        var periodService = new PeriodService(periodRep, periodManagerService);
            //        var period = periodService.AddPeriod("Test", DateTime.Now, DateTime.Now);
            //        periodService.Activate(period.Id);

            //        // uow.Commit();

            //    }

            //}

        }

        [TestMethod]
        public void PeriodStartInquiryTest()
        {
            LocatorCreator.Execute();
            //initialized();
            var periodService = ServiceLocator.Current.GetInstance<IPeriodService>();
            var period = periodService.GetCurrentPeriod();
            periodService.InitializeInquiry(period.Id);
            ServiceLocator.Current.Release(periodService);

            var timer = new Timer(res =>
            {
                periodService = ServiceLocator.Current.GetInstance<IPeriodService>();
                var state = periodService.GetIntializeInquiryState(period.Id);
                if(state.Percent==100)
                    trigger.Set();
                ServiceLocator.Current.Release(periodService);

            }, trigger, 5000, 30000);
            trigger.WaitOne();
            timer.Dispose();
            periodService = ServiceLocator.Current.GetInstance<IPeriodService>();
            periodService.StartInquiry(period.Id);


        }

    }
}
