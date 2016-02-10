using MITD.Core;

namespace MITD.PMS.Presentation.Contracts
{
    public interface IFunctionFacadeService:IFacadeService
    {
        PolicyFunctions GetPolicyFunctionsWithPagination(long policyId);
        FunctionDTO AddFunction(FunctionDTO function);
        FunctionDTO UpdateFunction(FunctionDTO function);
        FunctionDTO GetFunctionById(long id);
        string DeleteFunction(long policyId, long id);
    }
}
