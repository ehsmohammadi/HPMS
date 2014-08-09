using System;
using System.Linq;
using System.Windows;
using MITD.PMS.Presentation.Contracts;
using System.Collections.Generic;
using MITD.Presentation;


namespace MITD.PMS.Presentation.Logic.Wrapper
{
    public class JobIndexServiceWrapper : IJobIndexServiceWrapper
    {
        private readonly IUserProvider userProvider;
        private readonly string apiAddress = PMSClientConfig.BaseApiAddress + "JobIndex";
        private const string jobIndexClassType = "JobIndex";
        private const string jobIndexCategoryClassType = "JobIndexCategory";


        public JobIndexServiceWrapper(IUserProvider userProvider)
        {
            this.userProvider = userProvider;
        }

        public void GetAllAbstractJobIndex(Action<List<AbstractJobIndexDTOWithActions>, Exception> action)
        {
            var url = string.Format(apiAddress );
            WebClientHelper.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
           
        }

        public void GetJobIndex(Action<JobIndexDTO, Exception> action, long id)
        {
            var url = string.Format(apiAddress + "?Id=" + id);
            WebClientHelper.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void AddJobIndex(Action<JobIndexDTO, Exception> action, JobIndexDTO jobIndex)
        {
            var url = string.Format(apiAddress);
            WebClientHelper.Post(new Uri(url, PMSClientConfig.UriKind), action, jobIndex, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void UpdateJobIndex(Action<JobIndexDTO, Exception> action, JobIndexDTO jobIndex)
        {
            var url = string.Format(apiAddress + "?Id=" + jobIndex.Id);
            WebClientHelper.Put(new Uri(url, UriKind.Absolute), action, jobIndex, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void DeleteJobIndex(Action<string, Exception> action, long id)
        {
            var url = string.Format(apiAddress + "?Id=" + id);
            WebClientHelper.Delete(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void GetAllJobIndex(Action<List<JobIndexDTO>, Exception> action)
        {
            var url = string.Format(apiAddress +"?typeOf="+jobIndexClassType );
            WebClientHelper.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void GetAllJobIndexCategorys(Action<PageResultDTO<JobIndexCategoryDTOWithActions>, Exception> action, int pageSize, int pageIndex)
        {
            var url = string.Format(apiAddress + "?typeOf=" + jobIndexCategoryClassType + "&PageSize=" + pageSize + "&PageIndex=" + pageIndex);
            WebClientHelper.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        }

        public void GetJobIndexCategory(Action<JobIndexCategoryDTO, Exception> action, long id)
        {
            var url = string.Format(apiAddress + "?Id=" + id);
            WebClientHelper.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        }

        public void AddJobIndexCategory(Action<JobIndexCategoryDTO, Exception> action, JobIndexCategoryDTO jobIndexCategory)
        {
            var url = string.Format(apiAddress , UriKind.Absolute);
            WebClientHelper.Post(new Uri(url), action, jobIndexCategory, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void UpdateJobIndexCategory(Action<JobIndexCategoryDTO, Exception> action, JobIndexCategoryDTO jobIndexCategory)
        {
            var url = string.Format(apiAddress+"?Id="+jobIndexCategory.Id);
            WebClientHelper.Put(new Uri(url, PMSClientConfig.UriKind), action, jobIndexCategory, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }
        
        public void DeleteJobIndexCategory(Action<string, Exception> action, long id)
        {
            var url = string.Format(apiAddress+ "?Id=" + id);
            WebClientHelper.Delete(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        }



       
    }
}
