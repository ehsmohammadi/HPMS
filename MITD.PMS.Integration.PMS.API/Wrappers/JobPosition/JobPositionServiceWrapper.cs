using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.PMS.Integration.Core;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Integration.PMS.API
{
    public class JobPositionServiceWrapper : IJobPositionServiceWrapper
    {
        private readonly IUserProvider userProvider;
        private readonly string baseAddress = PMSClientConfig.BaseApiAddress + "JobPositions";
        //private string baseAddress = String.Format("{0}://{1}:{2}/api/JobPositions",
        //    Application.Current.Host.Source.Scheme,
        //    Application.Current.Host.Source.Host,
        //    Application.Current.Host.Source.Port);

        public JobPositionServiceWrapper(IUserProvider userProvider)
        {
            this.userProvider = userProvider;
        }


        public void GetAllJobPositions(Action<PageResultDTO<JobPositionDTOWithActions>, Exception> action, int pageSize, int pageIndex)
        {

            var url = string.Format(baseAddress + "?PageSize=" + pageSize + "&PageIndex=" + pageIndex);
            IntegrationWebClient.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void DeleteJobPosition(Action<string, Exception> action, long id)
        {
            var url = string.Format(baseAddress + "?Id=" + id);
            IntegrationWebClient.Delete(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void GetAllJobPositions(Action<List<JobPositionDTO>, Exception> action)
        {
            var url = string.Format(baseAddress);
            IntegrationWebClient.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void GetJobPosition(Action<JobPositionDTO, Exception> action, long id)
        {
            var url = string.Format(baseAddress + "?Id=" + id);
            IntegrationWebClient.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void AddJobPosition(Action<JobPositionDTO, Exception> action, JobPositionDTO jobPosition)
        {
            var url = string.Format(baseAddress);
            IntegrationWebClient.Post(new Uri(url, PMSClientConfig.UriKind), action, jobPosition, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void UpdateJobPosition(Action<JobPositionDTO, Exception> action, JobPositionDTO jobPosition)
        {
            var url = string.Format(baseAddress);
            IntegrationWebClient.Put(new Uri(url, PMSClientConfig.UriKind), action, jobPosition, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

    }
}
