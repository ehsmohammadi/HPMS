using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;

namespace MITD.PMS.Presentation.BasicInfoApp
{
    public class AddFunctionService : IActionService<FunctionListVM>
    {
        private readonly IBasicInfoController basicInfoController;

        public AddFunctionService(IBasicInfoController basicInfoController)
        {
            this.basicInfoController = basicInfoController;
        }


        public void DoAction(FunctionListVM vm)
        {
            var function = new FunctionDTO(){PolicyId=vm.PolicyFunctions.PolicyId};
            basicInfoController.ShowFunctionView(function, ActionType.AddFunction);
        }


    }
}

