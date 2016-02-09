using System;
using MITD.PMS.Integration.Core;
using MITD.PMS.Integration.PMS.Contract;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Integration.PMS.API
{
    public class JobIndexInPeriodServiceWrapper : IJobIndexInPeriodServiceWrapper
    {
        private readonly Uri apUri = new Uri(PMSClientConfig.BaseApiAddress);

        private readonly IUserProvider userProvider;
        private readonly string jobIndexClassType = "JobIndex";
        private readonly string jobIndexCategoryClassType = "JobIndexGroup";

        public JobIndexInPeriodServiceWrapper(IUserProvider userProvider)
        {
            this.userProvider = userProvider;
        }

        private string makeEndPoint(long periodId)
        {
            return "Periods/" + periodId + "/JobIndices";
        }


        public JobIndexInPeriodDTO GetJobIndexInPeriod(long periodId, long abstractId)
        {
            return GetJobIndexInPeriod(periodId, abstractId, "");
        }

        public JobIndexInPeriodDTO GetJobIndexInPeriod(long periodId, long abstractId, string columnNames)
        {
            var endpoint = makeEndPoint(periodId);
            endpoint += !string.IsNullOrWhiteSpace(columnNames) ? "&SelectedColumns=" + columnNames : "";
            return IntegrationHttpClient.Get<JobIndexInPeriodDTO>(apUri, endpoint + "?abstractId=" + abstractId);
            //var url = string.Format(baseAddress + makeApiAdress(periodId) + "?abstractId=" + abstractId);
            //url += !string.IsNullOrWhiteSpace(columnNames) ? "&SelectedColumns=" + columnNames : "";
            //IntegrationWebClient.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        }

        public JobIndexInPeriodDTO AddJobIndexInPeriod(JobIndexInPeriodDTO jobIndexInPeriod)
        {
            return IntegrationHttpClient.Post<JobIndexInPeriodDTO, JobIndexInPeriodDTO>(apUri,
                makeEndPoint(jobIndexInPeriod.PeriodId), jobIndexInPeriod);
            //var url = string.Format(baseAddress + makeApiAdress(jobIndexInPeriod.PeriodId));
            //IntegrationWebClient.Post(new Uri(url, PMSClientConfig.UriKind), action, jobIndexInPeriod, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        }

        //public void DeleteJobIndexInPeriod(Action<string, Exception> action, long periodId, long jobIndexId)
        //{
        //    var url = string.Format(baseAddress + makeApiAdress(periodId) + "?abstractId=" + jobIndexId);
        //    IntegrationWebClient.Delete(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        //}

        //public void GetPeriodAbstractIndexes(Action<List<AbstractIndexInPeriodDTOWithActions>, Exception> action, long periodId)
        //{
        //    var url = string.Format(baseAddress + makeApiAdress(periodId));
        //    IntegrationWebClient.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        //}

        //public void GetAllPeriodJobIndexes(Action<List<JobIndexInPeriodDTO>, Exception> action, long periodId)
        //{
        //    var url = string.Format(baseAddress + makeApiAdress(periodId) + "?typeOf=JobIndex");
        //    IntegrationWebClient.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        //}

        //public void UpdateJobIndexInPeriod(Action<JobIndexInPeriodDTO, Exception> action, JobIndexInPeriodDTO jobIndexInPeriod)
        //{
        //    //action(jobIndexInPeriod, null);
        //    //var url = string.Format(baseAddress + "?Id=" + jobIndexInPeriod.Id);
        //    //WebClientHelper.Put<JobIndexInPeriod, JobIndexInPeriod>(new Uri(url, UriKind.Absolute),
        //    //    (res, exp) => action(res, exp), jobIndexInPeriod,
        //    //    WebClientHelper.MessageFormat.Json);
        //    var url = string.Format(baseAddress + makeApiAdress(jobIndexInPeriod.PeriodId));
        //    IntegrationWebClient.Put(new Uri(url, PMSClientConfig.UriKind), action, jobIndexInPeriod, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        //}

        //public void AddJobIndexGroupInPeriod(Action<JobIndexGroupInPeriodDTO, Exception> action, JobIndexGroupInPeriodDTO jobIndexGroupInPeriod)
        //{
        //    var url = string.Format(baseAddress + makeApiAdress(jobIndexGroupInPeriod.PeriodId));
        //    IntegrationWebClient.Post(new Uri(url, PMSClientConfig.UriKind), action, jobIndexGroupInPeriod, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        //}

        //public void UpdateJobIndexGroupInPeriod(Action<JobIndexGroupInPeriodDTO, Exception> action, JobIndexGroupInPeriodDTO jobIndexGroupInPeriod)
        //{
        //    var url = string.Format(baseAddress + makeApiAdress(jobIndexGroupInPeriod.PeriodId));
        //    IntegrationWebClient.Put(new Uri(url, PMSClientConfig.UriKind), action, jobIndexGroupInPeriod, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        //}

        //public void GetJobIndexGroupInPeriod(Action<JobIndexGroupInPeriodDTO, Exception> action, long periodId,
        //                                     long abstractId)
        //{
        //    GetJobIndexGroupInPeriod(action, periodId, abstractId, "");
        //}

        //public void GetJobIndexGroupInPeriod(Action<JobIndexGroupInPeriodDTO, Exception> action, long periodId, long abstractId, string columnNames)
        //{
        //    //var c = TestData.abstractIndexInPeriodDtoWithActionses.Single(j => j.Id == id);
        //    //action(new JobIndexGroupInPeriodDTO
        //    //    {
        //    //        Id = c.Id,
        //    //        Name = c.Name,
        //    //        ParentId = c.ParentId
        //    //    }, null);

        //    var url = string.Format(baseAddress + makeApiAdress(periodId) + "?abstractId=" + abstractId);
        //    url += !string.IsNullOrWhiteSpace(columnNames) ? "&SelectedColumns=" + columnNames : "";
        //    IntegrationWebClient.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        //}

        //public void DeleteJobIndexGroupInPeriod(Action<string, Exception> action, long periodId, long abstractId)
        //{
        //    var url = string.Format(baseAddress + makeApiAdress(periodId) + "?abstractId=" + abstractId);
        //    IntegrationWebClient.Delete(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        //}

        //public void GetPeriodJobIndexes(Action<List<JobIndexGroupInPeriodDTO>, Exception> action, long periodId)
        //{
        //    throw new NotImplementedException();
        //}
    }
}