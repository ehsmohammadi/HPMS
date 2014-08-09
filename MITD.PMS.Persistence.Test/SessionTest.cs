using System;
using System.Collections.Generic;
using MITD.PMS.Domain.Model.JobIndices;
using MITD.PMSAdmin.Domain.Model.CustomFieldTypes;
using MITD.PMS.Domain.Model.JobPositions;
using MITD.PMS.Domain.Model.Units;
using MITD.PMSAdmin.Domain.Model.JobIndices;
using MITD.PMS.Domain.Model.Jobs;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Persistence.NH;
using MITD.PMSAdmin.Persistence.NH;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate.Linq;
using System.Linq;
using AbstractJobIndex = MITD.PMS.Domain.Model.JobIndices.AbstractJobIndex;
using AbstractJobIndexId = MITD.PMS.Domain.Model.JobIndices.AbstractJobIndexId;
using JobIndex = MITD.PMS.Domain.Model.JobIndices.JobIndex;
using JobPosition = MITD.PMSAdmin.Domain.Model.JobPositions.JobPosition;
using System.Transactions;
using MITD.PMS.Domain.Model.JobIndexPoints;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Domain.Model.Calculations;
using MITD.PMSAdmin.Domain.Model.Policies;
using MITD.Data.NH;
using MITD.PMSSecurity.Persistence.NH;
using MITD.PMSSecurity.Domain;

namespace MITD.PMS.Persistence.Test
{
    [TestClass]
    public class SessionTest
    {
        [TestMethod]
        public void UserRepTest()
        {
            long uid = 0;
            var uname = Guid.NewGuid().ToString();

            using (var session = PMSSecuritySession.GetSession())
            using (session.BeginTransaction())
            {
                //var gname = Guid.NewGuid().ToString();
                //Group g = new Group(new PartyId(gname), "manahers and admiddjdb");
                //Dictionary<ActionType, bool> gCustActs = new Dictionary<ActionType, bool>();
                //gCustActs.Add(ActionType.AddClaim, true);
                //gCustActs.Add(ActionType.ReplyClaim, false);
                //g.UpdateCustomActions(gCustActs);


                User u = new User(new PartyId(uname), "dssd", "sdfsd", "sdsdj@jjj.ff");
                Dictionary<ActionType, bool> custActs = new Dictionary<ActionType, bool>();
                custActs.Add(ActionType.ManageCalculations, true);
                custActs.Add(ActionType.ManageEmployees, false);

                Group g = session.Get<Group>(26);
                
                u.UpdateCustomActions(custActs);
                u.AssignGroup(g);
                
                session.Save(g);
                session.Save(u);
                
                session.Transaction.Commit();
            }

            using (var session = PMSSecuritySession.GetSession())
            using (session.BeginTransaction())
            {
                //User u = new User(new UserId(uname), "dssd", "sdfsd", "sdsdj@jjj.ff");
                //rep.AddUser(u);
                //var u = session.Get<User>(uname);
                var u2 = session.Get<User>(50);
                session.Transaction.Commit();
            }

        }

        [TestMethod]
        public void EmployeeRepTest()
        {
            using (var uow = new NHUnitOfWork(PMSSession.GetSession()))
            {
                var rep = new EmployeeRepository(uow);
                var x = rep.GetPoints(rep.First(), new CalculationId(10));
            }
        }

