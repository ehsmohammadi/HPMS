using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public class ShowPeriodCalculationResultDetailsService : IActionService<CalculationResultListVM>
    {
        private readonly IPeriodController periodController;
        private readonly ICalculationServiceWrapper calculationService;
        private readonly IPMSController pmsController;

        public ShowPeriodCalculationResultDetailsService(IPeriodController periodController,
            ICalculationServiceWrapper calculationService,IPMSController pmsController)
        {
            this.periodController = periodController;
            this.calculationService = calculationService;
            this.pmsController = pmsController;
        }


        public void DoAction(CalculationResultListVM vm)
        {
            //periodController.ShowPeriodCalculationResultView(vm.EmployeeCalculationResult.,
            //    vm.SelectedEmployeeCalculation.EmployeeNo);        
        }


    }
}

