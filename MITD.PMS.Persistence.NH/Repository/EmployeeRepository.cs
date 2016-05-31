using MITD.Data.NH;
using MITD.Domain.Repository;
using MITD.PMS.Common;
using MITD.PMS.Domain.Model.CalculationExceptions;
using MITD.PMS.Domain.Model.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MITD.PMS.Domain.Model.InquiryUnitIndexPoints;
using MITD.PMS.Domain.Model.UnitIndices;
using MITD.PMS.Domain.Model.Units;
using NHibernate.Linq;
using MITD.PMS.Domain.Model.JobIndexPoints;
using MITD.PMS.Domain.Model.Calculations;
using MITD.PMS.Domain.Model.JobPositions;
using MITD.PMSReport.Domain.Model;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Domain.Model.Jobs;
using MITD.PMS.Domain.Model.JobIndices;
using MITD.PMS.Domain.Model.InquiryJobIndexPoints;


namespace MITD.PMS.Persistence.NH
{
    public class EmployeeRepository : NHRepository, IEmployeeRepository
    {
        private NHRepository<Employee> rep;

        public EmployeeRepository(NHUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            init();
        }

        public EmployeeRepository(IUnitOfWorkScope unitOfWorkScope)
            : base(unitOfWorkScope)
        {
            init();
        }

        private void init()
        {
            rep = new NHRepository<Employee>(unitOfWork);
        }

        public IList<Employee> Find(Expression<Func<Employee, bool>> predicate)
        {
            return rep.Find(predicate);
        }

        public Dictionary<int, IList<Employee>> FindInListWithPath(IList<string> enList, PeriodId periodId, int pathNo)
        {
            return FindRemainingEmployeesOfCalculation(enList.ToList(), periodId, null, pathNo);
        }

        public Dictionary<int, IList<Employee>> FindRemainingEmployeesOfCalculation(List<string> enList, PeriodId periodId, CalculationId calculationId, int pathNo)
        {
            int firstLevel = 1;
            var res = new Dictionary<int, IList<EmployeeId>>();


            var employeeJobs = (from jo in session.Query<Job>()
                                join i in session.Query<JobPositionEmployee>()
                                    on jo.Id equals i.JobPosition.JobId
                                where enList.Contains(i.EmployeeId.EmployeeNo) && i.EmployeeId.PeriodId == periodId
                                select new { jo, i.EmployeeId }); //.Distinct();


            IEnumerable<EmployeeId> empCompletedInFirstLevel = new List<EmployeeId>();
            //if (firstLevel > 0)
            if (calculationId != null)
            {
                var calculation = session.Query<Calculation>().Where(c => c.Id == calculationId).SingleOrDefault();
                if (calculation.CalculationResult.LastCalculatedPath.HasValue)
                    firstLevel = calculation.CalculationResult.LastCalculatedPath.Value;
                //Get Completed Employee in this calc path , and finaly Remove from firstLevel
                empCompletedInFirstLevel =
                    session.Query<EmployeePoint>().Where(ep => ep.CalculationId == calculationId
                    //&& ep.CalculatePathNo == firstLevel
                        ).Select(ep => ep.EmployeeId).ToFuture();
            }
            //todo :(LOW) query for data 
            var dummy = employeeJobs.Select(empJob => empJob.jo).Fetch(j => j.JobIndexList).ToFuture();
            var employeeJobindexes = session.Query<Domain.Model.JobIndices.JobIndex>()
                                            .Where(j => employeeJobs.Select(empJob => empJob.jo)
                                                            .SelectMany(jo => jo.JobIndexList)
                                                            .Select(ji => ji.JobIndexId.Id)
                                                            .Contains(j.Id.Id)).ToFuture();

            var jobIndexLevels = employeeJobindexes.Where(ji => ji.CalculationLevel >= firstLevel)
                                  .Select(ji => ji.CalculationLevel).Distinct().ToList();

            //var empJobInexIdsLevel1 = employeeJobindexes.Where(ji=>jei.CalculationLevel ==1).Select(ji => ji.Id).ToList();
            //var empLevel1 = employeeJobs.Where(empj => empj.jo.JobIndexList.Any(i => empJobInexIdsLevel1.Contains(i))).Select(empj => empj.EmployeeId).ToList();

            for (int level = firstLevel; level <= jobIndexLevels.Max(); level++)
            {
                //uncomment this if you want to get employee with its job index level
                //var empJobInexIdsLevel = employeeJobindexes.Where(ji => ji.CalculationLevel == level).Select(ji => ji.Id).ToList();
                var empJobInexIdsLevel = employeeJobindexes.Select(ji => ji.Id).ToList();
                var empLevel = employeeJobs.Where(empj => empj.jo.JobIndexList.Select(j=>j.JobIndexId).Any(i => empJobInexIdsLevel.Contains(i)))
                                .Select(empj => empj.EmployeeId).ToList();

                //if (level == firstLevel)
                //    empLevel = empLevel.Where(emp => !empCompletedInFirstLevel.Contains(emp)).ToList();

                res.Add(level, empLevel);
            }

            return res.ToDictionary(i => i.Key, i => FindInList(i.Value.Select(x => x.EmployeeNo).ToList(), periodId));
        }

