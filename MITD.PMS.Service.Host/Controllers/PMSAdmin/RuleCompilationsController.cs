using MITD.PMS.Presentation.Contracts;
using System;
using System.Web;
using System.Web.Http;

namespace MITD.PMS.Service.Host.Controllers
{
    [Authorize]
    public class RuleCompilationsController : ApiController
    {
        private readonly IRuleFacadeService ruleService;

        public RuleCompilationsController(IRuleFacadeService ruleService)
        {
            this.ruleService = ruleService;
            var x = HttpContext.Current;
        }

        public string Post(long policyId, RuleContentDTO ruleContent)
        {
            return ruleService.CompileRule(policyId, ruleContent.RuleContent);
        }
    }
}