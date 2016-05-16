using MITD.PMS.Presentation.Logic;
using MITD.Presentation.UI;

namespace MITD.PMS.Presentation.BasicInfoApp.Views
{
    public partial class ChangePassword : ViewBase, IChangePasswordView
    {
        public ChangePassword()
        {
            InitializeComponent();
        }

        public ChangePassword(ChangePasswordVM vm)
        {
            InitializeComponent();
            ViewModel = vm;

        }
    }
}
