using System;
using System.Collections.Generic;
using MITD.Data.NH;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Domain.Model.InquiryJobIndexPoints;
using MITD.PMS.Domain.Model.JobIndices;
using MITD.PMS.Domain.Service;
using MITD.PMS.Domain.Model.JobPositions;
using MITD.PMS.Domain.Model.Units;
using MITD.PMS.Domain.Model.Jobs;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Persistence.NH;
using MITD.PMSAdmin.Domain.Model.JobIndices;
using MITD.PMSAdmin.Domain.Model.Units;
using MITD.PMSAdmin.Persistence.NH;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate.Linq;
using System.Linq;
using JobIndex = MITD.PMS.Domain.Model.JobIndices.JobIndex;
using pmsAdminModel = MITD.PMSAdmin.Domain.Model;
using Unit = MITD.PMS.Domain.Model.Units.Unit;
using UnitRepository = MITD.PMSAdmin.Persistence.NH.UnitRepository;

namespace MITD.PMS.Persistence.Test
{
    [TestClass]
    public class InquiryTest
    {



        #region Private Helper Methods PMS Admin
        private SharedJobPosition createSharedJobPosition()
        {
            pmsAdminModel.JobPositions.JobPosition jobPosition;
            using (var session = PMSAdminSession.GetSession())
            using (session.BeginTransaction())
            {
                var jobPositionRep = new MITD.PMSAdmin.Persistence.NH.JobPositionRepository(new NHUnitOfWork(session));
                var id = jobPositionRep.GetNextId();
                jobPosition = new PMSAdmin.Domain.Model.JobPositions.JobPosition(id, Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
                session.Save(jobPosition);
                session.Transaction.Commit();
            }
            return new SharedJobPosition(new SharedJobPositionId(jobPosition.Id.Id), jobPosition.Name, jobPosition.DictionaryName);
        }

        private SharedJob GetSharedJob()
        {
            pmsAdminModel.Jobs.Job job;
            using (var session = PMSAdminSession.GetSession())
            using (session.BeginTransaction())
            {
                var jobRep = new PMSAdmin.Persistence.NH.JobRepository(new NHUnitOfWork(session));
                var id = jobRep.GetNextId();
                job = new PMSAdmin.Domain.Model.Jobs.Job(id, Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
                session.Save(job);
                session.Transaction.Commit();
            }
            return new SharedJob(new SharedJobId(job.Id.Id), job.Name, job.DictionaryName);

        }

        private SharedUnit GetSharedUnit()
        {
            pmsAdminModel.Units.Unit unit;
            using (var session = PMSAdminSession.GetSession())
            using (session.BeginTransaction())
            {
                var unitRep = new UnitRepository(new NHUnitOfWork(session));
                var id = unitRep.GetNextId();
                unit = new PMSAdmin.Domain.Model.Units.Unit(id, Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
                session.Save(unit);
                session.Transaction.Commit();
            }
            return new SharedUnit(new SharedUnitId(unit.Id.Id), unit.Name, unit.DictionaryName);
        }

        private SharedJobIndex getSharedJobIndex()
        {
            pmsAdminModel.JobIndices.JobIndex jobIndex;
            using (var session = PMSAdminSession.GetSession())
            using (session.BeginTransaction())
            {
                var jobIndexRep = new PMSAdmin.Persistence.NH.JobIndexRepository(new NHUnitOfWork(session));
                var id = jobIndexRep.GetNextId();
                jobIndex = new PMSAdmin.Domain.Model.JobIndices.JobIndex(id,
                    jobIndexRep.GetJobIndexCategory(new PMSAdmin.Domain.Model.JobIndices.AbstractJobIndexId(1)),
                    Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
                session.Save(jobIndex);
                session.Transaction.Commit();
            }
            return new SharedJobIndex(new SharedJobIndexId(jobIndex.Id.Id), jobIndex.Name, jobIndex.DictionaryName);
        }

        #endregion

        #region Private Helper Methods Pms

        private JobIndex createJobIndexInPeriod(Period period)
        {
            JobIndex jobIndex = null;
            using (var session = PMSSession.GetSession())
            using (session.BeginTransaction())
            {
                var jiRep = new PMS.Persistence.NH.JobIndexRepository(new NHUnitOfWork(session));
                var id = jiRep.GetNextId();
                jobIndex = new JobIndex(id, period, getSharedJobIndex(),jiRep.GetJobIndexGroupById(new Domain.Model.JobIndices.AbstractJobIndexId(1)),true);
                session.Save(jobIndex);
                session.Transaction.Commit();
            }
            return jobIndex;
        }
        [TestMethod]
        public void isFullInquiryForms()//Period period)
        {
            JobIndex jobIndex = null;
            using (var session = PMSSession.GetSession())
            using (session.BeginTransaction())
            {
                //var jiRep = new PMS.Persistence.NH.JobIndexRepository(new NHUnitOfWork(session));
                var jiRep = new PMS.Persistence.NH.InquiryJobIndexPointRepository(new NHUnitOfWork(session));
                var pRep = new PMS.Persistence.NH.PeriodRepository(new NHUnitOfWork(session));
                var period = pRep.GetAll().First();
                var has = jiRep.IsAllInquiryJobIndexPointsHasValue(period);
                //var id = jiRep.GetNextId();
                //jobIndex = new JobIndex(id, period, getSharedJobIndex(),jiRep.GetJobIndexGroupById(new Domain.Model.JobIndices.AbstractJobIndexId(1)),true);
                //session.Save(jobIndex);
                session.Transaction.Commit();
            }
            
        }

        private Job createJobInPeriod(Period period)
        {
            Job job = null;
            var jobIndexInperiodList = new List<JobIndex>();
            for (int i = 0; i < 5; i++)
            {
                var jobIndex = createJobIndexInPeriod(period);
                jobIndexInperiodList.Add(jobIndex);
            }
            using (var session = PMSSession.GetSession())
            using (session.BeginTransaction())
            {
                job = new Job(period, GetSharedJob(), null, jobIndexInperiodList);
                session.Save(job);
                session.Transaction.Commit();
            }
            return job;
        }

        private Unit createUnitInPeriod(Period period)
        {
            Unit unit = null;
            using (var session = PMSSession.GetSession())
            using (session.BeginTransaction())
            {
                unit = new Unit(period, GetSharedUnit(), null);
                session.Save(unit);
                session.Transaction.Commit();
            }
            return unit;
        }

        private IEnumerable<Employee> createEmployees(Period period)
        {
            var empList = new List<Employee>();
            using (var session = PMSSession.GetSession())
            using (session.BeginTransaction())
            {

                for (int i = 0; i < 2; i++)
                {

                    var emp = new Employee(Guid.NewGuid().ToString(), period,
                        Guid.NewGuid().ToString(),
                    Guid.NewGuid().ToString());
                    empList.Add(emp);
                    session.Save(emp);

                }
                session.Transaction.Commit();
            }
            return empList;
        }

        private void assignJobPositionToEmployees(Period period, JobPosition jPosition)
        {
            var empList = createEmployees(period);
            using (var session = PMSSession.GetSession())
            using (session.BeginTransaction())
            {
                var empRep = new EmployeeRepository(new NHUnitOfWork(session));
                var jpRep = new MITD.PMS.Persistence.NH.JobPositionRepository(new NHUnitOfWork(session));
                foreach (var emp in empList)
                {
                    var employee = empRep.GetBy(emp.Id);
                    var jobPosition = jpRep.GetBy(jPosition.Id);
                    employee.AssignJobPosition(jobPosition, DateTime.Now, DateTime.Now,0,1,
                        null);
                    session.Save(employee);
                }
                session.Transaction.Commit();
            }
        }

        private JobPosition createJobPosition(Period period, JobPosition parentPosition)
        {

            JobPosition res = null;
            using (var session = PMSSession.GetSession())
            using (session.BeginTransaction())
            {
                var sharedJp = createSharedJobPosition();
                var unit = createUnitInPeriod(period);
                var job = createJobInPeriod(period);
                res = new JobPosition(period, sharedJp, parentPosition, job, unit);
                session.Save(res);
                session.Transaction.Commit();
            }
            return res;
        }

        private JobPosition createJobPositions()
        {
            JobPosition res = null;
            using (var session = PMSSession.GetSession())
            using (session.BeginTransaction())
            {
                var period = session.Query<Period>().Single();
                var jpRoot = createJobPosition(period, null);
                assignJobPositionToEmployees(period, jpRoot);
                for (int i = 0; i < 5; i++)
                {
                    var jpChild = createJobPosition(period, jpRoot);
                    assignJobPositionToEmployees(period, jpChild);
                    for (var j = 0; j < 3; j++)
                    {
                        var jp = createJobPosition(period, jpChild);
                        assignJobPositionToEmployees(period, jp);
                    }
                    if (i == 2)
                        res = jpChild;
                }



                session.Transaction.Commit();
            }
            return res;
        }



        #endregion


        [TestMethod]
        public void ConfigeJobPositionInquiry()
        {

            using (var session = PMSSession.GetSession())
            using (session.BeginTransaction())
            {
                JobPosition jp = createJobPositions();
                var jpRep = new MITD.PMS.Persistence.NH.JobPositionRepository(new NHUnitOfWork(session));
                var jobPositionForConfig = jpRep.GetBy(jp.Id);
                var configService = new JobPositionInquiryConfiguratorService(new MITD.PMS.Persistence.NH.JobPositionRepository(new NHUnitOfWork(session)));
                jobPositionForConfig.ConfigeInquirer(configService, true);
                session.Save(jobPositionForConfig);
                session.Transaction.Commit();

            }
        }

        [TestMethod]
        public void EmployeeJobInsexPointTest()
        {
            using (var session = PMSSession.GetSession())
            using (session.BeginTransaction())
            {
                var jpRep = new MITD.PMS.Persistence.NH.JobPositionRepository(new NHUnitOfWork(session));
                var empRep = new EmployeeRepository(new NHUnitOfWork(session));
                var inquiryRep = new InquiryJobIndexPointRepository(new NHUnitOfWork(session));
                var jobRep = new PMS.Persistence.NH.JobRepository(new NHUnitOfWork(session));
                var jobIndexRep = new PMS.Persistence.NH.JobIndexRepository(new NHUnitOfWork(session));
                var inquiryService = new JobPositionInquiryConfiguratorService(jpRep);
                var jobPosition = jpRep.GetBy(new JobPositionId(new PeriodId(1), new SharedJobPositionId(19)));
                var inquirerId=jobPosition.ConfigurationItemList.Select(c => c.Id.InquirerId).FirstOrDefault();
                var inquirer = empRep.GetBy(inquirerId);
                var employees = inquiryService.GetJobPositionInquiryConfigurationItemBy(inquirer);
                foreach (var itm in employees)
                {
                    var job=jobRep.GetById(itm.JobPosition.JobId);
                    foreach (var jobIndexId in job.JobIndexIdList)
                    {
                        var jobIndex = jobIndexRep.GetById(jobIndexId);
                        var id = inquiryRep.GetNextId();
                        var inquiryIndexPoint = new InquiryJobIndexPoint(new InquiryJobIndexPointId(id), itm, jobIndex as JobIndex,
                            Guid.NewGuid().ToString());
                        session.Save(inquiryIndexPoint);

                    }                   
                    
                }
                session.Transaction.Commit();
            }


        }

        [TestMethod]
        public void UpdateJobEmployeeCustomFieldValue()
        {

        }




    }
}
