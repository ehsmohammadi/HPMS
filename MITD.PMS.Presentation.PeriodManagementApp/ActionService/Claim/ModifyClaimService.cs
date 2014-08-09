using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;

namespace MITD.PMS.Presentation.PeriodManagementApp
{
    public class ModifyClaimService : IActionService<ManagerClaimListVM>
    {
        private readonly IPeriodController periodController;
        private readonly IPMSController pmsController;
        private readonly IClaimServiceWrapper claimService;

        public ModifyClaimService(IPMSController pmsController, IClaimServiceWrapper claimService, IPeriodController periodController)
        {
            this.pmsController = pmsController;
            this.claimService = claimService;
            this.periodController = periodController;
        }


        public void DoAction(ManagerClaimListVM vm)
        {
            claimService.GetClaim((res, exp) =>pmsController.BeginInvokeOnDispatcher(()=>
            {
                if (exp == null)
                {
                    if (res != null)
                        periodController.ShowClaimView(res, ActionType.ReplyToClaim);
                    else
                        pmsController.ShowMessage("اطلاعات درخواست اعتراض جهت ارسال به صفحه پاسخ معتبر نمی باشد");
                }
                else
                    pmsController.HandleException(exp);

            }),vm.SelectedClaim.PeriodId, vm.SelectedClaim.Id);
        }
    }
}

