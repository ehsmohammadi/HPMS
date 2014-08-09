using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Presentation.Logic
{
    public class ManageFunctionsService : IActionService<PolicyVM>,IActionService<PolicyListVM>
    {
        private readonly IBasicInfoController basicInfoController;

        public ManageFunctionsService(IBasicInfoController basicInfoController)
        {
            this.basicInfoController = basicInfoController;
        }

        public void DoAction(PolicyVM vm)
        {
            var policy = vm.Policy;
            showFunctionListView(policy.Id);
        }

        public void DoAction(PolicyListVM vm)
        {
            var policy = vm.SelectedPolicy;
            showFunctionListView(policy.Id);
        }

        private void showFunctionListView(long policyId)
        {
            basicInfoController.ShowFunctionListView(policyId);
        }
    }


    
}

