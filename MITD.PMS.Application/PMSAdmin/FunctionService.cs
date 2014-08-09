using System;
using System.Transactions;
using MITD.Core.RuleEngine;
using MITD.Core.RuleEngine.Model;
using MITD.Domain.Repository;
using MITD.PMS.Domain.Service;
using MITD.PMSAdmin.Application.Contracts;
using MITD.PMSAdmin.Domain.Model.Policies;
using MITD.PMSAdmin.Exceptions;

namespace MITD.PMSAdmin.Application
{
    public class FunctionService : IFunctionService
    {

        private readonly IRuleService ruleService;
        private readonly IPolicyRepository policyRep;

        public FunctionService(
                               IRuleService ruleService,
                               IPolicyRepository policyRep)
        {

            this.ruleService = ruleService;
            this.policyRep = policyRep;
        }

        public void DeleteFunction(PolicyId policyId, RuleFunctionId ruleFunctionId)
        {
            using (var scope = new TransactionScope())
            {
                var policy = policyRep.GetRuleBasePolicyById(policyId);
                var ruleFunction = ruleService.GetById(ruleFunctionId);
                policy.RemoveRuleFunction(ruleFunction);
                ruleService.DeleteFunction(ruleFunctionId);
                scope.Complete();
            }
        }

        public RuleFunctionBase AddFunction(string name, string content, PolicyId policyId)
        {

            using (var scope = new TransactionScope())
            {
                var policy = policyRep.GetRuleBasePolicyById(policyId);
                var ruleFunction = ruleService.AddRuleFunction(name, content);
                policy.AssignRuleFunction(ruleFunction);
                scope.Complete();
                return ruleFunction;

            }


        }

        public RuleFunctionBase UppdateFunction(RuleFunctionId ruleFunctionId, string name, string content)
        {
            using (var scope = new TransactionScope())
            {
                RuleFunctionBase function = ruleService.UpdateRuleFunction(ruleFunctionId, name, content);
                scope.Complete();
                return function;
            }
        }
    }
}
