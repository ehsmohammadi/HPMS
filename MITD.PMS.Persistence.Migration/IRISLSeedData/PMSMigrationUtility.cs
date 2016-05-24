using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Domain.Model.JobIndices;
using MITD.PMS.Domain.Model.JobPositions;
using MITD.PMS.Domain.Model.Jobs;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Domain.Model.UnitIndices;
using MITD.PMS.Domain.Model.Units;
using MITD.PMS.Domain.Service;
using MITD.PMS.Persistence.NH;


namespace MITD.PMS.Persistence
{
    public static class PMSMigrationUtility
    {
        #region Fields
        public static Period Period;
        public static List<Job> Jobs = new List<Job>();
        //public static List<Unit> Units = new List<Unit>();
        public static Dictionary<Unit, Dictionary<string, string>> Units = new Dictionary<Unit, Dictionary<string, string>>(); 
        public static Dictionary<JobPosition, Dictionary<string, List<string>>> JobPositions = new Dictionary<JobPosition, Dictionary<string, List<string>>>();
        public static List<Employee> Employees=new List<Employee>(); 


        private static List<JobIndex> jobIndices = new List<JobIndex>();
        private static List<UnitIndex> unitIndices = new List<UnitIndex>();
        #endregion


        #region Methods
        public static void CreatePeriod(IPeriodRepository periodRepository, string name, DateTime from, DateTime to)
        {
            var periodManagerService = new PeriodManagerService(periodRepository, null, null, null, null, null, null, null, null,null,null,null);
            Period = new Period(new PeriodId(periodRepository.GetNextId()), name, from, to,91);
            Period.ChangeActiveStatus(periodManagerService, true);
            periodRepository.Add(Period);

        }

        public static JobIndexGroup CreateJobIndexGroup(IJobIndexRepository jobIndexRepository, string name, string dictionaryName)
        {
            var jobIndexGroup = new JobIndexGroup(jobIndexRepository.GetNextId(), Period, null, name, dictionaryName);
            jobIndexRepository.Add(jobIndexGroup);
            return jobIndexGroup;
        }

        public static void CreateJobIndex(IJobIndexRepository jobIndexRepository, PMSAdmin.Domain.Model.JobIndices.JobIndex adminJobIndex,
            JobIndexGroup jobIndexGroup, bool isInquireable, Dictionary<PMSAdmin.Domain.Model.CustomFieldTypes.CustomFieldType, string> customFieldsDictionary, long clacLevel)
        {
            var sharedJobIndex = new SharedJobIndex(new SharedJobIndexId(adminJobIndex.Id.Id), adminJobIndex.Name,
                adminJobIndex.DictionaryName);
            var jobIndex = new JobIndex(jobIndexRepository.GetNextId(), Period, sharedJobIndex, jobIndexGroup,
                isInquireable, clacLevel);
            var sharedCustomFieldsDic =
                customFieldsDictionary.ToDictionary(
                    c =>
                        new SharedJobIndexCustomField(new SharedJobIndexCustomFieldId(c.Key.Id.Id), c.Key.Name,
                            c.Key.DictionaryName, c.Key.MinValue, c.Key.MaxValue),
                    c => c.Value);
            jobIndex.UpdateCustomFields(sharedCustomFieldsDic);
            jobIndexRepository.Add(jobIndex);
            jobIndices.Add(jobIndex);


        }

        public static void CreateJob(IJobRepository jobRepository, PMSAdmin.Domain.Model.Jobs.Job adminJob)
        {
            var jobCustomFields =
                AdminMigrationUtility.DefinedCustomFields.Where(d => adminJob.CustomFieldTypeIdList.Contains(d.Id))
                    .Select(
                        c =>
                            new JobCustomField(
                                new JobCustomFieldId(Period.Id, new SharedJobCustomFieldId(c.Id.Id),
                                    new SharedJobId(adminJob.Id.Id)),
                                new SharedJobCustomField(new SharedJobCustomFieldId(c.Id.Id), c.Name, c.DictionaryName,
                                    c.MinValue, c.MaxValue, c.TypeId))).ToList();
            var jobJobIndices = jobIndices.Select(j => new JobJobIndex(j.Id, true, true, true)).ToList();
            var job = new Job(Period, new SharedJob(new SharedJobId(adminJob.Id.Id), adminJob.Name, adminJob.DictionaryName), jobCustomFields, jobJobIndices);
            jobRepository.Add(job);
            Jobs.Add(job);


        }

