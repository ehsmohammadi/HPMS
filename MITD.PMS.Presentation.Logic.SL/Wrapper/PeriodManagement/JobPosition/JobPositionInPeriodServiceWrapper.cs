using System;
using MITD.PMS.Presentation.Contracts;
using System.Collections.Generic;
using MITD.Presentation;


namespace MITD.PMS.Presentation.Logic.Wrapper
{
    public class JobPositionInPeriodServiceWrapper : IJobPositionInPeriodServiceWrapper
    {
        private readonly IUserProvider userProvider;
        private readonly string baseAddress = PMSClientConfig.BaseApiAddress;

        public JobPositionInPeriodServiceWrapper(IUserProvider userProvider)
        {
            this.userProvider = userProvider;
        }

        private string makeApiAdress(long periodId)
        {
            return "Periods/" + periodId + "/JobPositions";
        }

        private string makeJobPositionInquirySubjectsApiAdress(long periodId, long jobPositionId)
        {
            return "Periods/" + periodId + "/JobPositions/" + jobPositionId + "/InquirySubjects";
        }

        private string makeJobPositionAssignmentApiAdress(long periodId, long jobPositionId)
        {
            return "Periods/" + periodId + "/JobPositions/" + jobPositionId + "/Jobs";
        }

        private string makeInquirySubjectInquirersApiAdress(long periodId, long jobPositionId,string inquirySubjectEmployeeNo)
        {
            return "Periods/" + periodId + "/JobPositions/" + jobPositionId + "/InquirySubjects/" + inquirySubjectEmployeeNo+"/Inquirers";
        }

        public void AddJobPositionInPeriod(Action<JobPositionInPeriodAssignmentDTO, Exception> action, JobPositionInPeriodAssignmentDTO jobPositionInPeriod)
        {
            var url = string.Format(baseAddress + makeApiAdress(jobPositionInPeriod.PeriodId));
            WebClientHelper.Post(new Uri(url, PMSClientConfig.UriKind), action, jobPositionInPeriod, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void UpdateJobPositionInPeriod(Action<JobPositionInPeriodAssignmentDTO, Exception> action, JobPositionInPeriodAssignmentDTO jobPositionInPeriod)
        {
            var url = string.Format(baseAddress + makeApiAdress(jobPositionInPeriod.PeriodId));
            WebClientHelper.Put(new Uri(url, PMSClientConfig.UriKind), action, jobPositionInPeriod, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void DeleteJobPositionInPeriod(Action<string, Exception> action, long periodId, long jobPositionId)
        {
            var url = string.Format(baseAddress + makeApiAdress(periodId) + "?JobPositionId=" + jobPositionId);
            WebClientHelper.Delete(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void GetAllJobPositions(Action<List<JobPositionInPeriodDTO>, Exception> action, long periodId)
        {
            var url = string.Format(baseAddress + makeApiAdress(periodId) + "?Type=JobPositionInPeriodDTO");
            WebClientHelper.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void GetJobPositionsWithActions(Action<List<JobPositionInPeriodDTOWithActions>, Exception> action, long periodId)
        {
            var url = string.Format(baseAddress + makeApiAdress(periodId) + "?Type=JobPositionInPeriodDTOWithActions");
            WebClientHelper.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void GetInquirySubjectWithInquirers(Action<List<InquirySubjectWithInquirersDTO>, Exception> action, long periodId, long jobPositionId)
        {
            var url = string.Format(baseAddress + makeJobPositionInquirySubjectsApiAdress(periodId,jobPositionId)+"?Include=Inquirers" );
            WebClientHelper.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void UpdateInquirySubjectInquirers(Action<InquirySubjectWithInquirersDTO, Exception> action, long periodId, long jobPositionId,
            InquirySubjectWithInquirersDTO inquirySubjectWithInquirersDTO)
        {
            var url = string.Format(baseAddress + makeInquirySubjectInquirersApiAdress(periodId,jobPositionId,inquirySubjectWithInquirersDTO.EmployeeNo) + "?Batch=1");
            WebClientHelper.Put(new Uri(url, PMSClientConfig.UriKind), action, inquirySubjectWithInquirersDTO, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void GetJobInPeriodByJobPostionId(Action<JobInPeriodDTO, Exception> action,long periodId, long jobPositionId)
        {
            var url = string.Format(baseAddress + makeJobPositionAssignmentApiAdress(periodId, jobPositionId) );
            WebClientHelper.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void GetJobPositionInPeriod(Action<JobPositionInPeriodAssignmentDTO, Exception> action, long periodId, long jobPositionId)
        {
            var url = string.Format(baseAddress + makeApiAdress(periodId)+"?jobPositionId="+jobPositionId);
            WebClientHelper.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }
    }
}
