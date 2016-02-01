using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.PMS.Integration.Core;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Integration.PMS.API
{
    public class JobServiceWrapper : IJobServiceWrapper
    {
        private readonly IUserProvider userProvider;
        private readonly string apiAddress = PMSClientConfig.BaseApiAddress + "Jobs";

        public JobServiceWrapper(IUserProvider userProvider)
        {
            this.userProvider = userProvider;
        }

        public void GetAllJobs(Action<PageResultDTO<JobDTOWithActions>, Exception> action, int pageSize, int pageIndex, Dictionary<string, string> sortBy)
        {

            var url = string.Format(apiAddress + "?PageSize=" + pageSize + "&PageIndex=" + pageIndex);
            IntegrationWebClient.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void GetAllJobs(Action<IList<JobDTO>, Exception> action)
        {
            var url = string.Format(apiAddress);
            IntegrationWebClient.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void DeleteJob(Action<string, Exception> action, long id)
        {
            var url = string.Format(apiAddress + "?Id=" + id);
            IntegrationWebClient.Delete(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void GetJobCustomFields(Action<List<CustomFieldDTO>, Exception> action, List<long> fieldIdList)
        {

            var url = string.Format(apiAddress);
            IntegrationWebClient.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        }

        public void GetJobCustomFields(Action<List<AbstractCustomFieldDescriptionDTO>, Exception> action, long id)
        {
            var url = string.Format(apiAddress);
            IntegrationWebClient.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        }


        public void GetJob(Action<JobDTO, Exception> action, long id)
        {
            var url = string.Format(apiAddress + "?Id=" + id);
            IntegrationWebClient.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void GetJob(Action<JobDTO, Exception> action, string transferId)
        {

        }

        public void AddJob(Action<JobDTO, Exception> action, JobDTO job)
        {
            IntegrationWebClient.Post(new Uri(apiAddress, PMSClientConfig.UriKind), action, job, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void UpdateJob(Action<JobDTO, Exception> action, JobDTO job)
        {
            var url = string.Format(apiAddress + "?Id=" + job.Id);
            IntegrationWebClient.Put(new Uri(url, PMSClientConfig.UriKind), action, job, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

    }
}
