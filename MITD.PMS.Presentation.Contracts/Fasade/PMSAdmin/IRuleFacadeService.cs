using MITD.Core;

namespace MITD.PMS.Presentation.Contracts
{
    public interface IRuleFacadeService:IFacadeService
    {
        PolicyRules GetPolicyRulesWithPagination(long policyId);
        RuleDTO AddRule(RuleDTO function);
        RuleDTO UpdateRule(RuleDTO function);
        string DeleteRule(long policyId, long id);
        string CompileRule(long policyId, string ruleContent);
        RuleDTO GetRuleById(long policyId, long id);
        PageResultDTO<RuleTrailDTOWithAction> GetRuleTrailsWithPagination(long ruleId, int pageSize, int pageIndex);
        RuleTrailDTO GetRuleTrail(long ruleTrailId);
    }
}
