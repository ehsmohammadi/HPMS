using MITD.Presentation.UI;
using MITD.PMS.Presentation.Logic;

namespace MITD.PMS.Presentation.PeriodManagementApp.Views
{
    public partial class JobIndexGroupInPeriodView : ViewBase, IJobIndexGroupInPeriodView
    {
        public JobIndexGroupInPeriodView()
        {
            InitializeComponent();
        }

        public JobIndexGroupInPeriodView(JobIndexGroupInPeriodVM vm)
        {
            InitializeComponent();
            ViewModel = vm;
        }

    }
}
