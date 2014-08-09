using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using MITD.Data.NH;
using MITD.Domain.Repository;
using MITD.PMS.Domain.Model.Calculations;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Domain.Model.JobIndexPoints;
using MITD.PMS.Domain.Model.JobIndices;
using MITD.PMSAdmin.Domain.Model.CustomFieldTypes;
using MITD.PMS.Domain.Model.JobPositions;
using MITD.PMS.Domain.Model.Units;
using MITD.PMSAdmin.Domain.Model.JobIndices;
using MITD.PMS.Domain.Model.Jobs;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Persistence.NH;
using MITD.PMSAdmin.Persistence.NH;
using MITD.PMSSecurity.Domain;
using MITD.PMSSecurity.Domain.Logs;
using MITD.PMSSecurity.Persistence.NH;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NHibernate.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using AbstractJobIndexId = MITD.PMS.Domain.Model.JobIndices.AbstractJobIndexId;

namespace MITD.PMS.Persistence.Test
{
    [TestClass]
    public class EmployeeTest
    {

        [TestMethod]
        public void AddUpdateEmployeeTest()
        {
            using (var session=PMSSession.GetSession())
            using (session.BeginTransaction())
            {
                var period = session.Query<Period>().First();
                var employee = new Employee(Guid.NewGuid().ToString(), period,
                    Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
                SharedEmployeeCustomField sharedEmployeeCustomField;
                using (var adminSession=PMSAdminSession.GetSession())
                using (adminSession.BeginTransaction())
                {
                    var employeeCustomFielType = adminSession.Query<CustomFieldType>().First();
                    sharedEmployeeCustomField =
                        new SharedEmployeeCustomField(new SharedEmployeeCustomFieldId(employeeCustomFielType.Id.Id),
                            Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), 1, 1);
                }
                employee.AssignCustomFieldAndValue(sharedEmployeeCustomField,"30");
                session.Save(employee);
                session.Transaction.Commit();
                using (session.BeginTransaction())
                {
                    var rep = new EmployeeRepository(new NHUnitOfWork(session));
                    var fs = new ListFetchStrategy<Employee>(Enums.FetchInUnitOfWorkOption.NoTracking);
                    fs.WithPaging(10, 1);
                    fs.OrderBy(c => c.Id);
                    rep.Find(e=>e.Id.PeriodId==new PeriodId(1),fs);
                    var newEmployee =
                    session.Query<Employee>()
                        .FirstOrDefault(
                            e => e.Id.EmployeeNo == employee.Id.EmployeeNo && e.Id.PeriodId == employee.Id.PeriodId);
                    newEmployee.Update("ehsan", "mohammadi");
                    session.Update(newEmployee);
                    session.Transaction.Commit();
                }

            }
            
        }

        [TestMethod]
        public void LogTest()
        {
            using (var session = PMSSecuritySession.GetSession())
            using (session.BeginTransaction())
            {
                LogRepository logRep = new LogRepository(new NHUnitOfWork(session));

                var user = session.Get<User>(1);
                var gid = Guid.NewGuid();
                Log log = new EventLog(new LogId(gid), "diddd", LogLevel.Information, user, "clll", "mett",  "ttttt", "mmmmmmm");
                
                logRep.Add(log);
                //session.Save(log);

                var l = session.Get<Log>(22);
                session.Transaction.Commit();
            }
        }

        [TestMethod]
        public void CalculationWithExceptionTest()
        {
            using (var session=PMSSession.GetSession())
            using (session.BeginTransaction())
            {
                //var calc = session.Query<Calculation>().Where(c => c.Id.Id == 10).FirstOrDefault();
                //var emp = session.Query<Employee>().FirstOrDefault();
                //List<EmployeeCalculationException> expList = new List<EmployeeCalculationException>();
                //var exp = new EmployeeCalculationException(calc, emp.Id, 0, "dddddd");
                //expList.Add(exp);
                //calc.UpdateCalculationException(expList);
                //session.Transaction.Commit();
            }

             using (var session=PMSSession.GetSession())
             using (session.BeginTransaction())
             {
                 var calc = session.Query<Calculation>().Where(c => c.Id.Id == 10).FirstOrDefault();
                
             }
        }

