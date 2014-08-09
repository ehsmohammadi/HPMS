using MITD.PMS.Presentation.Contracts;
using System.Web.Http;

namespace MITD.PMS.Service.Host.Controllers
{
    [Authorize]
    public class PolicyFunctionsController : ApiController
    {
        private readonly IFunctionFacadeService functionService;

        public PolicyFunctionsController(IFunctionFacadeService functionService)
        {
            this.functionService = functionService;
        }

        public PolicyFunctions GetPolicyFunctions(long policyId)
        {
            return functionService.GetPolicyFunctionsWithPagination( policyId);
        }
        public string DeleteFunction(long policyId,long id)
        {
            return functionService.DeleteFunction(policyId,id);
        }



       
    }
}