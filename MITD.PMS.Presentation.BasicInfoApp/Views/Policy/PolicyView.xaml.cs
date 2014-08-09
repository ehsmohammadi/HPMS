using MITD.Presentation.UI;
using MITD.PMS.Presentation.Logic;

namespace MITD.PMS.Presentation.BasicInfoApp.Views
{
    public partial class PolicyView : ViewBase, IPolicyView
    {
        public PolicyView()
        {
            InitializeComponent();
        }

        public PolicyView(PolicyVM vm)
        {
            InitializeComponent();
            ViewModel = vm;
        }

    }
}
