using MITD.PMS.Presentation.Logic;
using MITD.Presentation.UI;

namespace MITD.PMS.Presentation.PeriodManagementApp.Views
{
    public partial class JobIndexInquiryFormView : ViewBase, IJobIndexInquiryFormView
    {

        public JobIndexInquiryFormView()
        {
           InitializeComponent();
        }

        public JobIndexInquiryFormView(JobIndexInquiryFormVM vm)
        {
            InitializeComponent();
            ViewModel = vm;

        }

    }
}
