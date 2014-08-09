using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Presentation.Logic
{
    public class DeletePolicyService:IActionService<PolicyListVM>
    {
        private readonly IPMSController pmsController;
        private readonly IPolicyServiceWrapper policyService;

        public DeletePolicyService(IPMSController pmsController,IPolicyServiceWrapper policyService)
        {
            this.pmsController = pmsController;
            this.policyService = policyService;
        }


        public void DoAction(PolicyListVM vm)
        {
            if (pmsController.ShowConfirmationBox("آیا از عملیات حذف نظام محاسبه عملکرد اطمینان دارید؟", "حذف نظام محاسبه عملکرد"))
            {
                policyService.DeletePolicy((res, exp) => pmsController.BeginInvokeOnDispatcher(() =>
                {
                    if (exp == null)
                    {
                        pmsController.ShowMessage("عملیات حذف نظام محاسبه عملکرد با موفقیت انجام شد");
                        pmsController.Publish(new UpdatePolicyListArgs());
                    }
                    else
                    {
                        pmsController.HandleException(exp);
                    }
                }), vm.SelectedPolicy.Id);

            }
        }


    }
}