        public IList<Employee> FindInList(IList<string> enList, PeriodId periodId)
        {
            var res = new List<Employee>();
            var x = enList.Count() / 1000;
            for (int i = 0; i < x; i++)
            {
                var l = enList.Skip(i * 1000).Take(1000).ToList();
                res.AddRange(rep.Find(e => l.Contains(e.Id.EmployeeNo) && e.Id.PeriodId == periodId));
            }
            var y = enList.Count() % 1000;
            if (y > 0)
            {
                var l = enList.Skip(x * 1000).Take(y).ToList();
                res.AddRange(rep.Find(e => l.Contains(e.Id.EmployeeNo) && e.Id.PeriodId == periodId));
            }
            return res;
        }

        public void Find(Expression<Func<Employee, bool>> predicate, ListFetchStrategy<Employee> fs)
        {
            rep.Find(predicate, fs);
        }

        public Employee First()
        {
            return session.Query<Employee>().FirstOrDefault();
        }

        public void Add(Employee employee)
        {
            rep.Add(employee);
        }

        public long GetNextId()
        {
            return session.CreateSQLQuery("select next value for [dbo].[PeriodSeq]").UniqueResult<long>();
        }

        public Employee GetBy(EmployeeId employeeId)
        {
            return rep.Find(e => e.Id.EmployeeNo == employeeId.EmployeeNo && e.Id.PeriodId == employeeId.PeriodId)
                .SingleOrDefault();
        }

        public void Delete(Employee employee)
        {
            rep.Delete(employee);
        }

