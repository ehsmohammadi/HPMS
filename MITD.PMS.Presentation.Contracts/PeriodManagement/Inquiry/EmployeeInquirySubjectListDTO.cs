
using System.Collections.Generic;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class EmployeeInquirySubjectListDTO
    {

        private long employeeId;
        public long EmployeeId
        {
            get { return employeeId; }
            set { this.SetField(p => p.EmployeeId, ref employeeId, value); }
        }

        private PageResultDTO<InquirySubjectDTO> inquirySubjects;
        public PageResultDTO<InquirySubjectDTO> InquirySubjects
        {
            get { return inquirySubjects; }
            set { this.SetField(p => p.InquirySubjects, ref inquirySubjects, value); }
        }
       
    }
}
