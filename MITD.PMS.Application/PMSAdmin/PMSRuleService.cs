using System;
using System.Linq;
using System.Transactions;
using MITD.Core.RuleEngine;
using MITD.Core.RuleEngine.Model;
using MITD.Domain.Repository;
using MITD.PMS.Domain.Service;
using MITD.PMSAdmin.Application.Contracts;
using MITD.PMSAdmin.Domain.Model.Policies;
using MITD.PMSAdmin.Exceptions;
using MITD.PMSAdminReport.Domain.Model;

namespace MITD.PMSAdmin.Application
{
    public class PMSRuleService : IPMSRuleService
    {
        private readonly IRuleService ruleService;
        private readonly IPolicyRepository policyRep;

        public PMSRuleService(IRuleService ruleService,
                               IPolicyRepository policyRep)
        {
            this.ruleService = ruleService;
            this.policyRep = policyRep;
        }

        public void DeleteRule(PolicyId policyId, RuleId ruleId)
        {
            using (var scope = new TransactionScope())
            {
                var policy = policyRep.GetRuleBasePolicyById(policyId);
                var rule = ruleService.GetById(ruleId);
                policy.RemoveRule(rule);
                ruleService.DeleteRule(ruleId);
                scope.Complete();
            }
        }

        public RuleWithPolicyData AddRule(string name, string ruleText, RuleType ruleType, PolicyId policyId, int order)
        {
            using (var scope = new TransactionScope())
            {
                var policy = policyRep.GetRuleBasePolicyById(policyId);
                var rule = ruleService.AddRule(name, ruleText, ruleType, order);
                policy.AssignRule(rule);
                scope.Complete();
                return new RuleWithPolicyData { Policy = policy, Rule = rule };;
            }

        }

        public RuleWithPolicyData UpdateRule(PolicyId policyId, RuleId ruleId, string name, string ruleText, RuleType ruleType, int order)
        {
            using (var scope = new TransactionScope())
            {
                var policy = policyRep.GetRuleBasePolicyById(policyId);
                var rule = ruleService.UpdateRule(ruleId, name, ruleText, ruleType, order);
                scope.Complete();
                return new RuleWithPolicyData { Policy = policy, Rule = rule };
            }
        }

        public void CompileRule(PolicyId policyId, string ruleContent)
        {
            var policy = policyRep.GetRuleBasePolicyById(policyId);
            ruleService.CompileRule(policy.RuleFunctions, ruleContent);
        }

        public RuleWithPolicyData GetRuleById(PolicyId policyId, RuleId ruleId)
        {
            var policy = policyRep.GetRuleBasePolicyById(policyId);
            var rule = ruleService.GetById(ruleId);
            return new RuleWithPolicyData { Policy = policy, Rule = rule };
        }
    }
}
