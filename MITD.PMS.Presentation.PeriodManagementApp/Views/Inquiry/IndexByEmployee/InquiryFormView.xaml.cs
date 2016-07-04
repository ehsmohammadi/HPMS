using MITD.PMS.Presentation.Logic;
using MITD.Presentation.UI;

namespace MITD.PMS.Presentation.PeriodManagementApp.Views
{
    public partial class InquiryFormView : ViewBase, IInquiryFormView
    {




        public InquiryFormView()
        {
           InitializeComponent();
        }

        public InquiryFormView(InquiryFormVM vm)
        {
            InitializeComponent();
            ViewModel = vm;

        }

    }
}
