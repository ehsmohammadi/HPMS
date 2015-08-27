using System.Collections.Generic;
using MITD.Presentation;
using MITD.PMS.Presentation.Contracts;
using System;

namespace MITD.PMS.Presentation.Logic
{
    public interface IUnitInquiryServiceWrapper : IServiceWrapper
    {
        void GetInquirerInquirySubjects(Action<List<InquiryUnitDTO>, Exception> action, long periodId, string inquirerEmployeeNo);

        void GetInquiryForm(Action<InquiryUnitDTO, Exception> action, long periodId, string inquirerEmployeeNo, long InquiererJobPositiobId,
            string inquirySubjectEmployeeNo, long jobPositionId);

        void UpdateInquirySubjectForm(Action<InquiryUnitFormDTO, Exception> action, InquiryUnitFormDTO inquiryForm);

        
        void GetInquirySubjectSubEmployeesInquiryFormList(Action<InquirySubjectInquiryFormListDTO, Exception> action, long periodId,
                                              string inquirySubjectEmployeeNo, long inquirySubjectJobPositionId,
                                              string managerInquirerEmployeeNo, long managerInquirerJobPositionId);
    }
}
