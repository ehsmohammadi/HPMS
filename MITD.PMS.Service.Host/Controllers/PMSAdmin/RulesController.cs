using MITD.PMS.Presentation.Contracts;
using System.Web.Http;

namespace MITD.PMS.Service.Host.Controllers
{
    [Authorize]
    public class RulesController : ApiController
    {
        private readonly IRuleFacadeService ruleService;

        public RulesController(IRuleFacadeService ruleService)
        {
            this.ruleService = ruleService;
        } 

        public RuleDTO PostRule(RuleDTO function)
        {
            return ruleService.AddRule(function);
        }
        public RuleDTO PutRule(RuleDTO function)
        {
            return ruleService.UpdateRule(function);
        }




    }
}