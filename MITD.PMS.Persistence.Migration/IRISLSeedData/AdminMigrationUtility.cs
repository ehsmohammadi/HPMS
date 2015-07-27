using System.Collections.Generic;
using System.Linq;
using MITD.PMSAdmin.Domain.Model.CustomFieldTypes;
using MITD.PMSAdmin.Domain.Model.JobIndices;
using MITD.PMSAdmin.Domain.Model.JobPositions;
using MITD.PMSAdmin.Domain.Model.Jobs;
using MITD.PMSAdmin.Domain.Model.Policies;
using MITD.PMSAdmin.Domain.Model.UnitIndices;
using MITD.PMSAdmin.Domain.Model.Units;

namespace MITD.PMS.Persistence
{
    public static class AdminMigrationUtility
    {
        public static List<CustomFieldType> DefinedCustomFields =new List<CustomFieldType>();
        public static List<UnitIndex> UnitIndices=new List<UnitIndex>();
        public static Dictionary<string,JobIndex> JobIndices=new Dictionary<string, JobIndex>();
 
        private static UnitIndexCategory unitIndexCategory;
        private static JobIndexCategory jobIndexCategory;

        public static void DefineCustomFieldType(ICustomFieldRepository customFieldRepository, string name,
            string dictionaryName, int min, int max, EntityTypeEnum entityType)
        {
            var customfieldType = new CustomFieldType(customFieldRepository.GetNextId(),
                name, dictionaryName, min, max, entityType, "string");
            customFieldRepository.Add(customfieldType);
            DefinedCustomFields.Add(customfieldType);
        }


        public static void CreateUnit(IUnitRepository unitRepository, string name, string dictionaryName)
        {
            var unit = new Unit(unitRepository.GetNextId(),
                        name, dictionaryName);
            unit.AssignCustomFields(DefinedCustomFields.Where(dc => dc.EntityId == EntityTypeEnum.Unit).ToList());
            unitRepository.Add(unit);
            //unitList.Add(unit);
        }

        public static void CreateUnitIndex(IUnitIndexRepository unitIndexRepository, string name, string dictionaryName)
        {
            if (unitIndexCategory == null)
            {
                unitIndexCategory = new UnitIndexCategory(unitIndexRepository.GetNextId(), null, "شاخص های سازمانی",
                    "UnitIndices");
                unitIndexRepository.Add(unitIndexCategory);
            }

            var unitIndex = new UnitIndex(unitIndexRepository.GetNextId(), unitIndexCategory,
                    name, dictionaryName);
            unitIndex.AssignCustomFields(DefinedCustomFields.Where(dc => dc.EntityId == EntityTypeEnum.UnitIndex).ToList());
            unitIndexRepository.Add(unitIndex);
            UnitIndices.Add(unitIndex);
        }

        public static void CreateJob(IJobRepository jobRepository, string name, string dictionaryName)
        {
            var job = new Job(jobRepository.GetNextId(),name, dictionaryName);
            job.AssignCustomFields(DefinedCustomFields.Where(dc => dc.EntityId.Equals(EntityTypeEnum.Job)).ToList());
            jobRepository.AddJob(job);
            // GenralUnitIndexList.Add(new UnitindexDes { UnitIndex = jobIndex1, Importance = "7", IsInquirable = true });
        }



        public static void CreateJobIndex(IJobIndexRepository jobIndexRepository, string name, string dictionaryName,string group)
        {
            if (jobIndexCategory == null)
            {
                jobIndexCategory = new JobIndexCategory(jobIndexRepository.GetNextId(), null, "شاخص های شغل/فردی",
                    "JobIndices");
                jobIndexRepository.Add(jobIndexCategory);
            }

            var jobIndex = new JobIndex(jobIndexRepository.GetNextId(), jobIndexCategory,
                    name, dictionaryName);
            jobIndex.AssignCustomFields(DefinedCustomFields.Where(dc => dc.EntityId == EntityTypeEnum.JobIndex).ToList());
            jobIndexRepository.Add(jobIndex);
            JobIndices.Add(group,jobIndex);
        }

        public static void CreateJobPosition(IJobPositionRepository jobPositionRepository, string name, string dictionaryName)
        {
            var jobPosition = new JobPosition(jobPositionRepository.GetNextId(), name, dictionaryName);
            jobPositionRepository.Add(jobPosition);
            // GenralUnitIndexList.Add(new UnitindexDes { UnitIndex = jobPositionIndex1, Importance = "7", IsInquirable = true });
        }

        public static void CreatePolicy(IPolicyRepository policyRepository, string name, string dictionaryName)
        {
            var policy = new RuleEngineBasedPolicy(policyRepository.GetNextId(), name, dictionaryName);
            policyRepository.Add(policy);
            ////foreach (var rule in rules)
            ////{
            ////    policy.AssignRule(rule);
            ////}
            ////policy.AssignRuleFunction(rf);
            // GenralUnitIndexList.Add(new UnitindexDes { UnitIndex = policyIndex1, Importance = "7", IsInquirable = true });
        }

        


    }
}
