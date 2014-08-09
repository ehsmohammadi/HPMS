using System.Collections.Generic;
using MITD.Presentation;
using MITD.PMS.Presentation.Contracts;
using System.Collections.ObjectModel;
using System;


namespace MITD.PMS.Presentation.Logic
{
    public interface IPolicyServiceWrapper : IServiceWrapper
    {
        void GetPolicy(Action<PolicyDTO, Exception> action, long id);
        void AddPolicy(Action<PolicyDTO, Exception> action, PolicyDTO policy);
        void UpdatePolicy(Action<PolicyDTO, Exception> action, PolicyDTO policy);
        void GetAllPolicys(Action<PageResultDTO<PolicyDTOWithActions>, Exception> action, int pageSize, int pageIndex);
        void GetAllPolicys(Action<List<PolicyDescriptionDTO>, Exception> action);
        void DeletePolicy(Action<string, Exception> action, long id);
       
    }
}
