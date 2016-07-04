using System.Collections.Generic;
using MITD.Core;

namespace MITD.PMS.Presentation.Contracts
{
    public interface IInquiryServiceFacade : IFacadeService
    {
        //Get by emplyee
        List<InquirySubjectDTO> GetInquirerInquirySubjects(long periodId, string inquirerEmployeeNo);

        //Get by index
        List<InquiryIndexDTO> GetInquirerInquiryIndices(long periodId, string inquirerEmployeeNo);

        InquiryFormDTO GetInquiryForm(long periodId,long inquirerJobPositionId, string inquirerEmployeeNo, string inquirySubjectEmployeeNo,
            long jobPositionId);

        InquiryFormDTO UpdateInquirySubjectForm(InquiryFormDTO inquiryForm);
        
    }
}
