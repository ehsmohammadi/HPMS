using System.Collections.Generic;
using MITD.Presentation;
using MITD.PMS.Presentation.Contracts;
using System;

namespace MITD.PMS.Presentation.Logic
{
    public interface IUnitInquiryServiceWrapper : IServiceWrapper
    {
        void GetInquirerInquirySubjects(Action<List<InquiryUnitDTO>, Exception> action, long periodId, string inquirerEmployeeNo);

        void GetInquiryForm(Action<InquiryUnitFormDTO, Exception> action, long periodId, string inquirerEmployeeNo,
       long unitId);

        void UpdateInquirySubjectForm(Action<InquiryUnitFormDTO, Exception> action, InquiryUnitFormDTO inquiryForm);

        
        void GetInquirySubjectSubEmployeesInquiryFormList(Action<InquirySubjectInquiryFormListDTO, Exception> action, long periodId,
                                              string inquirySubjectEmployeeNo, long inquirySubjectJobPositionId,
                                              string managerInquirerEmployeeNo, long managerInquirerJobPositionId);
    }
}
