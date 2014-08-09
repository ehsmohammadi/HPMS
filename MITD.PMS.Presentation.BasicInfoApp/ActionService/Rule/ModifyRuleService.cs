using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;
using MITD.Presentation;

namespace MITD.PMS.Presentation.BasicInfoApp
{
    public class ModifyRuleService:IActionService<GridRuleListVM>
    {
        private readonly IBasicInfoController basicInfoController;
        private readonly IPMSController pmsController;
        private readonly IRuleServiceWrapper ruleService;

        public ModifyRuleService(IBasicInfoController basicInfoController
            ,IPMSController pmsController
            ,IRuleServiceWrapper ruleService)
        {
            this.basicInfoController = basicInfoController;
            this.pmsController = pmsController;
            this.ruleService = ruleService;
        }


        public void DoAction(GridRuleListVM vm)
        {
            ruleService.GetRule((res, exp) => pmsController.BeginInvokeOnDispatcher(() =>
            {
                if (exp == null)
                {
                    if (res != null)
                        basicInfoController.ShowRuleView(res, ActionType.ModifyRule);
                    else
                        pmsController.ShowMessage("اطلاعات قانون جهت ارسال به صفحه ویرایش معتبر نمی باشد");
                }
                else
                    pmsController.HandleException(exp);

            }),vm.Policy.Id, vm.SelectedRule.Id);
         
        }


    }
}