        public static UnitIndexGroup CreateUnitIndexGroup(IUnitIndexRepository unitIndexRepository, string name, string dictionaryName)
        {
            var unitIndexGroup = new UnitIndexGroup(unitIndexRepository.GetNextId(), Period, null, name, dictionaryName);
            unitIndexRepository.Add(unitIndexGroup);
            return unitIndexGroup;
        }

        public static void CreateUnitIndex(IUnitIndexRepository unitIndexRepository, PMSAdmin.Domain.Model.UnitIndices.UnitIndex adminUnitIndex,
            UnitIndexGroup unitIndexGroup, bool isInquireable, Dictionary<PMSAdmin.Domain.Model.CustomFieldTypes.CustomFieldType, string> customFieldsDictionary)
        {
            var sharedUnitIndex = new SharedUnitIndex(new SharedUnitIndexId(adminUnitIndex.Id.Id), adminUnitIndex.Name,
                adminUnitIndex.DictionaryName);
            var unitIndex = new UnitIndex(unitIndexRepository.GetNextId(), Period, sharedUnitIndex, unitIndexGroup,
                isInquireable);
            var sharedCustomFieldsDic =
                customFieldsDictionary.ToDictionary(
                    c =>
                        new SharedUnitIndexCustomField(new SharedUnitIndexCustomFieldId(c.Key.Id.Id), c.Key.Name,
                            c.Key.DictionaryName, c.Key.MinValue, c.Key.MaxValue),
                    c => c.Value);
            unitIndex.UpdateCustomFields(sharedCustomFieldsDic);
            unitIndexRepository.Add(unitIndex);
            unitIndices.Add(unitIndex);


        }

        public static Unit CreateUnit(IUnitRepository unitRepository, PMSAdmin.Domain.Model.Units.Unit adminUnit, Unit parentUnit,Dictionary<string,string> indexValue )
        {
            var unitCustomFields =
                AdminMigrationUtility.DefinedCustomFields.Where(d => adminUnit.CustomFieldTypeIdList.Contains(d.Id))
                    .Select(
                        c =>
                            new UnitCustomField(
                                new UnitCustomFieldId(Period.Id, new SharedUnitCustomFieldId(c.Id.Id),
                                    new SharedUnitId(adminUnit.Id.Id)),
                                new SharedUnitCustomField(new SharedUnitCustomFieldId(c.Id.Id), c.Name, c.DictionaryName,
                                    c.MinValue, c.MaxValue, c.TypeId))).ToList();
            var unitUnitIndices = unitIndices.Select(j => new UnitUnitIndex(j.Id, true, true, true)).ToList();
            var unit = new Unit(Period, new SharedUnit(new SharedUnitId(adminUnit.Id.Id), adminUnit.Name, adminUnit.DictionaryName), unitCustomFields, unitUnitIndices, parentUnit);
            unitRepository.Add(unit);
            Units.Add(unit,indexValue);
            return unit;
        }

        public static JobPosition CreateJobPosition(IJobPositionRepository jobPositionRepository, PMSAdmin.Domain.Model.JobPositions.JobPosition adminJobPosition, JobPosition parent, Job job, Unit unit, Dictionary<string, List<string>> indexValues)
        {

            var jobPosition = new JobPosition(Period,
                new SharedJobPosition(new SharedJobPositionId(adminJobPosition.Id.Id), adminJobPosition.Name,
                    adminJobPosition.DictionaryName), parent, job, unit);
            jobPositionRepository.Add(jobPosition);
            JobPositions.Add(jobPosition, indexValues);
            return jobPosition;
        }

