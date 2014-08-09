using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;

namespace MITD.PMS.Presentation.BasicInfoApp
{
    public class AddJobIndexService : IActionService<JobIndexTreeVM>
    {
        private readonly IBasicInfoController basicInfoController;

        public AddJobIndexService(IBasicInfoController basicInfoController)
        {
            this.basicInfoController = basicInfoController;
        }


        public void DoAction(JobIndexTreeVM vm)
        {
            var jobIndex = new JobIndexDTO();
            if (vm.SelectedJobIndex != null)
                jobIndex.ParentId = vm.SelectedJobIndex.Data.Id;
            basicInfoController.ShowJobIndexView(jobIndex, ActionType.AddJobIndex);
        }


    }
}

