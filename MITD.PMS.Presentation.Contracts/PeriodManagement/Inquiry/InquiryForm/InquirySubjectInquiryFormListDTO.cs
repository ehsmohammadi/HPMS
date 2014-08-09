
using System.Collections.Generic;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class InquirySubjectInquiryFormListDTO
    {
        private long periodId;
        public long PeriodId
        {
            get { return periodId; }
            set { this.SetField(p => p.PeriodId, ref periodId, value); }
        }

        private long inquirySubjectJobPositionId;
        public long InquirySubjectJobPositionId
        {
            get { return inquirySubjectJobPositionId; }
            set { this.SetField(p => p.InquirySubjectJobPositionId, ref inquirySubjectJobPositionId, value); }
        }

        private long inquirySubjectFname;
        public long InquirySubjectFname
        {
            get { return inquirySubjectFname; }
            set { this.SetField(p => p.InquirySubjectFname, ref inquirySubjectFname, value); }
        }

        private long inquirySubjectLname;
        public long InquirySubjectLname
        {
            get { return inquirySubjectLname; }
            set { this.SetField(p => p.InquirySubjectLname, ref inquirySubjectLname, value); }
        }

        private string inquirySubjectEmployeeNo;
        public string InquirySubjectEmployeeNo
        {
            get { return inquirySubjectEmployeeNo; }
            set { this.SetField(p => p.InquirySubjectEmployeeNo, ref inquirySubjectEmployeeNo, value); }
        }


        private List<InquiryFormInquirerDTO> inquirerList;
        public List<InquiryFormInquirerDTO> InquirerList
        {
            get { return inquirerList; }
            set { this.SetField(p => p.InquirerList, ref inquirerList, value); }
        }


       
    }
}
