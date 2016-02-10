using System;
using MITD.PMS.Presentation.Contracts;
using MITD.Presentation;

namespace MITD.PMS.Integration.PMS.Contract
{
    public partial interface IJobPositionInPeriodServiceWrapper : IServiceWrapper
    {
        JobPositionInPeriodAssignmentDTO AddJobPositionInPeriod(JobPositionInPeriodAssignmentDTO jobInPeriod);

        //JobInPeriodDTO GetJobInPeriodByJobPostionId(long periodId, long jobPositionId);

        //void DeleteJobPositionInPeriod(Action<string, Exception> action, long periodId, long unitId);
        //void GetAllJobPositions(Action<List<JobPositionInPeriodDTO>, Exception> action, long periodId);
        //void GetJobPositionsWithActions(Action<List<JobPositionInPeriodDTOWithActions>, Exception> action, long periodId);

        //void GetInquirySubjectWithInquirers(Action<List<InquirySubjectWithInquirersDTO>, Exception> action, long periodId, long jobPositionId);
        //void UpdateInquirySubjectInquirers(Action<InquirySubjectWithInquirersDTO, Exception> action, long periodId, long jobPositionId, InquirySubjectWithInquirersDTO inquirySubjectWithInquirersDTO);
        
    }
}