        public Dictionary<int, IList<Employee>> FindRemainingEmployeesOfCalculation(List<string> enList, PeriodId periodId, CalculationId calculationId)
        {
            int firstLevel = 1;
            var res = new Dictionary<int, IList<EmployeeId>>();


            var employeeJobs = (from jo in session.Query<Job>()
                                join i in session.Query<JobPositionEmployee>()
                                    on jo.Id equals i.JobPosition.JobId
                                where enList.Contains(i.EmployeeId.EmployeeNo) && i.EmployeeId.PeriodId == periodId
                                select new { jo, i.EmployeeId }); //.Distinct();


            IEnumerable<EmployeeId> empCompletedInFirstLevel = new List<EmployeeId>();
            Calculation calculation = null;
            if (calculationId != null)
            {
                calculation = session.Query<Calculation>().Where(c => c.Id == calculationId).SingleOrDefault();
                if (calculation.CalculationResult != null && calculation.CalculationResult.LastCalculatedPath.HasValue)
                    firstLevel = calculation.CalculationResult.LastCalculatedPath.Value;
            }
            // todo :(LOW) query for caculation data 
            var dummy = employeeJobs.Select(empJob => empJob.jo).Fetch(j => j.JobIndexList).ToFuture();
            var employeeJobindexes = session.Query<Domain.Model.JobIndices.JobIndex>()
                                            .Where(j => employeeJobs.Select(empJob => empJob.jo)
                                                            .SelectMany(jo => jo.JobIndexList)
                                                            .Select(ji => ji.JobIndexId.Id)
                                                            .Contains(j.Id.Id)).ToFuture();

            var jobIndexLevels = employeeJobindexes.Where(ji => ji.CalculationLevel >= firstLevel)
                                  .Select(ji => ji.CalculationLevel).Distinct().ToList();

            //var empJobInexIdsLevel1 = employeeJobindexes.Where(ji=>jei.CalculationLevel ==1).Select(ji => ji.Id).ToList();
            //var empLevel1 = employeeJobs.Where(empj => empj.jo.JobIndexList.Any(i => empJobInexIdsLevel1.Contains(i))).Select(empj => empj.EmployeeId).ToList();

            for (int level = firstLevel; level <= jobIndexLevels.Max(); level++)
            {
                var empJobInexIdsLevel = employeeJobindexes.Where(ji => ji.CalculationLevel == level).Select(ji => ji.Id).ToList();
                var empLevel = employeeJobs.Where(empj => empj.jo.JobIndexList.Select(j=>j.JobIndexId).Any(i => empJobInexIdsLevel.Contains(i)))
                                .Select(empj => empj.EmployeeId).OrderBy(emp => emp.EmployeeNo).ToList();

                if (level == firstLevel)
                {
                    if (calculation.CalculationResult != null && calculation.CalculationResult.LastCalculatedEmployeeId != null)
                    {
                        var enNotCompletedInFirstLevel = enList.Skip(
                            enList.FindLastIndex(s => s == calculation.CalculationResult.LastCalculatedEmployeeId.EmployeeNo) + 1);

                        //var expEmployeeIds = calculation.EmployeeCalculationExceptions.Select(exp => exp.EmployeeId).ToList();
                        var expEmployeeIds = session.Query<EmployeeCalculationException>().Where(c => c.CalculationId.Id == calculationId.Id).Select(exp => exp.EmployeeId).ToList();
                        enNotCompletedInFirstLevel = enNotCompletedInFirstLevel.Union(expEmployeeIds.Select(exp => exp.EmployeeNo));
                        empLevel = empLevel.Where(emp => enNotCompletedInFirstLevel.Contains(emp.EmployeeNo)).ToList();
                    }

                }

                res.Add(level, empLevel);
            }

            return res.ToDictionary(i => i.Key, i => FindInList(i.Value.Select(x => x.EmployeeNo).ToList(), periodId));
        }

        public List<InquirySubjectWithJobPosition> GetEmployeeByWithJobPosition(IEnumerable<JobPositionInquiryConfigurationItemId> configurationIds, PeriodId inquirerPeriodId)
        {
            if (configurationIds != null || configurationIds.Any())
            {
                var inquirySubjectIdList = configurationIds.Select(c => c.InquirySubjectId.EmployeeNo).ToList();
                var inquirySubjects =
                    session.Query<Employee>()
                        .Where(
                            e =>
                                inquirySubjectIdList.Contains(e.Id.EmployeeNo) &&
                                e.Id.PeriodId.Id == inquirerPeriodId.Id)
                        .ToList();
                var inquirySubjectjobPositionIdList =
                    configurationIds.Select(c => c.InquirySubjectJobPositionId.SharedJobPositionId.Id).ToList();
                var inquirySubjectJobpositions =
                    session.Query<JobPosition>()
                        .Where(
                            e =>
                                inquirySubjectjobPositionIdList.Contains(e.Id.SharedJobPositionId.Id) &&
                                e.Id.PeriodId.Id == inquirerPeriodId.Id)
                        .Distinct()
                        .ToList();
                var inquirerJobPositionIdList =
                    configurationIds.Select(c => c.InquirerJobPositionId.SharedJobPositionId.Id).ToList();
                var inquirerJobpositions =
                    session.Query<JobPosition>()
                        .Where(
                            e =>
                                inquirerJobPositionIdList.Contains(e.Id.SharedJobPositionId.Id) &&
                                e.Id.PeriodId.Id == inquirerPeriodId.Id)
                        .Distinct()
                        .ToList();


                var res = configurationIds.Select(c => new InquirySubjectWithJobPosition
                {
                    InquirerJobPosition = inquirerJobpositions.Single(ij => ij.Id == c.InquirerJobPositionId),
                    InquirySubject = inquirySubjects.Single(e => e.Id == c.InquirySubjectId),
                    InquirySubjectJobPosition =
                    inquirySubjectJobpositions.Single(ij => ij.Id == c.InquirySubjectJobPositionId),
                    IsCompleted = session.Query<InquiryJobIndexPoint>().Count(i => i.ConfigurationItemId==c&&i.JobIndexValue=="")==0

                }).ToList();
                return res;
            }
            else
            {
                return new List<InquirySubjectWithJobPosition>();
            }

        }

