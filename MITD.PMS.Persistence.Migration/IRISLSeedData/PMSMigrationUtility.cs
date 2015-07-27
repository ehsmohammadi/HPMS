using System;
using System.Collections.Generic;
using System.Linq;
using MITD.PMS.Domain.Model.JobIndices;
using MITD.PMS.Domain.Model.JobPositions;
using MITD.PMS.Domain.Model.Jobs;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Domain.Model.UnitIndices;
using MITD.PMS.Domain.Model.Units;
using MITD.PMS.Domain.Service;


namespace MITD.PMS.Persistence
{
    public static class PMSMigrationUtility
    {
        public static Period Period;

        public static void CreatePeriod(IPeriodRepository periodRepository, string name, DateTime from,DateTime to)
        {
            var periodManagerService = new PeriodManagerService(periodRepository, null, null, null, null, null, null, null, null);
            Period = new Period(new PeriodId(periodRepository.GetNextId()), name, from, to);
            Period.ChangeActiveStatus(periodManagerService, true);
            periodRepository.Add(Period);

        }

        public static JobIndexGroup CreateJobIndexGroup(IJobIndexRepository jobIndexRepository, string name, string dictionaryName)
        {
            //if (jobIndexCategory == null)
            //{
            //    jobIndexCategory = new JobIndexCategory(jobIndexRepository.GetNextId(), null, "شاخص های شغل/فردی",
            //        "JobIndices");
            //    jobIndexRepository.Add(jobIndexCategory);
            //}

            //var jobIndex = new JobIndex(jobIndexRepository.GetNextId(), jobIndexCategory,
            //        name, dictionaryName);
            // jobIndex.AssignCustomFields(DefinedCustomFields.Where(dc => dc.EntityId == EntityTypeEnum.JobIndex).ToList());
            //jobIndexRepository.Add(jobIndex);
            // GenralJobIndexList.Add(new UnitindexDes { JobIndex = jobIndex1, Importance = "7", IsInquirable = true });
            var jobIndexGroup=new JobIndexGroup(jobIndexRepository.GetNextId(),Period,null,name,dictionaryName);
            return jobIndexGroup;
        }

        public static void CreateJobIndex(IJobIndexRepository jobIndexRepository, PMSAdmin.Domain.Model.JobIndices.JobIndex adminJobIndex,
            JobIndexGroup jobIndexGroup, bool isInquireable)
        {
            var sharedJobIndex = new SharedJobIndex(new SharedJobIndexId(adminJobIndex.Id.Id), adminJobIndex.Name,
                adminJobIndex.DictionaryName);
            var jobIndex = new JobIndex(jobIndexRepository.GetNextId(), Period, sharedJobIndex, jobIndexGroup,
                isInquireable);
            jobIndexRepository.Add(jobIndex);

        }

        public static void CreateUnit(IUnitRepository unitRepository, string name, string dictionaryName)
        {
            //var unit = new Unit(unitRepository.GetNextId(),
            //            name, dictionaryName);
            //unit.AssignCustomFields(DefinedCustomFields.Where(dc => dc.EntityId == EntityTypeEnum.Unit).ToList());
            //unitRepository.Add(unit);
            //unitList.Add(unit);
        }

        public static void CreateUnitIndex(IUnitIndexRepository unitIndexRepository, string name, string dictionaryName)
        {
            //if (unitIndexCategory == null)
            //{
            //    unitIndexCategory = new UnitIndexCategory(unitIndexRepository.GetNextId(), null, "شاخص های سازمانی",
            //        "UnitIndices");
            //    unitIndexRepository.Add(unitIndexCategory);
            //}

            //var unitIndex = new UnitIndex(unitIndexRepository.GetNextId(), unitIndexCategory,
            //        name, dictionaryName);
            //unitIndex.AssignCustomFields(DefinedCustomFields.Where(dc => dc.EntityId == EntityTypeEnum.UnitIndex).ToList());
            //unitIndexRepository.Add(unitIndex);
            // GenralUnitIndexList.Add(new UnitindexDes { UnitIndex = jobIndex1, Importance = "7", IsInquirable = true });
        }

        public static void CreateJob(IJobRepository jobRepository, string name, string dictionaryName)
        {
            //var job = new Job(jobRepository.GetNextId(),name, dictionaryName);
           // job.AssignCustomFields(DefinedCustomFields.Where(dc => dc.EntityId.Equals(EntityTypeEnum.Job)).ToList());
            //jobRepository.Add(job);
            // GenralUnitIndexList.Add(new UnitindexDes { UnitIndex = jobIndex1, Importance = "7", IsInquirable = true });
        }

        

        public static void CreateJobPosition(IJobPositionRepository jobPositionRepository, string name, string dictionaryName)
        {
            //var jobPosition = new JobPosition(jobPositionRepository.GetNextId(), name, dictionaryName);
            //jobPositionRepository.Add(jobPosition);
            // GenralUnitIndexList.Add(new UnitindexDes { UnitIndex = jobPositionIndex1, Importance = "7", IsInquirable = true });
        }

    }
}
