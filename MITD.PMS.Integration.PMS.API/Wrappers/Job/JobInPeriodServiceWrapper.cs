using System;
using MITD.PMS.Integration.Core;
using MITD.PMS.Integration.PMS.Contract;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Integration.PMS.API
{
    public class JobInPeriodServiceWrapperWrapper : IJobInPeriodServiceWrapper
    {
        #region Fields

        private readonly Uri apiUri = new Uri(PMSClientConfig.BaseApiAddress);

        private readonly IUserProvider userProvider;
        #endregion

        #region Constructors
        public JobInPeriodServiceWrapperWrapper(IUserProvider userProvider)
        {
            this.userProvider = userProvider;
        }
        #endregion

        #region Private methods
        private string makeEndPoint(long periodId)
        {
            return "Periods/" + periodId + "/Jobs";
        }
        #endregion

        #region Public Methods
        public JobInPeriodDTO AddJobInPeriod(long periodId, JobInPeriodDTO jobInPeriodDto)
        {
            return IntegrationHttpClient.Post<JobInPeriodDTO, JobInPeriodDTO>(apiUri, makeEndPoint(periodId), jobInPeriodDto);
            //var url = string.Format(baseAddress + makeApiAdress(periodId));
            //IntegrationWebClient.Post(new Uri(url, PMSClientConfig.UriKind), action, jobInPeriodDto, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        }

        public JobInPeriodDTO GetJobInPeriod(long periodId, long jobId)
        {
            return GetJobInPeriod(periodId, jobId, "");
        }



        public JobInPeriodDTO GetJobInPeriod(long periodId, long jobId, string columnNames)
        {
            var endpoint = makeEndPoint(periodId) + "?jobId=" + jobId;
            endpoint += !string.IsNullOrWhiteSpace(columnNames) ? "&SelectedColumns=" + columnNames : "";
            return IntegrationHttpClient.Get<JobInPeriodDTO>(apiUri, endpoint);
            //var url = string.Format(baseAddress + makeApiAdress(periodId) + "?jobId=" + jobId);
            //  url += !string.IsNullOrWhiteSpace(columnNames) ? "&SelectedColumns=" + columnNames : "";
            //IntegrationWebClient.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        }
        #endregion

        //public void AddJobInPeriod(Action<JobInPeriodAssignmentDTO, Exception> action, JobInPeriodAssignmentDTO jobInPeriod)
        //{
        //    var url = string.Format(baseAddress + makeApiAdress(jobInPeriod.PeriodId));
        //    IntegrationWebClient.Post(new Uri(url, PMSClientConfig.UriKind), action, jobInPeriod, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        //}

        //public void DeleteJobInPeriod(Action<string, Exception> action, long periodId, long jobId)
        //{
        //    var url = string.Format(baseAddress + makeApiAdress(periodId) + "?JobId=" + jobId);
        //    IntegrationWebClient.Delete(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        //}

        //public void GetAllJobs(Action<List<JobInPeriodDTO>, Exception> action, long periodId)
        //{
        //    var url = string.Format(baseAddress + makeApiAdress(periodId) + "?Type=JobInPeriodDTO");
        //    IntegrationWebClient.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        //}

        //public void GetJobsWithActions(Action<List<JobInPeriodDTOWithActions>, Exception> action, long periodId)
        //{
        //    var url = string.Format(baseAddress + makeApiAdress(periodId) + "?Type=JobInPeriodDTOWithActions");
        //    IntegrationWebClient.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        //}

        //public void GetAllJobInPeriod(Action<IList<JobInPeriodDTO>, Exception> action, long periodId, string columnNames)
        //{
        //    var url = string.Format(baseAddress + makeApiAdress(periodId) + "?Type=jobInPeriodDTO");
        //    // url += !string.IsNullOrWhiteSpace(columnNames) ? "&SelectedColumns=" + columnNames : "";
        //    IntegrationWebClient.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        //}

        //public void GetAllJobInPeriod(Action<IList<JobInPeriodDTO>, Exception> action, long periodId)
        //{
        //    GetAllJobInPeriod(action, periodId, "");
        //}

        //public void UpdateJobInPeriod(Action<JobInPeriodDTO, Exception> action, long periodId, JobInPeriodDTO jobInPeriodDto)
        //{

        //    var url = string.Format(baseAddress + makeApiAdress(periodId));
        //    IntegrationWebClient.Put(new Uri(url, PMSClientConfig.UriKind), action, jobInPeriodDto, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        //}


        //public void AddInquirer(Action<string, Exception> action, long periodId, long jobId, string personalNo, long jobIndexInJobId)
        //{
        //    throw new NotImplementedException();
        //}

        //public void UpdateInquirySubjectInquirers(Action<InquirySubjectWithInquirersDTO, Exception> action, long periodId, long jobId,
        //    InquirySubjectWithInquirersDTO inquirySubjectWithInquirersDTO)
        //{
        //    throw new NotImplementedException();
        //}

        //public void GetInquirySubjectWithInquirers(Action<List<InquirySubjectWithInquirersDTO>, Exception> action, long periodId, long jobId)
        //{
        //    var url = string.Format(baseAddress + makeJobPositionInquirySubjectsApiAdress(periodId, jobId) + "?Include=Inquirers");
        //    IntegrationWebClient.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        //}

        //private string makeJobPositionInquirySubjectsApiAdress(long periodId, long jobId)
        //{
        //    return "Periods/" + periodId + "/Jobs/" + jobId + "/InquirySubjects";
        //}

        //public void AddInquirer(Action<string, Exception> action, long periodId, long jobId, string personalNo)
        //{
        //    var url = string.Format(baseAddress + makeJobPositionInquirySubjectsApiAdress(periodId, jobId) + "?employeeNo=" + personalNo);
        //    IntegrationWebClient.Put(new Uri(url, PMSClientConfig.UriKind), action, personalNo, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        //}


        //public void DeleteInquirer(Action<string, Exception> action, long periodId, long jobId, string personalNo)
        //{
        //    var url = string.Format(baseAddress + makeJobPositionInquirySubjectsApiAdress(periodId, jobId) + "?employeeNo=" + personalNo);
        //    IntegrationWebClient.Delete(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        //}

    }
}
