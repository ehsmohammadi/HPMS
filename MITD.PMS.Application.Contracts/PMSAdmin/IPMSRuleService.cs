using MITD.Core;
using MITD.Core.RuleEngine.Model;
using MITD.PMSAdmin.Domain.Model.Policies;
using MITD.PMSAdminReport.Domain.Model;

namespace MITD.PMSAdmin.Application.Contracts
{
    public interface IPMSRuleService : IService
    {
        void DeleteRule(PolicyId id, RuleId ruleId);
        RuleWithPolicyData AddRule(string name, string ruleText, RuleType ruleType, PolicyId policyIdt,int order);
        RuleWithPolicyData UpdateRule(PolicyId id, RuleId ruleId, string name, string ruleText, RuleType ruleType, int order);
        void CompileRule(PolicyId id, string ruleContent);
        RuleWithPolicyData GetRuleById(PolicyId policyId, RuleId ruleId);
    }
}
