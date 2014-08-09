using MITD.PMS.Presentation.Contracts;
using System.Web.Http;

namespace MITD.PMS.Service.Host.Controllers
{
    [Authorize]
    public class PolicyRulesController : ApiController
    {
        private readonly IRuleFacadeService ruleService;

        public PolicyRulesController(IRuleFacadeService ruleService)
        {
            this.ruleService = ruleService;
        }

        public PolicyRules GetPolicyRules(long policyId)
        {
            return ruleService.GetPolicyRulesWithPagination( policyId);
        }
        public string DeleteRule(long policyId,long id)
        {
            return ruleService.DeleteRule(policyId,id);
        }

        public RuleDTO GetRule(long policyId, long id)
        {
            return ruleService.GetRuleById(policyId, id);
        }



       
    }
}