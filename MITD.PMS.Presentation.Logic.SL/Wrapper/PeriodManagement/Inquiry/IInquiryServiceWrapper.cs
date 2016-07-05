using System.Collections.Generic;
using MITD.Presentation;
using MITD.PMS.Presentation.Contracts;
using System;

namespace MITD.PMS.Presentation.Logic
{
    public interface IInquiryServiceWrapper : IServiceWrapper
    {
        void GetInquirerInquirySubjects(Action<List<InquirySubjectDTO>, Exception> action, long periodId, string inquirerEmployeeNo);

        void GetInquirerInquiryIndices(Action<List<InquiryIndexDTO>, Exception> action, long periodId, string inquirerEmployeeNo);

        
        void GetInquiryForm(Action<InquiryFormDTO, Exception> action, long periodId, string inquirerEmployeeNo, long InquiererJobPositiobId,
            string inquirySubjectEmployeeNo, long jobPositionId);

        void GetInquiryFormByJobIndex(Action<InquiryFormByIndexDTO, Exception> action, long periodId, string inquirerEmployeeNo, long jobIndexId);

        void UpdateInquirySubjectForm(Action<InquiryFormDTO, Exception> action, InquiryFormDTO inquiryForm);



        
        void GetInquirySubjectSubEmployeesInquiryFormList(Action<InquirySubjectInquiryFormListDTO, Exception> action, long periodId,
                                              string inquirySubjectEmployeeNo, long inquirySubjectJobPositionId,
                                              string managerInquirerEmployeeNo, long managerInquirerJobPositionId);


        
    }
}
