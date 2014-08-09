using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;

namespace MITD.PMS.Presentation.PeriodManagementApp
{
    public class ShowPeriodCalculationStateService : IActionService<CalculationListVM>
    {
        private readonly IPeriodController periodController;
        private readonly IPMSController pmsController;
        private readonly ICalculationServiceWrapper calculationService;

        public ShowPeriodCalculationStateService(IPeriodController periodController
            , IPMSController pmsController, ICalculationServiceWrapper calculationService)
        {
            this.periodController = periodController;
            this.pmsController = pmsController;
            this.calculationService = calculationService;
        }

        public void DoAction(CalculationListVM vm)
        {
            periodController.ShowPeriodCalculationStateView(vm.SelectedCalculation.Id);
        }


    }



}

