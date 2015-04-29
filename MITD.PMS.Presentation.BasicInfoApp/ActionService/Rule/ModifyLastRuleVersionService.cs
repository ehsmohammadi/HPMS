using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;
using MITD.Presentation;

namespace MITD.PMS.Presentation.BasicInfoApp
{
    public class ModifyLastRuleVersionService:IActionService<GridRuleListVM>
    {
        private readonly IBasicInfoController basicInfoController;
        private readonly IPMSController pmsController;
        private readonly IRuleServiceWrapper ruleService;

        public ModifyLastRuleVersionService(IBasicInfoController basicInfoController
            ,IPMSController pmsController
            ,IRuleServiceWrapper ruleService)
        {
            this.basicInfoController = basicInfoController;
            this.pmsController = pmsController;
            this.ruleService = ruleService;
        }


        public void DoAction(GridRuleListVM vm)
        {
            ruleService.GetRule((res, exp) =>
            {
                if (exp == null)
                {
                    if (res != null)
                        basicInfoController.ShowRuleVersionView(res, ActionEnum.ModifyLastRuleVersion);
                    else
                        pmsController.ShowMessage("اطلاعات این ورژن قانون جهت ارسال به صفحه ویرایش معتبر نمی باشد");
                }
                else
                    pmsController.HandleException(exp);

            }, vm.SelectedRule.Id);
         
        }


    }
}

