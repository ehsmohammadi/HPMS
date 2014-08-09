using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Presentation.Logic
{
    public class ManageRulesService : IActionService<PolicyVM>,IActionService<PolicyListVM>
    {
        private readonly IBasicInfoController basicInfoController;

        public ManageRulesService(IBasicInfoController basicInfoController)
        {
            this.basicInfoController = basicInfoController;
        }

        public void DoAction(PolicyVM vm)
        {
            var policy = vm.Policy;
            showRuleListView(policy.Id);
        }

        public void DoAction(PolicyListVM vm)
        {
            var policy = vm.SelectedPolicy;
            showRuleListView(policy.Id);
        }

        private void showRuleListView(long policyId)
        {
            basicInfoController.ShowRuleList(policyId);
        }
    }


    
}

