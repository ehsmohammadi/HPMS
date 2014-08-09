using MITD.PMS.Presentation.Contracts;
using System.Collections.Generic;
using System.Web.Http;

namespace MITD.PMS.Service.Host.Controllers
{
    [Authorize]
    public class PoliciesController : ApiController
    {
        private readonly IPolicyFacadeService policyService;

        public PoliciesController(IPolicyFacadeService policyService)
        {
            this.policyService = policyService;
        } 

        public PageResultDTO<PolicyDTOWithActions> GetAllPolicys(int pageSize, int pageIndex,string filter="",string sortBy="")
        {
            return policyService.GetAllPolicies(pageSize, pageIndex);
        }

        public List<PolicyDTOWithActions> GetAllPolicys()
        {
            return policyService.GetAllPolicies();
        }

        public PolicyDTO PostPolicy(PolicyDTO policy)
        {
            return policyService.AddPolicy(policy);
        }
        public PolicyDTO PutPolicy(PolicyDTO policy)
        {
            return policyService.UpdatePolicy(policy);
        }

        public PolicyDTO GetPolicy(long id)
        {
            return policyService.GetPolicyById(id);
        }

        public string DeletePolicy(long id)
        {
            return policyService.DeletePolicy(id);
        }
    }
}