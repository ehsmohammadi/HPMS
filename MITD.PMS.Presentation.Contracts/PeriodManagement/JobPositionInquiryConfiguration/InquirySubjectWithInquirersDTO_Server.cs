
using System.Collections.Generic;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class InquirySubjectWithInquirersDTO : DTOBase
    {
        private List<InquirerDTO> inquirers;
        public List<InquirerDTO> Inquirers
        {
            get { return inquirers; }
            set { this.SetField(p => p.Inquirers, ref inquirers, value); }
        }


        private List<InquirerDTO> customInquirers;
        public List<InquirerDTO> CustomInquirers
        {
            get { return customInquirers; }
            set { this.SetField(p => p.CustomInquirers, ref customInquirers, value); }
        }
       
    }
}
