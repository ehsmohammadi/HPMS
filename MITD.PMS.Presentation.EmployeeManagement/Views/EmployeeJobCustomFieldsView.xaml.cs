using MITD.PMS.Presentation.Logic;
using MITD.Presentation.UI;

namespace MITD.PMS.Presentation.EmployeeManagement.Views
{
    public partial class EmployeeJobCustomFieldsView : ViewBase,IEmployeeJobCustomFieldsView
    {
        public EmployeeJobCustomFieldsView()
        {
            InitializeComponent();
        }
        public EmployeeJobCustomFieldsView(EmployeeJobCustomFieldsVM vm)
        {
            InitializeComponent();
            ViewModel = vm;
        }


    }
}
