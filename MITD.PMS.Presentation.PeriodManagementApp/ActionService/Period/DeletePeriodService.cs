using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;
using MITD.Presentation;

namespace MITD.PMS.Presentation.PeriodManagementApp
{
    public class DeletePeriodService : IActionService<PeriodListVM>
    {
        private readonly IPMSController pmsController;
        private readonly IPeriodServiceWrapper periodService;

        public DeletePeriodService(IPMSController pmsController,IPeriodServiceWrapper periodService)
        {
            this.pmsController = pmsController;
            this.periodService = periodService;
        }


        public void DoAction(PeriodListVM vm)
        {
            if (vm.SelectedPeriod != null)
            {
                if (pmsController.ShowConfirmationBox("آیا از عملیات حذف دوره اطمینان دارید؟", "حذف دوره"))
                {
                    periodService.DeletePeriod((res, exp) => pmsController.BeginInvokeOnDispatcher(() =>
                    {
                        if (exp == null)
                        {
                            pmsController.ShowMessage("عملیات حذف دوره با موفقیت انجام شد");
                            pmsController.Publish(new UpdatePeriodListArgs());
                        }
                        else
                        {
                            pmsController.HandleException(exp);
                        }
                    }),vm.SelectedPeriod.Id);
                }

            }
            else
                pmsController.ShowMessage("اطلاعات دوره جهت حذف معتبر نمی باشد");
        }


    }
}

