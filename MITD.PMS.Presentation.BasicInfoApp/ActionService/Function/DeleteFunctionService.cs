using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public class DeleteFunctionService:IActionService<FunctionListVM>
    {
        public IFunctionServiceWrapper functionService { get; set; }
        private readonly IPMSController pmsController;


        public DeleteFunctionService(IPMSController pmsController, IFunctionServiceWrapper functionService)
        {
            this.functionService = functionService;
            this.pmsController = pmsController;
        }


        public void DoAction(FunctionListVM vm)
        {
            if (vm.SelectedFunction == null)
            {
                pmsController.ShowMessage("تابع مورد نظر جهت حذف یافت نشد");
                return;
            }
            if (pmsController.ShowConfirmationBox("آیا از خذف تابع اطمینان دارید؟", "حذف تابع"))
            {
                functionService.DeleteFunction((res, exp) => pmsController.BeginInvokeOnDispatcher(() =>
                {
                    if (exp != null)
                        pmsController.HandleException(exp);
                    else
                    {
                        pmsController.Publish(new UpdateFunctionListArgs());
                        pmsController.ShowMessage("عملیات حذف  با موفقیت انجام شد");
                    }
                }),vm.PolicyFunctions.PolicyId, vm.SelectedFunction.Id);
            }

        }


    }
}