        public static void CreateEmployee(IEmployeeRepository employeeRepository, string employeeNo, string firstName, string lastName, JobPosition jobPosition)
        {
            var employee = new Employee(employeeNo, Period, firstName, lastName);
            employeeRepository.Add(employee);
            if (jobPosition != null)
                employee.AssignJobPosition(jobPosition, DateTime.Now, DateTime.Now, 100, 1, null);
            Employees.Add(employee);
            //var employee1 =
            //        new PMS.Domain.Model.Employees.Employee(
            //            ((1 + 1) * 2000).ToString(), period, "کارمند" + 1,
            //            "کارمندیان" + 1);

            //var jobPositionInPeriod1 = jobPositionInPeriodList[1];

            //var jobcustomFields1 = jobInPeriodList.Single(j => j.Id.Equals(jobPositionInPeriod1.JobId)).CustomFields;
            //if (jobcustomFields1 != null && jobcustomFields1.Count != 0)
            //{
            //    var employeeJobPosition = new Domain.Model.Employees.EmployeeJobPosition(employee1, jobPositionInPeriod1, period.StartDate, period.EndDate, 100, 1,
            //    jobcustomFields1.Select(j => new EmployeeJobCustomFieldValue(j.Id, docSeedValues.Single(d => d.DicName == j.DictionaryName).Value1)).ToList()
            //    );
            //    employee1.AssignJobPositions(new List<Domain.Model.Employees.EmployeeJobPosition> { employeeJobPosition }, periodManagerService);

            //}

            //empList.Add(employee1);
            //employeeRep.Add(employee1);

        }


        public static void CreateJobIndexPointWithValuesFromMatrix(KeyValuePair<JobPosition, Dictionary<string, List<string>>> jobPositionWithValue,
            JobRepository jobRep, JobIndexRepository jobIndexRep, InquiryJobIndexPointRepository inquiryRep, JobPositionRepository jobPositionRepository)
        {
            var jobPosition = jobPositionRepository.GetBy(jobPositionWithValue.Key.Id);
            foreach (var itm in jobPosition.ConfigurationItemList)
            {
                var job = jobRep.GetById(itm.JobPosition.JobId);
                foreach (var jobIndexId in job.JobIndexList)
                {
                    var jobIndex = (JobIndex)jobIndexRep.GetById(jobIndexId.JobIndexId);
                    PMSMigrationUtility.CreateJobIndexIndexPointWithValue(inquiryRep, jobIndex, itm,
                            jobPositionWithValue.Value[jobIndex.DictionaryName]);
                }
            }
        }


        private static void CreateJobIndexIndexPointWithValue(InquiryJobIndexPointRepository inquiryRep, JobIndex jobIndex,
            JobPositionInquiryConfigurationItem itm, List<string> values)
        {

            if (itm.InquirerJobPositionLevel.Equals(JobPositionLevel.None))
            {
                CreateJobIndexPoint(inquiryRep, jobIndex, itm, values[0]);
            }
            if (itm.InquirerJobPositionLevel.Equals(JobPositionLevel.Childs))
            {
                CreateJobIndexPoint(inquiryRep, jobIndex, itm, values[1]);
            }

            if (itm.InquirerJobPositionLevel.Equals(JobPositionLevel.Parents))
            {
                CreateJobIndexPoint(inquiryRep, jobIndex, itm, values[2]);
            }
        }

        private static void CreateJobIndexPoint(InquiryJobIndexPointRepository inquiryRep, JobIndex jobIndex,
            JobPositionInquiryConfigurationItem itm, string value)
        {
            var inquiryIndexPoint = new Domain.Model.InquiryJobIndexPoints.InquiryJobIndexPoint(
                new Domain.Model.InquiryJobIndexPoints.InquiryJobIndexPointId(inquiryRep.GetNextId()),
                itm, jobIndex, value);
            inquiryRep.Add(inquiryIndexPoint);
        }

        #endregion

    }
}
