using System;
using System.Collections.Generic;
using System.Windows;
using MITD.PMS.Presentation.Contracts;
using System.Linq;
using MITD.Presentation;


namespace MITD.PMS.Presentation.Logic.Wrapper
{
    public class PolicyServiceWrapper : IPolicyServiceWrapper
    {
        private readonly IUserProvider userProvider;
        private string apiAddress = PMSClientConfig.BaseApiAddress + "Policies";

        public PolicyServiceWrapper(IUserProvider userProvider)
        {
            this.userProvider = userProvider;
        }

        public void GetAllPolicys(Action<PageResultDTO<PolicyDTOWithActions>, Exception> action, int pageSize, int pageIndex)
        {

            var url = string.Format(apiAddress + "?PageSize=" + pageSize + "&PageIndex=" + pageIndex);
            WebClientHelper.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void GetAllPolicys(Action<List<PolicyDescriptionDTO>, Exception> action)
        {
            var url = string.Format(apiAddress );
            WebClientHelper.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void DeletePolicy(Action<string, Exception> action, long id)
        {
            var url = string.Format(apiAddress + "?Id=" + id);
            WebClientHelper.Delete(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void GetPolicy(Action<PolicyDTO, Exception> action, long id)
        {

            var url = string.Format(apiAddress + "?Id=" + id);
            WebClientHelper.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void AddPolicy(Action<PolicyDTO, Exception> action, PolicyDTO policy)
        {
            var url = string.Format(apiAddress);
            WebClientHelper.Post(new Uri(url, PMSClientConfig.UriKind), action, policy, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void UpdatePolicy(Action<PolicyDTO, Exception> action, PolicyDTO policy)
        {
            var url = string.Format(apiAddress);
            WebClientHelper.Put(new Uri(url, PMSClientConfig.UriKind), action, policy, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

    }
}
