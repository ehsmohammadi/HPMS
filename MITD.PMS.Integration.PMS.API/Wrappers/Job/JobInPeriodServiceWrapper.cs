using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.PMS.Integration.Core;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Integration.PMS.API
{
    public partial class JobInPeriodServiceWrapper : IJobInPeriodServiceWrapper
    {
        private readonly IUserProvider userProvider;
        private readonly string baseAddress = PMSClientConfig.BaseApiAddress;

        public JobInPeriodServiceWrapper(IUserProvider userProvider)
        {
            this.userProvider = userProvider;
        }

        private string makeApiAdress(long periodId)
        {
            return "Periods/" + periodId + "/Jobs";
        }


        public void GetAllJobInPeriodWithPagination(Action<PageResultDTO<JobInPeriodDTOWithActions>, Exception> action,
                                                    long periodId, int pageSize, int pageIndex)
        {
            GetAllJobInPeriodWithPagination(action, periodId, pageSize, pageIndex, "");
        }

        public void GetAllJobInPeriodWithPagination(Action<PageResultDTO<JobInPeriodDTOWithActions>, Exception> action, long periodId, int pageSize, int pageIndex, string columnNames)
        {

            var url = string.Format(baseAddress + makeApiAdress(periodId) + "?PageSize=" + pageSize + "&PageIndex=" + pageIndex);
            url += !string.IsNullOrWhiteSpace(columnNames) ? "&SelectedColumns=" + columnNames : "";
            IntegrationWebClient.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        }

        public void GetAllJobInPeriod(Action<IList<JobInPeriodDTO>, Exception> action, long periodId, string columnNames)
        {
            var url = string.Format(baseAddress + makeApiAdress(periodId) + "?Typeof=jobInPeriodDTO");
            url += !string.IsNullOrWhiteSpace(columnNames) ? "&SelectedColumns=" + columnNames : "";
            IntegrationWebClient.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        }

        public void GetAllJobInPeriod(Action<IList<JobInPeriodDTO>, Exception> action, long periodId)
        {
            GetAllJobInPeriod(action, periodId, "");
        }


        public void GetJobInPeriod(Action<JobInPeriodDTO, Exception> action, long periodId, long jobId)
        {
            GetJobInPeriod(action, periodId, jobId, "");
        }

        public void GetJobInPeriod(Action<JobInPeriodDTO, Exception> action, long periodId, long jobId, string columnNames)
        {

            var url = string.Format(baseAddress + makeApiAdress(periodId) + "?JobId=" + jobId);
            url += !string.IsNullOrWhiteSpace(columnNames) ? "&SelectedColumns=" + columnNames : "";
            IntegrationWebClient.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        }

        public void AddJobInPeriod(Action<JobInPeriodDTO, Exception> action, long periodId, JobInPeriodDTO jobInPeriod)
        {

            var url = string.Format(baseAddress + makeApiAdress(periodId));
            IntegrationWebClient.Post(new Uri(url, PMSClientConfig.UriKind), action, jobInPeriod, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        }

        public void UpdateJobInPeriod(Action<JobInPeriodDTO, Exception> action, long periodId, JobInPeriodDTO jobInPeriod)
        {

            var url = string.Format(baseAddress + makeApiAdress(periodId));
            IntegrationWebClient.Put(new Uri(url, PMSClientConfig.UriKind), action, jobInPeriod, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        }

        public void DeleteJobInPeriod(Action<string, Exception> action, long periodId, long jobId)
        {
            var url = string.Format(baseAddress + makeApiAdress(periodId) + "?jobId=" + jobId);
            IntegrationWebClient.Delete(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }


    }
}
