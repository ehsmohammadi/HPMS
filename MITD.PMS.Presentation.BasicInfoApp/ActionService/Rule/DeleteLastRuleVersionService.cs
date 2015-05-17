using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;

namespace MITD.PMS.Presentation.BasicInfoApp
{
    public class DeleteLastRuleVersionService:IActionService<GridRuleListVM>
    {
        private readonly IPMSController pmsController;
        private readonly IRuleServiceWrapper ruleService;

        public DeleteLastRuleVersionService(IPMSController pmsController, IRuleServiceWrapper ruleService)
        {
            this.pmsController = pmsController;
            this.ruleService = ruleService;
        }


        public void DoAction(GridRuleListVM vm)
        {
            if (pmsController.ShowConfirmationBox("آیا از عملیات حذف آخرین قانون اطمینان دارید؟", "حذف آخرین قانون"))
            {
                ruleService.DeleteLastRuleVersion((res, exp) =>
                {
                    if (exp == null && res)
                    {
                        pmsController.ShowMessage("عملیات حذف آخرین قانون با موفقیت انجام شد");
                        pmsController.Publish(new UpdateRuleVersionsArgs());
                    }
                    else if (exp != null)
                    {
                        pmsController.HandleException(exp);
                    }
                    else 
                    {
                        pmsController.ShowMessage("عملیات حذف آخرین قانون انجام نشد");
                    }

                }
                , vm.SelectedRule.Id);

            }
            
        }


    }
}

