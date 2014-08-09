using MITD.Core;
using MITD.Core.RuleEngine.Model;
using MITD.PMSAdmin.Domain.Model.Policies;

namespace MITD.PMSAdmin.Application.Contracts
{
    public interface IFunctionService : IService
    {


        void DeleteFunction(PolicyId id, RuleFunctionId functionId);
        RuleFunctionBase AddFunction(string name, string content, PolicyId policyId);
        RuleFunctionBase UppdateFunction(RuleFunctionId functionId, string name, string content);
    }
}
