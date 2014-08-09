using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;

namespace MITD.PMS.Presentation.BasicInfoApp
{
    public class AddJobService : IActionService<JobListVM>
    {
        private readonly IBasicInfoController basicInfoController;

        public AddJobService(IBasicInfoController basicInfoController)
        {
            this.basicInfoController = basicInfoController;
        }


        public void DoAction(JobListVM vm)
        {
            var job = new JobDTO();
            basicInfoController.ShowJobView(job, ActionType.AddJob);
        }


    }
}

