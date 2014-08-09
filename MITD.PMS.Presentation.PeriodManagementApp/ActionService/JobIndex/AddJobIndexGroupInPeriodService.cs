using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public class AddJobIndexGroupInPeriodService : IActionService<JobIndexInPeriodTreeVM>
    {
        private readonly IPeriodController periodController;

        public AddJobIndexGroupInPeriodService(IPeriodController periodController)
        {
            this.periodController = periodController;
        }


        public void DoAction(JobIndexInPeriodTreeVM vm)
        {

            var jobIndexGroupInPeriod = new JobIndexGroupInPeriodDTO
                {
                    PeriodId = vm.Period.Id            
                };
            if (vm.SelectedAbstractIndexInPeriod == null)
                jobIndexGroupInPeriod.ParentId = null;
            else
                jobIndexGroupInPeriod.ParentId = vm.SelectedAbstractIndexInPeriod.Data.Id;


            periodController.ShowJobIndexGroupInPeriodView(jobIndexGroupInPeriod, ActionType.AddJobIndexGroupInPeriod);
        }


    }
}

