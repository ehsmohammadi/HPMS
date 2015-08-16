using System;
using System.Collections.Generic;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Integration.PMS.API
{
    public partial interface IJobPositionInPeriodServiceWrapper
    {
        void AddJobPositionInPeriod(Action<JobPositionInPeriodAssignmentDTO, Exception> action, JobPositionInPeriodAssignmentDTO unitInPeriod);
        void DeleteJobPositionInPeriod(Action<string, Exception> action, long periodId, long unitId);
        void GetAllJobPositions(Action<List<JobPositionInPeriodDTO>, Exception> action, long periodId);
        void GetJobPositionsWithActions(Action<List<JobPositionInPeriodDTOWithActions>, Exception> action, long periodId);

        void GetInquirySubjectWithInquirers(Action<List<InquirySubjectWithInquirersDTO>, Exception> action, long periodId, long jobPositionId);
        void UpdateInquirySubjectInquirers(Action<InquirySubjectWithInquirersDTO, Exception> action, long periodId, long jobPositionId, InquirySubjectWithInquirersDTO inquirySubjectWithInquirersDTO);
        void GetJobInPeriodByJobPostionId(Action<JobInPeriodDTO, Exception> action, long periodId, long jobPositionId);
    }
}
