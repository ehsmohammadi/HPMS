using MITD.PMS.Presentation.Logic;
using MITD.Presentation.UI;

namespace MITD.PMS.Presentation.BasicInfoApp.Views
{
    public partial class EmailInView : ViewBase, IEmailInView
    {
        public EmailInView()
        {
            InitializeComponent();
        }

        public EmailInView(EmailInVM vm)
        {
            InitializeComponent();
            ViewModel = vm;

        }
    }
}
