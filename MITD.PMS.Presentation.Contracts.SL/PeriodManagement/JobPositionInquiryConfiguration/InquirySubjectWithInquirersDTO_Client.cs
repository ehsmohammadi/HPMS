using System.Collections.ObjectModel;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class InquirySubjectWithInquirersDTO : ViewModelBase
    {
        private ObservableCollection<InquirerDTO> inquirers;
        public ObservableCollection<InquirerDTO> Inquirers
        {
            get { return inquirers; }
            set { this.SetField(p => p.Inquirers, ref inquirers, value); }
        }


        private ObservableCollection<InquirerDTO> customInquirers;
        public ObservableCollection<InquirerDTO> CustomInquirers
        {
            get { return customInquirers; }
            set { this.SetField(p => p.CustomInquirers, ref customInquirers, value); }
        }
    }


}
