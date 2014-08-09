using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Presentation.Logic
{
    public class AddJobPositionInPeriodService : IActionService<JobPositionInPeriodTreeVM>
    {
        private readonly IPeriodController periodController;

        public AddJobPositionInPeriodService(IPeriodController periodController)
        {
            this.periodController = periodController;
        }


        public void DoAction(JobPositionInPeriodTreeVM vm)
        {
            var jobPositionInPeriod = new JobPositionInPeriodAssignmentDTO {PeriodId = vm.Period.Id};
            if (vm.SelectedJobPositionInPeriod != null)
            jobPositionInPeriod.ParentJobPositionId = vm.SelectedJobPositionInPeriod.Data.JobPositionId;
            else
            {
                jobPositionInPeriod.ParentJobPositionId = null;
            }

            periodController.ShowJobPositionInPeriodView(jobPositionInPeriod, ActionType.AddJobPositionInPeriod);
        }


    }
}

