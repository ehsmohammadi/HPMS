using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public class AddPeriodService : IActionService<PeriodListVM>
    {
        private readonly IPeriodController periodController;

        public AddPeriodService(IPeriodController periodController)
        {
            this.periodController = periodController;
        }


        public void DoAction(PeriodListVM vm)
        {
            var Period = new PeriodDTO();
            periodController.ShowPeriodView(Period, ActionType.AddPeriod);
        }


    }
}

