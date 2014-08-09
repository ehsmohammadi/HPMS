
using System.Collections.Generic;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class JobPositionInquiryConfigurationDTO
    {

        private long periodId;
        public long PeriodId
        {
            get { return periodId; }
            set { this.SetField(p => p.PeriodId, ref periodId, value); }
        }

        private long jobPositionId;
        public long JobPositionId
        {
            get { return jobPositionId; }
            set { this.SetField(p => p.JobPositionId, ref jobPositionId, value); }
        }



        private List<InquirySubjectInquirersDTO> inquirySubjectInquirers;
        public List<InquirySubjectInquirersDTO> InquirySubjectInquirers
        {
            get { return inquirySubjectInquirers; }
            set { this.SetField(p => p.InquirySubjectInquirers, ref inquirySubjectInquirers, value); }
        }
       
    }
}