        public CalculationData ProvideDataForRule(Employee employee, CalculationId calculationId, bool withCalculationPoints = false)
        {
            #region Query
            // employee job customFieldValue
            var employeeJobpositionCustomFieldsWithValue = session.Query<EmployeeJobPosition>().Where(j => j.Employee == employee)
                .FetchMany(j => j.EmployeeJobCustomFieldValues).ToFuture();

            var employeeWithCustomFieldsWithValue = session.Query<Employee>().Where(e => e == employee).FetchMany(e => e.CustomFieldValues).ToFuture();

            var employeeJobPositionsWithUnitAndSharedData = (from jobPositionEmployee in session.Query<JobPositionEmployee>().Where(j => j.EmployeeId == employee.Id)
                                                      join unit in session.Query<Unit>() on jobPositionEmployee.JobPosition.UnitId equals  unit.Id
                                                      select new {JobPositionEmployee= jobPositionEmployee , Unit=unit} )
                                                      .ToFuture();
            
            // employee Job and JobPosition
            var employeeJobs = (from job in session.Query<Job>()
                                join jobPositionEmployee in session.Query<JobPositionEmployee>()
                                on job.Id equals jobPositionEmployee.JobPosition.JobId
                                where jobPositionEmployee.EmployeeId == employee.Id
                                select job);
            var dummy1 = employeeJobs.Fetch(j => j.JobIndexList).ToFuture();
            var employeeJobsWithSharedJob = employeeJobs.Fetch(j => j.SharedJob).ToFuture();

            var employeeJobIndexesWithSharedData = session.Query<JobIndex>()
                .Where(j => employeeJobs.SelectMany(jo => jo.JobIndexList).Select(ji => ji.JobIndexId.Id).Contains(j.Id.Id)).OrderBy(i => i.CalculationOrder)
                .Fetch(j => j.SharedJobIndex).ToFuture();

            var jobIndexInquiryPoints = (from inquiryJobIndexPoint in session.Query<InquiryJobIndexPoint>().Where(i => i.ConfigurationItemId.InquirySubjectId == employee.Id)
                                         join emp in session.Query<Employee>() on inquiryJobIndexPoint.ConfigurationItemId.InquirerId equals emp.Id
                                         join jobPositionEmployee in session.Query<JobPositionEmployee>() on inquiryJobIndexPoint.ConfigurationItemId.InquirerJobPositionId equals jobPositionEmployee.JobPosition.Id
                                         select new { inquiryJobIndexPoint = inquiryJobIndexPoint, emp = emp, jobPositionEmployee.JobPosition.SharedJobPosition, inquiryJobIndexPoint.ConfigurationItemId.InquirySubjectJobPositionId.SharedJobPositionId, }
                          ).ToFuture();

            // employee Unit and JobPosition
            var employeeUnits = (from unit in session.Query<Unit>()
                                join jobPositionEmployee in session.Query<JobPositionEmployee>()
                                on unit.Id equals jobPositionEmployee.JobPosition.UnitId
                                where jobPositionEmployee.EmployeeId == employee.Id
                                select unit);
            var dummy2 = employeeUnits.Fetch(j => j.UnitIndexList).ToFuture();
            //var employeeUnitsWithSharedUnit = employeeUnits.Fetch(j => j.SharedUnit).ToFuture();

            var employeeUnitIndexesWithSharedData = session.Query<UnitIndex>()
                .Where(j => employeeUnits.SelectMany(jo => jo.UnitIndexList).Select(ji => ji.UnitIndexId.Id).Contains(j.Id.Id)).OrderBy(i => i.CalculationOrder)
                .Fetch(j => j.SharedUnitIndex).ToFuture();

            var unitIdList = employeeUnits.Select(eu => eu.Id.SharedUnitId.Id);
            var unitIndexInquiryPoints = (from inquiryUnitIndexPoint in session.Query<InquiryUnitIndexPoint>().Where(i => unitIdList.Contains(i.ConfigurationItemId.InquirySubjectUnitId.SharedUnitId.Id))
                                          join emp in session.Query<Employee>() on inquiryUnitIndexPoint.ConfigurationItemId.InquirerId equals emp.Id
                                          select new { inquiryUnitIndexPoint = inquiryUnitIndexPoint, emp = emp }
                          ).ToFuture();


            var allPoints = new List<CalculationPoint>();

            if (withCalculationPoints)
            {
                var empJobIndexPoints = session.Query<EmployeePoint>().Where(ep => ep.EmployeeId == employee.Id && ep.CalculationId == calculationId).ToList();//.ToFuture();
                allPoints.AddRange(empJobIndexPoints);
            }

            employeeWithCustomFieldsWithValue.ToList();

            #endregion

            var employeeData = from job in employeeJobsWithSharedJob.ToList()
                join employeeJobPositionWithUnit in employeeJobPositionsWithUnitAndSharedData.ToList()
                    on job.Id equals employeeJobPositionWithUnit.JobPositionEmployee.JobPosition.JobId
                join c in employeeJobpositionCustomFieldsWithValue.ToList()
                    on employeeJobPositionWithUnit.JobPositionEmployee.JobPosition.Id equals c.JobPositionId
                select new
                {
                    JobPosition = employeeJobPositionWithUnit.JobPositionEmployee.JobPosition,
                    Unit = employeeJobPositionWithUnit.Unit,
                    UnitIndexList =
                        employeeUnitIndexesWithSharedData.ToList()
                            .Where(
                                ji =>
                                    employeeJobPositionWithUnit.Unit.UnitIndexList.Select(x => x.UnitIndexId)
                                        .Contains(ji.Id))
                            .ToList(),
                    Job = job,
                    jobIndexList =
                        employeeJobIndexesWithSharedData.ToList()
                            .Where(ji => job.JobIndexList.Select(x => x.JobIndexId).Contains(ji.Id))
                            .ToList(),
                    c.EmployeeJobCustomFieldValues
                };


            var calculationData = new CalculationData();

            calculationData.employee = employee;

            calculationData.JobPositions = employeeData.ToDictionary(
                empData => empData.JobPosition,
                empData => new JobPositionData
                {
                    Job = empData.Job,
                    Indices = (from jobIndex in empData.jobIndexList
                        join jobIndexWithInquiryValues in
                            jobIndexInquiryPoints.Where(
                                g => g.SharedJobPositionId.Id == empData.JobPosition.SharedJobPosition.Id.Id)
                                .GroupBy(g => g.inquiryJobIndexPoint.JobIndexId)
                                .ToList()
                            on jobIndex.Id equals jobIndexWithInquiryValues.Key into gjii
                        from k in gjii.DefaultIfEmpty()
                        select new {ji = jobIndex, k})
                        .ToDictionary(j => j.ji,
                            j => (j.k != null
                                ? j.k.GroupBy(jk => jk.emp)
                                    .ToDictionary(g => g.Key,
                                        g =>
                                            g.Select(
                                                f =>
                                                    new InquiryData
                                                    {
                                                        JobPosition = f.SharedJobPosition,
                                                        Point = f.inquiryJobIndexPoint
                                                    }).ToList())
                                : null)),
                    Unit = empData.Unit,
                    //UnitIndices = (from unitIndex in empData.UnitIndexList
                    //    join unitIndexWithInquiryValues in
                    //        unitIndexInquiryPoints
                    //        .GroupBy(
                    //            g => g.inquiryUnitIndexPoint.ConfigurationItemId.UnitIndexIdUintPeriod).ToList()
                    //        on unitIndex.Id equals unitIndexWithInquiryValues.Key into gjii
                    //    from k in gjii.DefaultIfEmpty()
                    //    select new {UnitIndex = unitIndex, k})
                    //    .ToDictionary(j => j.UnitIndex,
                    //        j => j.k.Select(
                    //            ss =>
                    //                new System.Tuple<Employee, string>(ss.emp,
                    //                    ss.inquiryUnitIndexPoint.UnitIndexValue)).First()),

                    CustomFields = empData.EmployeeJobCustomFieldValues,
                    WorkTimePercent =
                        employee.JobPositions.Single(j => j.JobPositionId == empData.JobPosition.Id).WorkTimePercent,
                    Weight =
                        employee.JobPositions.Single(j => j.JobPositionId == empData.JobPosition.Id).JobPositionWeight

                });
            foreach (var jobPositionData in calculationData.JobPositions)
            {
                jobPositionData.Value.UnitIndices=new Dictionary<UnitIndex, System.Tuple<Employee, string>>();
                foreach (var unitIndex in employeeData.Single(e=>e.JobPosition.DictionaryName==jobPositionData.Key.DictionaryName).UnitIndexList)
                {
                    var inquiryunitIndexPoint = unitIndexInquiryPoints.Single(
                        ui =>
                            ui.inquiryUnitIndexPoint.ConfigurationItemId.UnitIndexIdUintPeriod == unitIndex.Id &&
                            ui.inquiryUnitIndexPoint.ConfigurationItemId.InquirySubjectUnitId.SharedUnitId ==
                            jobPositionData.Value.Unit.SharedUnit.Id);
                    jobPositionData.Value.UnitIndices.Add(unitIndex,
                        new System.Tuple<Employee, string>(inquiryunitIndexPoint.emp,
                            inquiryunitIndexPoint.inquiryUnitIndexPoint.UnitIndexValue));
                }
            }
            if (withCalculationPoints)
            {
                calculationData.CalculationPoints = allPoints.ToList();
            }
            return calculationData;
        }

