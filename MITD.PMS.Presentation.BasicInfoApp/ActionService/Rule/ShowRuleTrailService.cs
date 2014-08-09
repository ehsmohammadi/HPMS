using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;
using MITD.Presentation;

namespace MITD.PMS.Presentation.BasicInfoApp
{
    public class ShowRuleTrailService:IActionService<RuleTrailListVM>
    {
        private readonly IBasicInfoController basicInfoController;
        private readonly IPMSController pmsController;
        private readonly IRuleServiceWrapper ruleService;

        public ShowRuleTrailService(IBasicInfoController basicInfoController
            ,IPMSController pmsController
            ,IRuleServiceWrapper ruleService)
        {
            this.basicInfoController = basicInfoController;
            this.pmsController = pmsController;
            this.ruleService = ruleService;
        }


        public void DoAction(RuleTrailListVM vm)
        {
            ruleService.GetRuleTrail((res, exp) => pmsController.BeginInvokeOnDispatcher(() =>
            {
                if (exp == null)
                {
                    if (res != null)
                        basicInfoController.ShowRuleTrailView(res);
                    else
                        pmsController.ShowMessage("اطلاعات سابقه قانون جهت ارسال به صفحه نمایش  معتبر نمی باشد");
                }
                else
                    pmsController.HandleException(exp);

            }), vm.SelectedRuleTrail.PolicyId, vm.SelectedRuleTrail.RuleId, vm.SelectedRuleTrail.Id);
         
        }


    }
}

