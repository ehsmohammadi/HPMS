using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;

namespace MITD.PMS.Presentation.BasicInfoApp
{ 
    public class AddJobIndexCategoryService : IActionService<JobIndexTreeVM>
    {
        private readonly IBasicInfoController basicInfoController;

        public AddJobIndexCategoryService(IBasicInfoController basicInfoController)
        {
            this.basicInfoController = basicInfoController;
        }


        public void DoAction(JobIndexTreeVM vm)
        {
            var jobIndexCategory = new JobIndexCategoryDTO();
            if (vm.SelectedJobIndex != null)
                jobIndexCategory.ParentId = vm.SelectedJobIndex.Data.Id;
            else
                jobIndexCategory.ParentId = null;
            basicInfoController.ShowJobIndexCategoryView(jobIndexCategory, ActionType.AddJobIndexCategory);
        }
    }
}

