using MITD.Presentation.UI;
using MITD.PMS.Presentation.Logic;

namespace MITD.PMS.Presentation.BasicInfoApp.Views
{
    public partial class RuleVersionView : ViewBase, IRuleVersionView
    {
        public RuleVersionView()
        {
            InitializeComponent();
        }

        public RuleVersionView(RuleVersionVM vm)
        {
            InitializeComponent();
            ViewModel = vm;
        }

    }
}
