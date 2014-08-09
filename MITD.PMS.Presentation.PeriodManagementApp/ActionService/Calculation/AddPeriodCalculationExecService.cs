using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public class AddPeriodCalculationExecService : IActionService<CalculationListVM>
    {
        private readonly IPeriodController periodController;
        private readonly IPMSController pmsController;

        public AddPeriodCalculationExecService(IPeriodController periodController,IPMSController pmsController)
        {
            this.periodController = periodController;
            this.pmsController = pmsController;
        }


        public void DoAction(CalculationListVM vm)
        {
            var calculation = new CalculationDTO {PeriodId = vm.Period.Id};
            periodController.ShowPeriodCalculationExecView(calculation, ActionType.AddCalculation);
        }


    }
}