        [TestMethod]
        public void EmployeesInJobIndexLevelTest()
        {
            int firstLevel = 1;
            var periodId = new PeriodId(10);
            var enList = new string[] { "2000", "4000", "8000", "12000", "16000" };
            var res = new Dictionary<int, IList<EmployeeId>>();
            var calculationId = new CalculationId(10);

            using (var session = PMSSession.GetSession())
            using (session.BeginTransaction())
            {
                var calculationExpRep = new CalculationExceptionRepository(new NHUnitOfWork(session));

                var employeeJobs = (from jo in session.Query<Job>()
                                    join i in session.Query<JobPositionEmployee>()
                                        on jo.Id equals i.JobPosition.JobId
                                    where enList.Contains(i.EmployeeId.EmployeeNo) && i.EmployeeId.PeriodId == periodId
                                    select new { jo, i.EmployeeId });//.Distinct();


                IEnumerable<EmployeeId> empCompletedInFirstLevel = new List<EmployeeId>();
                Calculation calculation = null;
                if (calculationId != null)
                {
                    calculation = session.Query<Calculation>().Where(c => c.Id == calculationId).SingleOrDefault();
                    if (calculation.CalculationResult != null && calculation.CalculationResult.LastCalculatedPath.HasValue)
                        firstLevel = calculation.CalculationResult.LastCalculatedPath.Value;
                }

                var dummy = employeeJobs.Select(empJob => empJob.jo).Fetch(j => j.JobIndexIdList).ToFuture();
                var employeeJobindexes = session.Query<Domain.Model.JobIndices.JobIndex>()
                    .Where(j => employeeJobs.Select(empJob => empJob.jo).SelectMany(jo => jo.JobIndexIdList).Select(ji => ji.Id).Contains(j.Id.Id))
                   .ToFuture();

                var jobIndexLevels = employeeJobindexes.Where(ji=>ji.CalculationLevel>= firstLevel).Select(ji => ji.CalculationLevel).Distinct().ToList();

                //var empJobInexIdsLevel1 = employeeJobindexes.Where(ji=>jei.CalculationLevel ==1).Select(ji => ji.Id).ToList();
                //var empLevel1 = employeeJobs.Where(empj => empj.jo.JobIndexIdList.Any(i => empJobInexIdsLevel1.Contains(i))).Select(empj => empj.EmployeeId).ToList();

                
                for (int level = firstLevel; level <= jobIndexLevels.Max(); level++)
                {
                    var empJobInexIdsLevel = employeeJobindexes.Where(ji => ji.CalculationLevel == level).Select(ji => ji.Id).ToList();
                    var empLevel = employeeJobs.Where(empj => empj.jo.JobIndexIdList.Any(i => empJobInexIdsLevel.Contains(i)))
                                    .Select(empj => empj.EmployeeId).OrderBy(emp => emp.EmployeeNo).ToList();

                    if (level == firstLevel)
                    {
                        if (calculation.CalculationResult != null && calculation.CalculationResult.LastCalculatedEmployeeId != null)
                        {
                            var enNotCompletedInFirstLevel = enList.Skip(
                                enList.ToList().FindLastIndex(s => s == calculation.CalculationResult.LastCalculatedEmployeeId.EmployeeNo) + 1);
                            
                            
                            //var expEmployeeIds = calculation.EmployeeCalculationExceptions.Select(exp => exp.EmployeeId).ToList();
                            var expEmployeeIds = calculationExpRep.GetAllBy(calculationId).Select(exp => exp.EmployeeId).ToList();
                            enNotCompletedInFirstLevel = enNotCompletedInFirstLevel.Union(expEmployeeIds.Select(exp => exp.EmployeeNo));
                            empLevel = empLevel.Where(emp => enNotCompletedInFirstLevel.Contains(emp.EmployeeNo)).ToList();
                        }

                    }
                    res.Add(level, empLevel);
                }


            }
            
        }

        [TestMethod]
        public void AssignJobPositionToEmployee()
        {
            using (var session = PMSSession.GetSession())
            using (session.BeginTransaction())
            {
                var period = session.Query<Period>().First();
                var employee = new Employee(Guid.NewGuid().ToString(), period,
                    Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
                SharedEmployeeCustomField sharedEmployeeCustomField;
                JobCustomField sharedJobCustomField;
                using (var adminSession = PMSAdminSession.GetSession())
                using (adminSession.BeginTransaction())
                {
                    var employeeCustomFielType = adminSession.Query<CustomFieldType>().First();
                    sharedEmployeeCustomField =
                        new SharedEmployeeCustomField(new SharedEmployeeCustomFieldId(employeeCustomFielType.Id.Id),
                            Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), 1, 1);
                    //sharedJobCustomField =
                    //    new JobCustomField(new JobCustomFieldId(employeeCustomFielType.Id.Id),
                    //        Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), 1, 1);
                }
                employee.AssignCustomFieldAndValue(sharedEmployeeCustomField, "30");
                var jobposition = session.Query<JobPosition>().FirstOrDefault();
                //employee.AssignJobPosition(jobposition, DateTime.Now.Date, DateTime.Now.Date,
                //    new Dictionary<JobCustomField, string> {{sharedJobCustomField, "10"}});
                session.Save(employee);
                session.Transaction.Commit();
                using (session.BeginTransaction())
                {
                    var newEmployee =
                    session.Query<Employee>()
                        .FirstOrDefault(
                            e => e.Id.EmployeeNo == employee.Id.EmployeeNo && e.Id.PeriodId == employee.Id.PeriodId);
                    //newEmployee.AssignJobPositions(null, );
                    session.Update(newEmployee);
                    session.Transaction.Commit();
                }



            }


        }

        [TestMethod]
        public void UpdateJobEmployeeCustomFieldValue()
        {

        }
        



    }
}
