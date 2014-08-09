using MITD.Domain.Model;
using MITD.PMS.Domain.Model.Policies;

namespace MITD.PMS.Domain.Service
{
    public class PolicyConfigurator : IPolicyConfigurator
    {
        private readonly IRuleEngineBasedPolicyService ruleEngineBasedPolicyService;
        private readonly IDllBasedPolicyEngineService dllBasedPolicyEngineService;


        public PolicyConfigurator(IRuleEngineBasedPolicyService ruleEngineBasedPolicyService)
        {
            this.ruleEngineBasedPolicyService = ruleEngineBasedPolicyService;
            this.dllBasedPolicyEngineService = null;
        }

        public void Config(Policy policy)
        {
            if(policy is RuleEngineBasedPolicy)
                ((RuleEngineBasedPolicy)policy).SetPolicyEngine(ruleEngineBasedPolicyService);
            if(policy is DllBasedPolicy)
                ((DllBasedPolicy)policy).SetPolicyEngin(dllBasedPolicyEngineService);
        }
    }
}
