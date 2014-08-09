using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public class AddJobIndexInPeriodService : IActionService<JobIndexInPeriodTreeVM>
    {
        private readonly IPeriodController periodController;

        public AddJobIndexInPeriodService(IPeriodController periodController)
        {
            this.periodController = periodController;
        }


        public void DoAction(JobIndexInPeriodTreeVM vm)
        {
            if (vm.SelectedAbstractIndexInPeriod == null)
                return;
            var jobIndexInPeriod = new JobIndexInPeriodDTO();
            jobIndexInPeriod.PeriodId = vm.Period.Id; //vm.PeriodAbstractIndexes.PeriodId;
            jobIndexInPeriod.ParentId = vm.SelectedAbstractIndexInPeriod.Data.Id;
            
            periodController.ShowJobIndexInPeriodView(jobIndexInPeriod, ActionType.AddJobIndexInPeriod);
        }


    }
}

