using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;
using MITD.Presentation;

namespace MITD.PMS.Presentation.PeriodManagementApp
{
    public class DeleteClaimService : IActionService<EmployeeClaimListVM>
    {
        private readonly IPMSController pmsController;
        private readonly IClaimServiceWrapper claimService;

        public DeleteClaimService(IPMSController pmsController, IClaimServiceWrapper claimService)
        {
            this.pmsController = pmsController;
            this.claimService = claimService;
        }


        public void DoAction(EmployeeClaimListVM vm)
        {
            if (vm.SelectedClaim != null)
            {
                if (pmsController.ShowConfirmationBox("آیا از عملیات حذف درخواست اعتراض اطمینان دارید؟", "حذف درخواست اعتراض"))
                {
                    claimService.DeleteClaim((res, exp) => pmsController.BeginInvokeOnDispatcher(() =>
                    {
                        if (exp == null)
                        {
                            pmsController.ShowMessage("عملیات حذف درخواست اعتراض با موفقیت انجام شد");
                            pmsController.Publish(new UpdateClaimListArgs(vm.SelectedClaim.EmployeeNo));
                        }
                        else
                        {
                            pmsController.HandleException(exp);
                        }
                    }),vm.SelectedClaim.PeriodId, vm.SelectedClaim.Id);
                }

            }
            else
                pmsController.ShowMessage("اطلاعات درخواست اعتراض جهت حذف معتبر نمی باشد");
        }


    }
}

