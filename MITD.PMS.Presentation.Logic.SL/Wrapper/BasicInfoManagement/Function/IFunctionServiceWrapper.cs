using System;
using MITD.PMS.Presentation.Contracts;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public interface IFunctionServiceWrapper : IServiceWrapper
    {
        void DeleteFunction(Action<string, Exception> action, long policyId, long functionId);
        void GetFunction(Action<FunctionDTO, Exception> action, long id);
        void AddFunction(Action<FunctionDTO, Exception> action, FunctionDTO function);
        void UpdateFunction(Action<FunctionDTO, Exception> action, FunctionDTO function);
        void GetPolicyFunctionsWithPagination(Action<PolicyFunctions, Exception> action, long periodId, int pageSize, int pageIndex);
        void GetPolicyFunctions(Action<PolicyFunctions, Exception> action, long policyId);
    }
}
