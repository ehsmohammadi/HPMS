using System.Collections.Generic;
using MITD.Core;

namespace MITD.PMS.Presentation.Contracts
{
    public interface IUnitInquiryServiceFacade : IFacadeService
    {
        List<InquiryUnitDTO> GetInquirerInquirySubjects(long periodId, string inquirerEmployeeNo);

        InquiryUnitFormDTO GetInquiryForm(long periodId,string inquirerEmployeeNo,long unitId,
            long indexId);

        InquiryUnitFormDTO UpdateInquirySubjectForm(InquiryUnitFormDTO inquiryForm);
    }
}
