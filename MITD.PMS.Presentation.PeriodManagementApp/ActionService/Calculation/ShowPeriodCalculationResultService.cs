using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public class ShowCalculationResultService : IActionService<CalculationListVM>
    {
        private readonly IPeriodController periodController;

        public ShowCalculationResultService(IPeriodController periodController)
        {
            this.periodController = periodController;
        }


        public void DoAction(CalculationListVM vm)
        {
            periodController.ShowCalculationResultListView(vm.SelectedCalculation.Id,vm.Period.Id);
        }


    }
}