        [TestMethod]
        public void EmployeeTest()
        {
            Job emp;
            using (var session = PMSSession.GetSession())
            {
                long id = 98304;
                emp = session.Get<Job>(id);
            }
            var x = emp;
        }
        [TestMethod]
        public void JobIndexInPeriodTest()
        {
            long nextAbsIndexId;
            long jobIndexId;
            using (var session = PMSSession.GetSession())
            using (session.BeginTransaction())
            {
                var period = session.Query<Period>().FirstOrDefault();

                nextAbsIndexId =
                    session.CreateSQLQuery("Select next value for dbo.Periods_AbstractJobIndexSeq").UniqueResult<long>();
                var rootJobIndexCat = new JobIndexGroup(new AbstractJobIndexId(nextAbsIndexId), period, null,
                    Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
                session.Save((AbstractJobIndex) rootJobIndexCat);

                SharedJobIndex sharedJobIndex;
                SharedJobIndexCustomField sharedJobIndexCustomField;

                using (var sessionAdmin = PMSAdminSession.GetSession())
                {
                    var pmsAdminJobIndex =
                        sessionAdmin.Query<MITD.PMSAdmin.Domain.Model.JobIndices.JobIndex>().FirstOrDefault();
                    sharedJobIndex = new SharedJobIndex(new SharedJobIndexId(pmsAdminJobIndex.Id.Id),
                        pmsAdminJobIndex.Name, pmsAdminJobIndex.DictionaryName);

                    var customFld = sessionAdmin.Query<CustomFieldType>().SingleOrDefault();
                    sharedJobIndexCustomField =
                        new SharedJobIndexCustomField(new SharedJobIndexCustomFieldId(customFld.Id.Id), customFld.Name,
                            customFld.DictionaryName, 1, 100);
                }

                nextAbsIndexId =
                    session.CreateSQLQuery("Select next value for dbo.Periods_AbstractJobIndexSeq").UniqueResult<long>();
                var jobIndex = new JobIndex(new AbstractJobIndexId(nextAbsIndexId), period, sharedJobIndex,
                    rootJobIndexCat, true);
                jobIndex.UpdateCustomFields(new Dictionary<SharedJobIndexCustomField, string>()
                {
                    {sharedJobIndexCustomField, "asdasdasdasd"}
                });

                session.Save(jobIndex);
                session.Transaction.Commit();

            }
            using (var session = PMSSession.GetSession())
            using (session.BeginTransaction())
            {
                var jobIndex =
                    session.Query<JobIndex>().SingleOrDefault(j => j.Id == new AbstractJobIndexId(nextAbsIndexId));

                //    var rootJobIndexCat = session.Query<JobIndexCategory>().SingleOrDefault(p => p.Id == new AbstractJobIndexId(nextAbsIndexId));



                //    nextAbsIndexId = session.CreateSQLQuery("Select next value for dbo.AbstractJobIndexSeq").UniqueResult<long>();
                //    var jobIndexCat = new JobIndexCategory(new AbstractJobIndexId(nextAbsIndexId), rootJobIndexCat, Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
                //    session.Save((AbstractJobIndex)jobIndexCat);

                //    jobIndexId = session.CreateSQLQuery("Select next value for dbo.AbstractJobIndexSeq").UniqueResult<long>();
                //    var jobIndex = new JobIndex(new AbstractJobIndexId(jobIndexId), jobIndexCat, Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
                //    session.Save((AbstractJobIndex)jobIndex);

                //    session.Transaction.Commit();
            }
        }


        //[TestMethod]
        //public void JobIndexTest()
        //{
        //    long nextIndexId;
        //    long jobIndexId;
        //    using (var session = PMSAdminSession.GetSession())
        //    using (session.BeginTransaction())
        //    {
        //         nextIndexId =
        //            session.CreateSQLQuery("Select next value for dbo.AbstractJobIndexSeq").UniqueResult<long>();
        //        var rootJobIndexCat = new JobIndexCategory(new AbstractJobIndexId(nextIndexId), null,
        //                                                   Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
        //        session.Save((AbstractJobIndex)rootJobIndexCat);

        //        session.Transaction.Commit();
        //    }
        //    using (var session = PMSAdminSession.GetSession())
        //    using (session.BeginTransaction())
        //    {
        //        var rootJobIndexCat = session.Query<JobIndexCategory>().SingleOrDefault(p => p.Id == new AbstractJobIndexId(nextIndexId));


        //        nextIndexId = session.CreateSQLQuery("Select next value for dbo.AbstractJobIndexSeq").UniqueResult<long>();
        //        var jobIndexCat = new JobIndexCategory(new AbstractJobIndexId(nextIndexId), rootJobIndexCat, Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
        //        session.Save((AbstractJobIndex)jobIndexCat);

        //        jobIndexId = session.CreateSQLQuery("Select next value for dbo.AbstractJobIndexSeq").UniqueResult<long>();
        //        var jobIndex = new JobIndex(new AbstractJobIndexId(jobIndexId), jobIndexCat, Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
        //        session.Save((AbstractJobIndex)jobIndex);

        //        session.Transaction.Commit();
        //    }

        //    //using (var session = PMSAdminSession.GetSession())
        //    //using (session.BeginTransaction())
        //    //{
        //    //    var jobIndexCat = session.Query<JobIndexCategory>().SingleOrDefault(p => p.Id == new AbstractJobIndexId(nextIndexId));

        //    //    var jobIndex = session.Query<AbstractJobIndex>().SingleOrDefault(p => p.Id == new AbstractJobIndexId(jobIndexId));
        //    //    var j = (JobIndex) jobIndex;

        //    //}

        //}


        [TestMethod]
        public void JobPositionInPeriodTest()
        {
            Period period;
            JobPosition jobPosition;

            using (var session = PMSAdminSession.GetSession())
            using (session.BeginTransaction())
            {

                jobPosition = session.Query<JobPosition>().FirstOrDefault();
                session.Transaction.Commit();
            }
            using (var session = PMSSession.GetSession())
            using (session.BeginTransaction())
            {
                period = session.Query<Period>().FirstOrDefault();
                var unitInPeriod = session.Query<Unit>().FirstOrDefault();
                var jobInPeriod = session.Query<Job>().FirstOrDefault();
                var sharedJobPosition = new SharedJobPosition(new SharedJobPositionId(jobPosition.Id.Id),
                    jobPosition.Name,
                    jobPosition.DictionaryName);

                var jobPositionInPeriod = new MITD.PMS.Domain.Model.JobPositions.JobPosition(period, sharedJobPosition,
                    null,
                    jobInPeriod, unitInPeriod);
                session.Save(jobPositionInPeriod);

                session.Transaction.Commit();
            }

        }

        //[TestMethod]
        //public void TestMethod1()
        //{
        //    var sharedJob = PMSAdminTestHelper.AddJob( Guid.NewGuid().ToString() ,Guid.NewGuid().ToString());

        //    using (var session = PMSSession.GetSession() )
        //     using (session.BeginTransaction())
        //     {
        //         var nextPeriodId = session.CreateSQLQuery("Select next value for dbo.PeriodSeq").UniqueResult<long>();
        //         var period = new Period(new PeriodId(nextPeriodId), Guid.NewGuid().ToString(),DateTime.Now,DateTime.Now);
        //         session.Save(period);

        //         var job = new Job(period, sharedJob);
        //         session.Save(job);
        //         session.Transaction.Commit();
        //     }

        //    using (var session = PMSSession.GetSession())
        //    using (session.BeginTransaction())
        //    {
        //        var job = session.Query<Job>().Where(p => p.Id.SharedJobId == sharedJob.Id).Single();

        //    }

        //}


        //[TestMethod]
        //public void TestMethod2()
        //{
        //    var sharedJob = PMSAdminTestHelper.AddJob(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());

        //    using (var session = PMSSession.GetSession())
        //    using (session.BeginTransaction())
        //    {
        //        var nextPeriodId = session.CreateSQLQuery("Select next value for dbo.PeriodSeq").UniqueResult<long>();
        //        var period = new Period(new PeriodId(nextPeriodId), Guid.NewGuid().ToString(),DateTime.Now,DateTime.Now);
        //        session.Save(period);

        //        var job = new Job(period, sharedJob);
        //        session.Save(job);
        //        session.Transaction.Commit();
        //    }

        //    using (var session = PMSSession.GetSession())
        //    using (session.BeginTransaction())
        //    {
        //        var job = session.Query<Job>().Where(p => p.Id.SharedJobId == sharedJob.Id).Single();

        //    }

        //}
        [TestMethod]
        public void JobIndexPointTest()
        { 
            PMSAdmin.Domain.Model.JobIndices.JobIndex jobIndex;
            PMSAdmin.Domain.Model.JobPositions.JobPosition jobPosition;
            PMSAdmin.Domain.Model.Jobs.Job job;
            PMSAdmin.Domain.Model.Units.Unit unit;
            RuleEngineBasedPolicy policy;
            long x = 190;
            using(var tr = new TransactionScope())
            using (var adminSession = PMSAdminSession.GetSession())
            {
                policy = new RuleEngineBasedPolicy(new PolicyId(x),Guid.NewGuid().ToString(),Guid.NewGuid().ToString());
                adminSession.Save(policy);
                var jobIndexCategory = new PMSAdmin.Domain.Model.JobIndices.JobIndexCategory(
                    new PMSAdmin.Domain.Model.JobIndices.AbstractJobIndexId(x+1), null, Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
                adminSession.Save(jobIndexCategory);
                jobIndex = new PMSAdmin.Domain.Model.JobIndices.JobIndex(
                    new PMSAdmin.Domain.Model.JobIndices.AbstractJobIndexId(x+2), jobIndexCategory, Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
                adminSession.Save(jobIndex);

                unit = new PMSAdmin.Domain.Model.Units.Unit(new PMSAdmin.Domain.Model.Units.UnitId(x + 3), Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
                adminSession.Save(unit);
                job = new PMSAdmin.Domain.Model.Jobs.Job(new PMSAdmin.Domain.Model.Jobs.JobId(x + 4), Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
                adminSession.Save(job);
                jobPosition = new JobPosition(new PMSAdmin.Domain.Model.JobPositions.JobPositionId(x + 5)
                    , Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
                adminSession.Save(jobPosition);

                adminSession.Flush();
                tr.Complete();
            }

            long y = x - 4;
            Employee emp;
            using(var tr = new TransactionScope())
            using (var session = PMSSession.GetSession())
            {
                var policy2 = session.Get<MITD.PMS.Domain.Model.Policies.RuleEngineBasedPolicy>(
                    new MITD.PMS.Domain.Model.Policies.PolicyId(policy.Id.Id));
                var period = new Period(new PeriodId(y),Guid.NewGuid().ToString(),DateTime.Now,DateTime.Now);
                
                //period.Activate(null);
                session.Save(period);
                emp = new Employee(Guid.NewGuid().ToString(),period,Guid.NewGuid().ToString(),Guid.NewGuid().ToString());
                session.Save(emp);
                var calc = new Calculation(new CalculationId(y),period,policy2,Guid.NewGuid().ToString(), DateTime.Now,"1");
                session.Save(calc);
                var jobIndexGroup = new JobIndexGroup(new AbstractJobIndexId(y), period,null, Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
                session.Save(jobIndexGroup);
                var ji = new JobIndex(new AbstractJobIndexId(y + 1), period, new SharedJobIndex(
                    new SharedJobIndexId(jobIndex.Id.Id), jobIndex.Name, jobIndex.DictionaryName), jobIndexGroup, true);
                session.Save(ji);
                var j = new Job(period, new SharedJob(new SharedJobId(job.Id.Id), job.Name, job.DictionaryName));
                session.Save(j);
                var u = new Unit(period, new SharedUnit(new SharedUnitId(unit.Id.Id), unit.Name, unit.DictionaryName), null);
                session.Save(u);
                var jp = new Domain.Model.JobPositions.JobPosition(period, new SharedJobPosition(
                    new SharedJobPositionId(jobPosition.Id.Id),jobPosition.Name,jobPosition.DictionaryName),null,j,u);
                session.Save(jp);
                EmployeePoint p = new JobIndexPoint(new CalculationPointId(y),period, emp, calc, jp, ji, "hh", 10);
                session.Save(p);
                p = new SummaryEmployeePoint(new CalculationPointId(y+1),period, emp, calc,"ff", 10);
                session.Save(p);
                session.Flush();
                tr.Complete();
            }
            using(var tr = new TransactionScope())
            using (var session = PMSSession.GetSession())
            {
                var lst = session.Query<EmployeePoint>().ToList();
                session.Flush();
                tr.Complete();
            }

        }
    }
}
