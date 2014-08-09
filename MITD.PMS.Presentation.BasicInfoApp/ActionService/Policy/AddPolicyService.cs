using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Presentation.Logic
{
    public class AddPolicyService : IActionService<PolicyListVM>
    {
        private readonly IBasicInfoController basicInfoController;

        public AddPolicyService(IBasicInfoController basicInfoController)
        {
            this.basicInfoController = basicInfoController;
        }

        public void DoAction(PolicyListVM vm)
        {
            var policy = new PolicyDTO();
            basicInfoController.ShowPolicyView(policy, ActionType.AddPolicy);
        }


    }
}

