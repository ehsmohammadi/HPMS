using MITD.Presentation.UI;
using MITD.PMS.Presentation.Logic;



namespace MITD.PMS.Presentation.PeriodManagementApp.Views
{
    public partial class CalculationResultView : ViewBase, ICalculationResultView
    {
        public CalculationResultView()
        {
            InitializeComponent();
        }

        public CalculationResultView(CalculationResultVM vm)
        {
            InitializeComponent();
            ViewModel = vm;

        }
    }
}
