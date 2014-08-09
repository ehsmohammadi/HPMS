using System;
using MITD.PMS.Presentation.Contracts;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic.Wrapper
{
    public partial class FunctionServiceWrapper :IFunctionServiceWrapper
    {
        private readonly IUserProvider userProvider;
        private readonly string apiAddress = PMSClientConfig.BaseApiAddress + "Functions";
        private readonly string policyFunctionsApiAddress = PMSClientConfig.BaseApiAddress + "PolicyFunctions";

        public FunctionServiceWrapper(IUserProvider userProvider)
        {
            this.userProvider = userProvider;
        }

        public void DeleteFunction(Action<string, Exception> action, long policyId, long functionId)
        {
            var url = string.Format(policyFunctionsApiAddress +"?policyId=" + policyId+ "&Id=" + functionId);
            WebClientHelper.Delete(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void GetPolicyFunctionsWithPagination(Action<PolicyFunctions, Exception> action, long policyId, int pageSize, int pageIndex)
        {
            var url = string.Format(policyFunctionsApiAddress + "?PageSize=" + pageSize + "&PageIndex=" + pageIndex + "&PolicyId=" + policyId);
            WebClientHelper.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void GetPolicyFunctions(Action<PolicyFunctions, Exception> action, long policyId)
        {
            var url = string.Format(policyFunctionsApiAddress + "?PolicyId=" + policyId);
            WebClientHelper.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void GetFunction(Action<FunctionDTO, Exception> action, long id)
        {
            var url = string.Format(apiAddress + "?Id=" + id);
            WebClientHelper.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void AddFunction(Action<FunctionDTO, Exception> action, FunctionDTO function)
        {
            WebClientHelper.Post(new Uri(apiAddress, PMSClientConfig.UriKind), action, function, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void UpdateFunction(Action<FunctionDTO, Exception> action, FunctionDTO function)
        {
            var url = string.Format(apiAddress + "?Id=" + function.Id);
            WebClientHelper.Put(new Uri(url, PMSClientConfig.UriKind), action, function, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }


    }
}
