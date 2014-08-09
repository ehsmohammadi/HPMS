using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public class AddJobInPeriodService : IActionService<JobInPeriodListVM>
    {
        private readonly IPeriodController periodController;

        public AddJobInPeriodService(IPeriodController periodController)
        {
            this.periodController = periodController;
        }


        public void DoAction(JobInPeriodListVM vm)
        {
            periodController.ShowJobInPeriodView(vm.Period.Id,null, ActionType.AddJobInPeriod);
        }


    }
}

