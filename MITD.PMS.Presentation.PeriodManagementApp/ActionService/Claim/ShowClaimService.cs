using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;

namespace MITD.PMS.Presentation.PeriodManagementApp
{
    public class ShowClaimService : IActionService<EmployeeClaimListVM>, IActionService<ManagerClaimListVM>
    {
        private readonly IPeriodController periodController;
        private readonly IPMSController pmsController;
        private readonly IClaimServiceWrapper claimService;

        public ShowClaimService(IPMSController pmsController, IClaimServiceWrapper claimService, IPeriodController periodController)
        {
            this.pmsController = pmsController;
            this.claimService = claimService;
            this.periodController = periodController;
        }


        public void DoAction(EmployeeClaimListVM vm)
        {
            var periodId = vm.SelectedClaim.PeriodId;
            var claimId = vm.SelectedClaim.Id;
            showClaim(periodId, claimId);
        }

        public void DoAction(ManagerClaimListVM vm)
        {
            var periodId = vm.SelectedClaim.PeriodId;
            var claimId = vm.SelectedClaim.Id;
            showClaim(periodId, claimId);
        }

        private void showClaim(long periodId, long claimId)
        {
            claimService.GetClaim((res, exp) => pmsController.BeginInvokeOnDispatcher(() =>
            {
                if (exp == null)
                {
                    if (res != null)
                        periodController.ShowClaimView(res);
                    else
                        pmsController.ShowMessage("اطلاعات درخواست اعتراض جهت ارسال به صفحه نمایش معتبر نمی باشد");
                }
                else
                    pmsController.HandleException(exp);
            }), periodId, claimId);
        }


    }
}

