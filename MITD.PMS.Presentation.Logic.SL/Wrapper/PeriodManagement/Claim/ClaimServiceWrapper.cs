using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using MITD.PMS.Presentation.Contracts;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic.Wrapper
{
    public partial class ClaimServiceWrapper : IClaimServiceWrapper
    {
        private readonly IUserProvider userProvider;

        private readonly string baseAddress = PMSClientConfig.BaseApiAddress;

        public ClaimServiceWrapper(IUserProvider userProvider)
        {
            this.userProvider = userProvider;
        }

        private string makeApiAdress(long periodId)
        {
            return "Periods/" + periodId + "/Claims";
        }

        private string makeApiAdressClaimStates(long periodId)
        {
            return "Periods/" + periodId + "/ClaimStates";
        }

        private string makeApiAdressClaimTypes(long periodId)
        {
            return "Periods/" + periodId + "/ClaimTypes";
        }


        public void GetClaim(Action<ClaimDTO, Exception> action,long periodId , long claimId)
        {
            var url = string.Format(baseAddress + makeApiAdress(periodId) + "?Id=" + claimId);
            WebClientHelper.Get(new Uri(url, UriKind.Absolute),
                action, WebClientHelper.MessageFormat.Json, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void GetAllClaim(Action<PageResultDTO<ClaimDTOWithAction>, Exception> action, long periodId, string employeeNo, int pageSize, int pageIndex)
        {
            var url = string.Format(baseAddress + makeApiAdress(periodId) + "?EmployeeNo=" + employeeNo + "&PageSize=" + pageSize + "&PageIndex=" + pageIndex);
            WebClientHelper.Get(new Uri(url, UriKind.Absolute),action,
                WebClientHelper.MessageFormat.Json, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void GetAllClaim(Action<PageResultDTO<ClaimDTOWithAction>, Exception> action, long periodId, int pageSize, int pageIndex)
        {
            var url = string.Format(baseAddress + makeApiAdress(periodId) + "?PageSize=" + pageSize + "&PageIndex=" + pageIndex);
            WebClientHelper.Get(new Uri(url, UriKind.Absolute), action,
                WebClientHelper.MessageFormat.Json, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void AddClaim(Action<ClaimDTO, Exception> action, ClaimDTO claim)
        {
            var url = string.Format(baseAddress + makeApiAdress(claim.PeriodId));
            WebClientHelper.Post(new Uri(url, UriKind.Absolute), action, claim, WebClientHelper.MessageFormat.Json, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        
        public void DeleteClaim(Action<string, Exception> action,long periodId, long id)
        {
            var url = string.Format(baseAddress + makeApiAdress(periodId) + "?Id=" + id);
            WebClientHelper.Delete(new Uri(url, UriKind.Absolute), action, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void GetClaimTypeList(Action<List<ClaimTypeDTO>, Exception> action, long periodId)
        {
            var url = string.Format(baseAddress + makeApiAdressClaimTypes(periodId));
            WebClientHelper.Get(new Uri(url, UriKind.Absolute),
                                action, WebClientHelper.MessageFormat.Json, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        }

        public void GetAllClaimStateList(Action<List<ClaimStateDTO>, Exception> action, long periodId)
        {
            var url = string.Format(baseAddress + makeApiAdressClaimStates(periodId) );
            WebClientHelper.Get(new Uri(url, UriKind.Absolute),
                action, WebClientHelper.MessageFormat.Json, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }


        public void ChangeClaimState(Action<ClaimDTO, Exception> action, long periodId, long claimId, string changeStateMessage, ClaimStateDTO claimState)
        {
            var url = string.Format(baseAddress + makeApiAdress(periodId) +"?Id="+claimId+"&Message="+changeStateMessage);
            WebClientHelper.Put(new Uri(url, UriKind.Absolute), action, claimState, WebClientHelper.MessageFormat.Json, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

     
    }
}
