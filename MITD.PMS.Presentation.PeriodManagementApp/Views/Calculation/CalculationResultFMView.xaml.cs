using MITD.Presentation.UI;
using MITD.PMS.Presentation.Logic;



namespace MITD.PMS.Presentation.PeriodManagementApp.Views
{
    public partial class CalculationResultFMView : ViewBase, ICalculationResultView
    {
        public CalculationResultFMView()
        {
            InitializeComponent();
        }

        public CalculationResultFMView(CalculationResultVM vm)
        {
            InitializeComponent();
            ViewModel = vm;

        }
    }
}
