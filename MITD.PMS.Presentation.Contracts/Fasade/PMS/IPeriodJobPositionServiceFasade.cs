using System.Collections.Generic;
using MITD.Core;

namespace MITD.PMS.Presentation.Contracts
{
    public interface IPeriodJobPositionServiceFacade : IFacadeService
    {
        JobPositionInPeriodAssignmentDTO AssignJobPosition(long periodId, JobPositionInPeriodAssignmentDTO jobPositionInPeriod);
        string RemoveJobPosition(long periodId, long jobPositionId);
        IEnumerable<JobPositionInPeriodDTOWithActions> GetJobPositionsWithActions(long periodId);
        IEnumerable<JobPositionInPeriodDTO> GetJobPositions(long periodId);

        JobPositionInPeriodDTO GetJobPosition(long periodId, long jobPositionId);

        List<InquirySubjectWithInquirersDTO> GetInquirySubjectsWithInquirers(long periodId, long jobPositionId);

        InquirySubjectWithInquirersDTO UpdateInquirySubjectInquirers(long periodId, long jobPositionId,
            string inquirySubjectEmployeeNo, InquirySubjectWithInquirersDTO inquirySubjectWithInquirersDTO);

    }
}
