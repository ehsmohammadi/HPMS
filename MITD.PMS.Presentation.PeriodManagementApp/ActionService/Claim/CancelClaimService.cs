using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;
using MITD.Presentation;

namespace MITD.PMS.Presentation.PeriodManagementApp
{
    public class CancelClaimService : IActionService<EmployeeClaimListVM>
    {
        private readonly IPMSController pmsController;
        private readonly IClaimServiceWrapper claimService;

        public CancelClaimService(IPMSController pmsController, IClaimServiceWrapper claimService)
        {
            this.pmsController = pmsController;
            this.claimService = claimService;
        }


        public void DoAction(EmployeeClaimListVM vm)
        {
            if (vm.SelectedClaim != null)
            {
                if (pmsController.ShowConfirmationBox("آیا از عملیات انصراف از درخواست اعتراض اطمینان دارید؟", "انصراف درخواست اعتراض"))
                {
                    claimService.ChangeClaimState((res, exp) => pmsController.BeginInvokeOnDispatcher(() =>
                    {
                        if (exp == null)
                        {
                            pmsController.ShowMessage("عملیات انصراف از درخواست اعتراض با موفقیت انجام شد");
                            pmsController.Publish(new UpdateClaimListArgs(vm.SelectedClaim.EmployeeNo));
                        }
                        else
                        {
                            pmsController.HandleException(exp);
                        }
                    }),vm.SelectedClaim.PeriodId, vm.SelectedClaim.Id,"CancelClaim",new ClaimStateDTO{Id=2});
                }

            }
            else
                pmsController.ShowMessage("اطلاعات درخواست اعتراض جهت انصراف معتبر نمی باشد");
        }


    }
}

