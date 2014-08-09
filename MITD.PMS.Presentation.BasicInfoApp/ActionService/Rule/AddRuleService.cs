using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;

namespace MITD.PMS.Presentation.BasicInfoApp
{
    public class AddRuleService : IActionService<GridRuleListVM>
    {
        private readonly IBasicInfoController basicInfoController;

        public AddRuleService(IBasicInfoController basicInfoController)
        {
            this.basicInfoController = basicInfoController;
        }


        public void DoAction(GridRuleListVM vm)
        {
            var rule = new RuleDTO{PolicyId=vm.PolicyRules.PolicyId};
            basicInfoController.ShowRuleView(rule, ActionType.AddRule);
        }


    }
}

