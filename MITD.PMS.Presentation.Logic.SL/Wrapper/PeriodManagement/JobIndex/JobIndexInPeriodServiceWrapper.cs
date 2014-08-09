using System;
using System.Linq;
using System.Windows;
using MITD.PMS.Presentation.Contracts;
using System.Collections.Generic;
using MITD.Presentation;


namespace MITD.PMS.Presentation.Logic.Wrapper
{
    public class JobIndexInPeriodServiceWrapper : IJobIndexInPeriodServiceWrapper
    {
        private readonly IUserProvider userProvider;
        private readonly string baseAddress = PMSClientConfig.BaseApiAddress;
        private readonly string jobIndexClassType = "JobIndex";
        private readonly string jobIndexCategoryClassType = "JobIndexGroup";

        public JobIndexInPeriodServiceWrapper(IUserProvider userProvider)
        {
            this.userProvider = userProvider;
        }

        private string makeApiAdress(long periodId)
        {
            return "Periods/" + periodId + "/JobIndices";
        }

        #region Temprory Presentation Code


        #endregion


        public void DeleteJobIndexInPeriod(Action<string, Exception> action,long periodId, long jobIndexId)
        {
            var url = string.Format(baseAddress + makeApiAdress(periodId) + "?abstractId=" + jobIndexId);
            WebClientHelper.Delete(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void GetPeriodAbstractIndexes(Action<List<AbstractIndexInPeriodDTOWithActions>, Exception> action, long periodId)
        {
            var url = string.Format(baseAddress + makeApiAdress(periodId) );
            WebClientHelper.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
 
        }

        public void GetAllPeriodJobIndexes(Action<List<JobIndexInPeriodDTO>, Exception> action, long periodId)
        {
            var url = string.Format(baseAddress + makeApiAdress(periodId) +"?typeOf=JobIndex");
            WebClientHelper.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        }

        public void GetJobIndexInPeriod(Action<JobIndexInPeriodDTO, Exception> action,long periodId, long abstractId)
        {
            //var c = TestData.abstractIndexInPeriodDtoWithActionses.Single(j => j.Id == id);
            //action(new JobIndexInPeriodDTO
            //{
            //    Id = c.Id,
            //    Name = c.Name,
            //    ParentId = c.ParentId,
            //    JobIndexId = ((JobIndexInPeriodDTOWithActions)c).JobIndexId 
            //}, null);
            GetJobIndexInPeriod(action, periodId, abstractId, "");
        }

        public void GetJobIndexInPeriod(Action<JobIndexInPeriodDTO, Exception> action, long periodId, long abstractId, string columnNames)
        {
            var url = string.Format(baseAddress + makeApiAdress(periodId) + "?abstractId=" + abstractId);
            url += !string.IsNullOrWhiteSpace(columnNames) ? "&SelectedColumns=" + columnNames : "";
            WebClientHelper.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        }

        public void AddJobIndexInPeriod(Action<JobIndexInPeriodDTO, Exception> action, JobIndexInPeriodDTO jobIndexInPeriod)
        {
            //action(jobIndexInPeriod, null);
            //var url = string.Format(baseAddress);
            //WebClientHelper.Post<JobIndexInPeriod, JobIndexInPeriod>(new Uri(url, UriKind.Absolute),
            //    (res, exp) => action(res, exp), jobIndexInPeriod,
            //    WebClientHelper.MessageFormat.Json);

            var url = string.Format(baseAddress + makeApiAdress(jobIndexInPeriod.PeriodId));
            WebClientHelper.Post(new Uri(url, PMSClientConfig.UriKind), action, jobIndexInPeriod, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        }

        public void UpdateJobIndexInPeriod(Action<JobIndexInPeriodDTO, Exception> action, JobIndexInPeriodDTO jobIndexInPeriod)
        {
            //action(jobIndexInPeriod, null);
            //var url = string.Format(baseAddress + "?Id=" + jobIndexInPeriod.Id);
            //WebClientHelper.Put<JobIndexInPeriod, JobIndexInPeriod>(new Uri(url, UriKind.Absolute),
            //    (res, exp) => action(res, exp), jobIndexInPeriod,
            //    WebClientHelper.MessageFormat.Json);
            var url = string.Format(baseAddress + makeApiAdress(jobIndexInPeriod.PeriodId));
            WebClientHelper.Put(new Uri(url, PMSClientConfig.UriKind), action, jobIndexInPeriod, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        }

        public void AddJobIndexGroupInPeriod(Action<JobIndexGroupInPeriodDTO, Exception> action, JobIndexGroupInPeriodDTO jobIndexGroupInPeriod)
        {
            var url = string.Format(baseAddress + makeApiAdress(jobIndexGroupInPeriod.PeriodId));
            WebClientHelper.Post(new Uri(url, PMSClientConfig.UriKind), action, jobIndexGroupInPeriod, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        }

        public void UpdateJobIndexGroupInPeriod(Action<JobIndexGroupInPeriodDTO, Exception> action, JobIndexGroupInPeriodDTO jobIndexGroupInPeriod)
        {
            var url = string.Format(baseAddress + makeApiAdress(jobIndexGroupInPeriod.PeriodId));
            WebClientHelper.Put(new Uri(url, PMSClientConfig.UriKind), action, jobIndexGroupInPeriod, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        }

        public void GetJobIndexGroupInPeriod(Action<JobIndexGroupInPeriodDTO, Exception> action, long periodId,
                                             long abstractId)
        {
            GetJobIndexGroupInPeriod(action, periodId, abstractId, "");
        }

        public void GetJobIndexGroupInPeriod(Action<JobIndexGroupInPeriodDTO, Exception> action,long periodId, long abstractId, string columnNames)
        {
            //var c = TestData.abstractIndexInPeriodDtoWithActionses.Single(j => j.Id == id);
            //action(new JobIndexGroupInPeriodDTO
            //    {
            //        Id = c.Id,
            //        Name = c.Name,
            //        ParentId = c.ParentId
            //    }, null);

            var url = string.Format(baseAddress + makeApiAdress(periodId) + "?abstractId=" + abstractId);
            url += !string.IsNullOrWhiteSpace(columnNames) ? "&SelectedColumns=" + columnNames : "";
            WebClientHelper.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        }

        public void DeleteJobIndexGroupInPeriod(Action<string, Exception> action,long periodId, long abstractId)
        {
            var url = string.Format(baseAddress + makeApiAdress(periodId) + "?abstractId=" + abstractId);
            WebClientHelper.Delete(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void GetPeriodJobIndexes(Action<List<JobIndexGroupInPeriodDTO>, Exception> action, long periodId)
        {
            throw new NotImplementedException();
        }
    }
}
