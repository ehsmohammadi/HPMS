using MITD.PMS.Presentation.Contracts;
using System.Web.Http;

namespace MITD.PMS.Service.Host.Controllers
{
    [Authorize]
    public class PolicyRuleTrailsController : ApiController
    {
        private readonly IRuleFacadeService ruleService;

        public PolicyRuleTrailsController(IRuleFacadeService ruleService)
        {
            this.ruleService = ruleService;
        }

        public PageResultDTO<RuleTrailDTOWithAction> GetRuleTrails(long policyId, long ruleId, int pageSize, int pageIndex)
        {
            return ruleService.GetRuleTrailsWithPagination(ruleId, pageSize, pageIndex);
        }

        public RuleTrailDTO GetRuleTrail(long policyId, long ruleId, long id)
        {
            return ruleService.GetRuleTrail(id);
        }


       
    }
}