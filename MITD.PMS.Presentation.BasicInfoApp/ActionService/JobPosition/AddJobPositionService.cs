using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;
using MITD.Presentation;

namespace MITD.PMS.Presentation.BasicInfoApp
{
    public class AddJobPositionService : IActionService<JobPositionListVM>
    {
        private readonly IBasicInfoController basicInfoController;

        public AddJobPositionService(IBasicInfoController basicInfoController)
        {
            this.basicInfoController = basicInfoController;
        }


        public void DoAction(JobPositionListVM vm)
        {
            var jobPosition = new JobPositionDTO();
            basicInfoController.ShowJobPositionView(jobPosition, ActionType.AddJobPosition);
        }


    }
}

