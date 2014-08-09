using MITD.PMS.Presentation.Logic;
using MITD.Presentation.UI;

namespace MITD.PMS.Presentation.EmployeeManagement.Views
{
    public partial class EmployeeView : ViewBase,IEmployeeView
    {
        public EmployeeView()
        {
            InitializeComponent();
        }
        public EmployeeView(EmployeeVM vm)
        {
            InitializeComponent();
            ViewModel = vm;
        }


    }
}