        public void Attach(Employee employee)
        {
            rep.Attach(employee);
        }

        public List<string> GetAllEmployeeNo(Expression<Func<Employee, bool>> predicate)
        {
            return rep.Find(predicate).Select(e => e.Id.EmployeeNo).ToList();
        }

        public EmployeePointData GetPoints(Employee emp, CalculationId calcId)
        {
            var q = session.Query<SummaryEmployeePoint>().Where(j => j.EmployeeId == emp.Id && j.CalculationId == calcId).ToFuture();
            var q3 = session.Query<SummaryJobPositionPoint>().Where(j => j.EmployeeId == emp.Id && j.CalculationId == calcId);
            var q4 = q3.ToFuture();
            var q2 = (from j in session.Query<JobPosition>()
                      join i in session.Query<SummaryJobPositionPoint>()
                        on j.Id equals i.JobPositionId
                      where i.EmployeeId == emp.Id && i.CalculationId == calcId
                      select j);
            var q5 = q2.Fetch(j => j.SharedJobPosition).ToFuture();

            var q7 = session.Query<JobIndexPoint>().Where(j => j.EmployeeId == emp.Id && j.CalculationId == calcId);
            var q8 = q7.ToFuture();
            var q9 = (from j in session.Query<JobIndex>()
                      join i in session.Query<JobIndexPoint>()
                     on j.Id equals i.JobIndexId
                      where i.EmployeeId == emp.Id && i.CalculationId == calcId
                      select j).Fetch(j => j.SharedJobIndex).ToFuture();

            var res = new Dictionary<JobPosition, JobPositionPointData>();
            return new EmployeePointData
            {
                SummaryEmployeePoints = q.ToList(),
                SummaryJobPositionPoints = q5.ToDictionary(i => i, i =>
                new JobPositionPointData
                {
                    SummaryJobPositionPoints = q4.ToList().Where(j => j.JobPositionId.Equals(i.Id)).ToList(),
                    JobIndexPoints = q9.ToDictionary(j => j, j => q8.Where(k => k.JobIndexId.Equals(j.Id)).ToList())
                })
            };
        }


        public Exception ConvertException(Exception exp)
        {
            string typeName = "";
            string keyName = "";
            if (NHExceptionDetector.IsDublicateException(exp, out typeName, out keyName))
                return new EmployeeDuplicateException("Employee", "EmployeeNo");
            if (NHExceptionDetector.IsDeleteHasRelatedDataException(exp, out typeName, out keyName))
                return new EmployeeDeleteException("Employee", "EmployeeId");
            throw new Exception();
        }

        public Exception TryConvertException(Exception exp)
        {
            Exception res = null;
            try
            {
                res = ConvertException(exp);
            }
            catch (Exception e)
            {

            }
            return res;
        }
    }
}
