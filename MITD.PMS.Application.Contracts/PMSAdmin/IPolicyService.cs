using MITD.Core;
using MITD.PMSAdmin.Domain.Model.Policies;

namespace MITD.PMSAdmin.Application.Contracts
{
    public interface IPolicyService:IService
    { 
        
        Policy AddPolicy(string name, string dictionaryName);
        Policy UpdatePolicy(PolicyId policyId, string name, string dictionaryName);
        void DeletePolicy(PolicyId policyId);
    }
}
