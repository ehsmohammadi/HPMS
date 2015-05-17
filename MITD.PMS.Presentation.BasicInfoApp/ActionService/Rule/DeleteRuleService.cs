using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;

namespace MITD.PMS.Presentation.BasicInfoApp
{
    public class DeleteRuleService:IActionService<GridRuleListVM>
    {
        private readonly IPMSController pmsController;
        private readonly IRuleServiceWrapper ruleService;

        public DeleteRuleService(IPMSController pmsController,IRuleServiceWrapper ruleService)
        {
            this.pmsController = pmsController;
            this.ruleService = ruleService;
        }


        public void DoAction(GridRuleListVM vm)
        {
            if (pmsController.ShowConfirmationBox("آیا از عملیات حذف قانون اطمینان دارید؟", "حذف قانون"))
            {
                ruleService.DeleteRule((res, exp) => pmsController.BeginInvokeOnDispatcher(() =>
                {
                    if (exp == null)
                    {
                        pmsController.ShowMessage("عملیات حذف قانون با موفقیت انجام شد");
                        pmsController.Publish(new UpdateRuleListArgs());
                    }
                    else
                    {
                        pmsController.HandleException(exp);
                    }
                }),vm.Policy.Id, vm.SelectedRule.Id);

            }
            
        }


    }
}

