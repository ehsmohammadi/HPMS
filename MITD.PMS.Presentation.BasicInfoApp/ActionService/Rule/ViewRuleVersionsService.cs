using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;

namespace MITD.PMS.Presentation.BasicInfoApp
{
    public class ViewRuleVersionsService : IActionService<GridRuleListVM>
    {
        private readonly IBasicInfoController basicInfoController;

        public ViewRuleVersionsService(IBasicInfoController basicInfoController)
        {
            this.basicInfoController = basicInfoController;
        }


        public void DoAction(GridRuleListVM vm)
        {
            basicInfoController.ShowRuleVersionView(vm.SelectedRule, ActionEnum.ViewRuleVersions);
        }


    }
}

