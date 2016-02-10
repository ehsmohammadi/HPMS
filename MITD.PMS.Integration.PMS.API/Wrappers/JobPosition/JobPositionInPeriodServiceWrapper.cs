using System;
using MITD.PMS.Integration.Core;
using MITD.PMS.Integration.PMS.Contract;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Integration.PMS.API
{
    public class JobPositionInPeriodServiceWrapper : IJobPositionInPeriodServiceWrapper
    {
        private readonly Uri apiUri = new Uri(PMSClientConfig.BaseApiAddress);
        private readonly IUserProvider userProvider;

        public JobPositionInPeriodServiceWrapper(IUserProvider userProvider)
        {
            this.userProvider = userProvider;
        }

        private string makeEndPoint(long periodId)
        {
            return "Periods/" + periodId + "/JobPositions";
        }



        public JobPositionInPeriodAssignmentDTO AddJobPositionInPeriod(JobPositionInPeriodAssignmentDTO jobPositionInPeriod)
        {
            return IntegrationHttpClient.Post<JobPositionInPeriodAssignmentDTO, JobPositionInPeriodAssignmentDTO>(apiUri, makeEndPoint(jobPositionInPeriod.PeriodId), jobPositionInPeriod);
        //    var url = string.Format(baseAddress + makeApiAdress(jobPositionInPeriod.PeriodId));
        //    IntegrationWebClient.Post(new Uri(url, PMSClientConfig.UriKind), action, jobPositionInPeriod, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        //private string makeJobPositionAssignmentApiAdress(long periodId, long jobPositionId)
        //{
        //    return "Periods/" + periodId + "/JobPositions/" + jobPositionId + "/Jobs";
        //}

        //public JobInPeriodDTO GetJobInPeriodByJobPostionId( long periodId, long jobPositionId)
        //{
        //    var url = string.Format(baseAddress + makeJobPositionAssignmentApiAdress(periodId, jobPositionId));
        //    IntegrationWebClient.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        //}

        //private string makeJobPositionInquirySubjectsApiAdress(long periodId, long jobPositionId)
        //{
        //    return "Periods/" + periodId + "/JobPositions/" + jobPositionId + "/InquirySubjects";
        //}

        //private string makeInquirySubjectInquirersApiAdress(long periodId, long jobPositionId, string inquirySubjectEmployeeNo)
        //{
        //    return "Periods/" + periodId + "/JobPositions/" + jobPositionId + "/InquirySubjects/" + inquirySubjectEmployeeNo + "/Inquirers";
        //}

        //public void DeleteJobPositionInPeriod(Action<string, Exception> action, long periodId, long jobPositionId)
        //{
        //    var url = string.Format(baseAddress + makeApiAdress(periodId) + "?JobPositionId=" + jobPositionId);
        //    IntegrationWebClient.Delete(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        //}

        //public void GetAllJobPositions(Action<List<JobPositionInPeriodDTO>, Exception> action, long periodId)
        //{
        //    var url = string.Format(baseAddress + makeApiAdress(periodId) + "?Type=JobPositionInPeriodDTO");
        //    IntegrationWebClient.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        //}

        //public void GetJobPositionsWithActions(Action<List<JobPositionInPeriodDTOWithActions>, Exception> action, long periodId)
        //{
        //    var url = string.Format(baseAddress + makeApiAdress(periodId) + "?Type=JobPositionInPeriodDTOWithActions");
        //    IntegrationWebClient.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        //}

        //public void GetInquirySubjectWithInquirers(Action<List<InquirySubjectWithInquirersDTO>, Exception> action, long periodId, long jobPositionId)
        //{
        //    var url = string.Format(baseAddress + makeJobPositionInquirySubjectsApiAdress(periodId, jobPositionId) + "?Include=Inquirers");
        //    IntegrationWebClient.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        //}

        //public void UpdateInquirySubjectInquirers(Action<InquirySubjectWithInquirersDTO, Exception> action, long periodId, long jobPositionId,
        //    InquirySubjectWithInquirersDTO inquirySubjectWithInquirersDTO)
        //{
        //    var url = string.Format(baseAddress + makeInquirySubjectInquirersApiAdress(periodId, jobPositionId, inquirySubjectWithInquirersDTO.EmployeeNo) + "?Batch=1");
        //    IntegrationWebClient.Put(new Uri(url, PMSClientConfig.UriKind), action, inquirySubjectWithInquirersDTO, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        //}

        
    }
}
