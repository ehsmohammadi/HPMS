using System;
using System.Collections.Generic;
using MITD.PMS.Integration.Core;
using MITD.PMS.Integration.PMS.Contract;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Integration.PMS.API
{
    public class JobIndexServiceWrapper : IJobIndexServiceWrapper
    {

        #region Fields
        private readonly string endpoint = "JobIndex";
        private readonly Uri apiUri = new Uri(PMSClientConfig.BaseApiAddress);
        #endregion

        #region Not Use
        private readonly IUserProvider userProvider;
        private const string jobIndexClassType = "JobIndex";
        private const string jobIndexCategoryClassType = "JobIndexCategory";
        #endregion

        #region Constructors
        public JobIndexServiceWrapper(IUserProvider userProvider)
        {
            this.userProvider = userProvider;
        }
        #endregion

        #region Public Methods
        public JobIndexDTO GetJobIndex(long id)
        {
            return IntegrationHttpClient.Get<JobIndexDTO>(apiUri, endpoint + "?Id=" + id);
            //var url = string.Format(apiAddress + "?Id=" + id);
            //IntegrationWebClient.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public JobIndexDTO GetByTransferId(Guid transferId)
        {
            return IntegrationHttpClient.Get<JobIndexDTO>(apiUri, endpoint + "?TransferId=" + transferId);
        }

        public JobIndexDTO AddJobIndex(JobIndexDTO jobIndex)
        {
            return IntegrationHttpClient.Post<JobIndexDTO, JobIndexDTO>(apiUri, endpoint, jobIndex);
            //var url = string.Format(apiAddress);
            //IntegrationWebClient.Post(new Uri(url, PMSClientConfig.UriKind), action, jobIndex, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        #endregion

        //public void GetAllAbstractJobIndex(Action<List<AbstractJobIndexDTOWithActions>, Exception> action)
        //{
        //    var url = string.Format(apiAddress);
        //    IntegrationWebClient.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        //}

        //public void UpdateJobIndex(Action<JobIndexDTO, Exception> action, JobIndexDTO jobIndex)
        //{
        //    var url = string.Format(apiAddress + "?Id=" + jobIndex.Id);
        //    IntegrationWebClient.Put(new Uri(url, UriKind.Absolute), action, jobIndex, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        //}

        //public void DeleteJobIndex(Action<string, Exception> action, long id)
        //{
        //    var url = string.Format(apiAddress + "?Id=" + id);
        //    IntegrationWebClient.Delete(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        //}

        //public void GetAllJobIndex(Action<List<JobIndexDTO>, Exception> action)
        //{
        //    var url = string.Format(apiAddress + "?typeOf=" + jobIndexClassType);
        //    IntegrationWebClient.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        //}

        //public void GetAllJobIndexCategorys(Action<PageResultDTO<JobIndexCategoryDTOWithActions>, Exception> action, int pageSize, int pageIndex)
        //{
        //    var url = string.Format(apiAddress + "?typeOf=" + jobIndexCategoryClassType + "&PageSize=" + pageSize + "&PageIndex=" + pageIndex);
        //    IntegrationWebClient.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        //}

        //public void GetJobIndexCategory(Action<JobIndexCategoryDTO, Exception> action, long id)
        //{
        //    var url = string.Format(apiAddress + "?Id=" + id);
        //    IntegrationWebClient.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        //}

        //public void AddJobIndexCategory(Action<JobIndexCategoryDTO, Exception> action, JobIndexCategoryDTO jobIndexCategory)
        //{
        //    var url = string.Format(apiAddress, UriKind.Absolute);
        //    IntegrationWebClient.Post(new Uri(url), action, jobIndexCategory, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        //}

        //public void UpdateJobIndexCategory(Action<JobIndexCategoryDTO, Exception> action, JobIndexCategoryDTO jobIndexCategory)
        //{
        //    var url = string.Format(apiAddress + "?Id=" + jobIndexCategory.Id);
        //    IntegrationWebClient.Put(new Uri(url, PMSClientConfig.UriKind), action, jobIndexCategory, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        //}

        //public void DeleteJobIndexCategory(Action<string, Exception> action, long id)
        //{
        //    var url = string.Format(apiAddress + "?Id=" + id);
        //    IntegrationWebClient.Delete(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        //}
    }
}
