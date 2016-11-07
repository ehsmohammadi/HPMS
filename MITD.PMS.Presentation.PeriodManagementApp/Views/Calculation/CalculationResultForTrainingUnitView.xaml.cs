using MITD.Presentation.UI;
using MITD.PMS.Presentation.Logic;



namespace MITD.PMS.Presentation.PeriodManagementApp.Views
{
    public partial class CalculationResultForTrainingUnitView : ViewBase, ICalculationResultForTrainingUnitView
    {
        public CalculationResultForTrainingUnitView()
        {
            InitializeComponent();
        }

        public CalculationResultForTrainingUnitView(CalculationResultForTrainingUnitVM vm)
        {
            InitializeComponent();
            ViewModel = vm;

        }
    }
}
