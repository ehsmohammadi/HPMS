using MITD.Core;
using System.Collections.Generic;

namespace MITD.PMS.Presentation.Contracts
{
    public interface IPolicyFacadeService:IFacadeService
    {
         
        PageResultDTO<PolicyDTOWithActions> GetAllPolicies(int pageSize, int pageIndex);
        List<PolicyDTOWithActions> GetAllPolicies();
        PolicyDTO AddPolicy(PolicyDTO policy);
        PolicyDTO UpdatePolicy(PolicyDTO policy);
        PolicyDTO GetPolicyById(long id);
        string DeletePolicy(long id);
    }
}
