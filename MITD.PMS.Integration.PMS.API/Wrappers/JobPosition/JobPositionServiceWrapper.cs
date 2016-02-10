using System;
using MITD.PMS.Integration.Core;
using MITD.PMS.Integration.PMS.Contract;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Integration.PMS.API
{
    public class JobPositionServiceWrapper : IJobPositionServiceWrapper
    {
        private readonly Uri apiUri = new Uri(PMSClientConfig.BaseApiAddress);

        private string endpoint = "JobPositions";

        private readonly IUserProvider userProvider;
        
        public JobPositionServiceWrapper(IUserProvider userProvider)
        {
            this.userProvider = userProvider;
        }

        public JobPositionDTO GetJobPosition(long id)
        {
            return IntegrationHttpClient.Get<JobPositionDTO>(apiUri, endpoint + "?Id=" + id);
            //var url = string.Format(baseAddress + "?Id=" + id);
            //IntegrationWebClient.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public JobPositionDTO AddJobPosition(JobPositionDTO jobPosition)
        {
            return IntegrationHttpClient.Post<JobPositionDTO, JobPositionDTO>(apiUri, endpoint, jobPosition);
           // var url = string.Format(baseAddress);
            //IntegrationWebClient.Post(new Uri(url, PMSClientConfig.UriKind), action, jobPosition, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        //public void UpdateJobPosition(Action<JobPositionDTO, Exception> action, JobPositionDTO jobPosition)
        //{
        //    var url = string.Format(baseAddress);
        //    IntegrationWebClient.Put(new Uri(url, PMSClientConfig.UriKind), action, jobPosition, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        //}

        //public void GetAllJobPositions(Action<PageResultDTO<JobPositionDTOWithActions>, Exception> action, int pageSize, int pageIndex)
        //{

        //    var url = string.Format(baseAddress + "?PageSize=" + pageSize + "&PageIndex=" + pageIndex);
        //    IntegrationWebClient.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        //}

        //public void DeleteJobPosition(Action<string, Exception> action, long id)
        //{
        //    var url = string.Format(baseAddress + "?Id=" + id);
        //    IntegrationWebClient.Delete(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        //}

        //public void GetAllJobPositions(Action<List<JobPositionDTO>, Exception> action)
        //{
        //    var url = string.Format(baseAddress);
        //    IntegrationWebClient.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        //}

    }
}
