using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public class ModifyFunctionService:IActionService<FunctionListVM>
    {
        private readonly IBasicInfoController basicInfoController;
        private readonly IPMSController pmsController;
        private readonly IFunctionServiceWrapper functionService;


        public ModifyFunctionService(IBasicInfoController basicInfoController
            ,IPMSController pmsController,IFunctionServiceWrapper functionService)
        {
            this.basicInfoController = basicInfoController;
            this.pmsController = pmsController;
            this.functionService = functionService;
        }


        public void DoAction(FunctionListVM vm)
        {
            if (vm.SelectedFunction == null)
                return;
            functionService.GetFunction((res, exp) => pmsController.BeginInvokeOnDispatcher(() =>
                {
                    if(exp != null)
                        pmsController.HandleException(exp);
                    if (res != null)
                    {
                        basicInfoController.ShowFunctionView(res, ActionType.ModifyFunction);
                    }
                    else
                        pmsController.ShowMessage("تابع مورد نظر یافت نشد");

                }),vm.SelectedFunction.Id);
        }


    }
}

