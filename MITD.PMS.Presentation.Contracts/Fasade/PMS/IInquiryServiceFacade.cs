using System.Collections.Generic;
using MITD.Core;

namespace MITD.PMS.Presentation.Contracts
{
    public interface IInquiryServiceFacade : IFacadeService
    {
        List<InquirySubjectDTO> GetInquirerInquirySubjects(long periodId, string inquirerEmployeeNo);

        InquiryFormDTO GetInquiryForm(long periodId,long inquirerJobPositionId, string inquirerEmployeeNo, string inquirySubjectEmployeeNo,
            long jobPositionId);

        InquiryFormDTO UpdateInquirySubjectForm(InquiryFormDTO inquiryForm);
    }
}
