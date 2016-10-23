using MITD.Presentation.UI;
using MITD.PMS.Presentation.Logic;



namespace MITD.PMS.Presentation.PeriodManagementApp.Views
{
    public partial class CalculationResultForManagerView : ViewBase, ICalculationResultForManagerView
    {
        public CalculationResultForManagerView()
        {
            InitializeComponent();
        }

        public CalculationResultForManagerView(CalculationResultForManagerVM vm)
        {
            InitializeComponent();
            ViewModel = vm;

        }
    }
}
