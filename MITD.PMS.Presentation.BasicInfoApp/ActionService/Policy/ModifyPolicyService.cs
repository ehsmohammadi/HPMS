using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Presentation.Logic
{
    public class ModifyPolicyService : IActionService<PolicyListVM>
    {
        private readonly IBasicInfoController basicInfoController;
        private readonly IPMSController pmsController;
        private readonly IPolicyServiceWrapper policyService;

        public ModifyPolicyService(IBasicInfoController basicInfoController,
            IPMSController pmsController, 
            IPolicyServiceWrapper policyService)
        {
            this.basicInfoController = basicInfoController;
            this.pmsController = pmsController;
            this.policyService = policyService;
        }


        public void DoAction(PolicyListVM vm)
        {
            policyService.GetPolicy((res, exp) => pmsController.BeginInvokeOnDispatcher(() =>
                {
                    if (exp == null)
                    {
                        if (res != null)
                            basicInfoController.ShowPolicyView(res, ActionType.ModifyPolicy);
                        else
                            pmsController.ShowMessage("اطلاعات نظام محاسبه عملکرد جهت ارسال به صفحه ویرایش معتبر نمی باشد");
                    }
                    else
                        pmsController.HandleException(exp);

                }), vm.SelectedPolicy.Id);



        }


    }


    
}

