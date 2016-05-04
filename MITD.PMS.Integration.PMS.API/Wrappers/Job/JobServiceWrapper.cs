using System;
using MITD.PMS.Integration.Core;
using MITD.PMS.Integration.PMS.Contract;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Integration.PMS.API
{
    public class JobServiceWrapper : IJobServiceWrapper
    {

        private readonly IUserProvider userProvider;

        private Uri apiUri = new Uri(PMSClientConfig.BaseApiAddress);

        private string endpoint = "Jobs";

        public JobServiceWrapper(IUserProvider userProvider)
        {
            this.userProvider = userProvider;
        }


        public JobDTO GetJob(long id)
        {
            return IntegrationHttpClient.Get<JobDTO>(apiUri, endpoint + "?Id=" + id);
            //var url = string.Format(baseAddress + "?Id=" + id);
            //IntegrationWebClient.Get(new Uri(url, UriKind.Absolute),
            //    action, IntegrationWebClient.MessageFormat.Json, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public JobDTO AddJob(JobDTO job)
        {
            return IntegrationHttpClient.Post<JobDTO, JobDTO>(apiUri, endpoint, job);
            //var url = string.Format(baseAddress);
            //IntegrationWebClient.Post(new Uri(url, UriKind.Absolute),
            //    action, job, IntegrationWebClient.MessageFormat.Json, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public JobDTO GetByTransferId(Guid transferId)
        {
            return IntegrationHttpClient.Get<JobDTO>(apiUri, endpoint + "?TransferId=" + transferId);
        }

        //public void GetAllJobs(Action<PageResultDTO<JobDTOWithActions>, Exception> action, int pageSize, int pageIndex)
        //{

        //    var url = string.Format(baseAddress + "?PageSize=" + pageSize + "&PageIndex=" + pageIndex);
        //    IntegrationWebClient.Get(new Uri(url, UriKind.Absolute),
        //        action, IntegrationWebClient.MessageFormat.Json, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        //}

        //public void DeleteJob(Action<string, Exception> action, long id)
        //{
        //    var url = string.Format(baseAddress + "?Id=" + id);
        //    IntegrationWebClient.Delete(new Uri(url, UriKind.Absolute), action, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        //}

        //public void GetAllJobs(Action<List<JobDTO>, Exception> action)
        //{
        //    var url = string.Format(baseAddress);
        //    IntegrationWebClient.Get(new Uri(url, UriKind.Absolute),
        //        action, IntegrationWebClient.MessageFormat.Json, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        //}



        //public void UpdateJob(Action<JobDTO, Exception> action, JobDTO job)
        //{
        //    var url = string.Format(baseAddress);
        //    IntegrationWebClient.Put(new Uri(url, UriKind.Absolute),
        //        action, job,
        //        IntegrationWebClient.MessageFormat.Json, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        //}

    }
}
