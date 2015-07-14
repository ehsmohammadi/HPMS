using MITD.Presentation.UI;
using MITD.PMS.Presentation.Logic;

namespace MITD.PMS.Presentation.PeriodManagementApp.Views
{
    public partial class UnitIndexGroupInPeriodView : ViewBase, IUnitIndexGroupInPeriodView
    {
        public UnitIndexGroupInPeriodView()
        {
            InitializeComponent();
        }

        public UnitIndexGroupInPeriodView(UnitIndexGroupInPeriodVM vm)
        {
            InitializeComponent();
            ViewModel = vm;
        }

    }
}
