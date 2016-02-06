using System.Collections.Generic;
using System.Linq;
using MITD.Core.RuleEngine;
using MITD.Core.RuleEngine.Model;
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
        #region Fields
        public static List<CustomFieldType> DefinedCustomFields = new List<CustomFieldType>();
        public static List<UnitIndex> UnitIndices = new List<UnitIndex>();
        public static Dictionary<JobIndex, string> JobIndices = new Dictionary<JobIndex, string>();
        public static List<Job> Jobs = new List<Job>();
        public static List<Unit> Units = new List<Unit>();
        public static List<JobPosition> JobPositions = new List<JobPosition>();
        private static List<RuleFunction> ruleFunctions = new List<RuleFunction>();
        private static List<Rule> rules = new List<Rule>();

        private static UnitIndexCategory unitIndexCategory;
        private static JobIndexCategory jobIndexCategory;
        public static Policy Policy { get; set; }

        #endregion

        #region Methods
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
            Units.Add(unit);
        }

        public static void CreateUnitIndexCategory(IUnitIndexRepository unitIndexRepository)
        {
            if (unitIndexCategory == null)
            {
                unitIndexCategory = new UnitIndexCategory(unitIndexRepository.GetNextId(), null, "شاخص های سازمانی",
                    "UnitIndexCategory");
                unitIndexRepository.Add(unitIndexCategory);
            }
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
            var job = new Job(jobRepository.GetNextId(), name, dictionaryName);
            job.AssignCustomFields(DefinedCustomFields.Where(dc => dc.EntityId.Equals(EntityTypeEnum.Job)).ToList());
            jobRepository.AddJob(job);
            Jobs.Add(job);
        }

        public static void CreateJobIndexCategory(IJobIndexRepository jobIndexRepository)
        {
            if (jobIndexCategory == null)
            {
                jobIndexCategory = new JobIndexCategory(jobIndexRepository.GetNextId(), null, "شاخص های شغل/فردی",
                    "JobIndexCategory");
                jobIndexRepository.Add(jobIndexCategory);
            }

        }

        public static void CreateJobIndex(IJobIndexRepository jobIndexRepository, string name, string dictionaryName, string group)
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
            JobIndices.Add(jobIndex, group);
        }

        public static void CreateJobPosition(IJobPositionRepository jobPositionRepository, string name, string dictionaryName)
        {
            var jobPosition = new JobPosition(jobPositionRepository.GetNextId(), name, dictionaryName);
            jobPositionRepository.Add(jobPosition);
            JobPositions.Add(jobPosition);
        }

        public static void CreatePolicy(IPolicyRepository policyRepository, string name, string dictionaryName)
        {
            var policy = new RuleEngineBasedPolicy(policyRepository.GetNextId(), name, dictionaryName);
            policyRepository.Add(policy);
            foreach (var rule in rules)
            {
                policy.AssignRule(rule);
            }
            foreach (var function in ruleFunctions)
            {
                policy.AssignRuleFunction(function);
            }
            Policy = policy;
        }

        public static void CreateRuleEnginConfigurationItem(IREConfigeRepository reConfigeRepository, string name, string value)
        {
            var rec = new RuleEngineConfigurationItem(
                    new RuleEngineConfigurationItemId(name),value);
            reConfigeRepository.Add(rec);

        }

        public static void CreateRule(IRuleRepository ruleRepository, string name, RuleType ruleType, int ruleOrder, string ruleText)
        {
            var rule = new Rule(new RuleId(ruleRepository.GetNextId()), name, ruleText, ruleType, ruleOrder);
            ruleRepository.Add(rule);
            rules.Add(rule);
            
        }

        public static void CreateRuleFunction(IRuleFunctionRepository ruleFunctionRepository, string name, string ruleFunctionText)
        {
            var ruleFunction = new RuleFunction(ruleFunctionRepository.GetNextId(), name, ruleFunctionText);
            ruleFunctionRepository.Add(ruleFunction);
            ruleFunctions.Add(ruleFunction);

        } 
        #endregion

    }
}
