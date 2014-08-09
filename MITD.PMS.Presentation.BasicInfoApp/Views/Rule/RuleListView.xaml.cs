using MITD.Presentation.UI;
using MITD.PMS.Presentation.Logic;

namespace MITD.PMS.Presentation.BasicInfoApp.Views
{
    public partial class RuleListView : ViewBase, IRuleListView
    {
        public RuleListView()
        {
            InitializeComponent();
            
        }
        public RuleListView(RuleListVM vm)
        {
            InitializeComponent();
            ViewModel = vm;
        }
        

       
    }
}
