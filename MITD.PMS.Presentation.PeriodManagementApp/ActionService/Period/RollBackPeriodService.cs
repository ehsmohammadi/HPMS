using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;

namespace MITD.PMS.Presentation.PeriodManagementApp
{
    public class RollBackPeriodService : IActionService<PeriodListVM>
    {
      
        private readonly IPMSController pmsController;
        private readonly IPeriodServiceWrapper periodService;

        public RollBackPeriodService(IPMSController pmsController, IPeriodServiceWrapper periodService)
        {
            this.pmsController = pmsController;
            this.periodService = periodService;
          
        }


        public void DoAction(PeriodListVM vm)
        {
            var period = vm.SelectedPeriod;
            if (period == null)
            {
                pmsController.ShowMessage("دوره ای انتخاب نشده است");
                return;
            }
            if (pmsController.ShowConfirmationBox("برگشت دوره میتواند موجب حذف بخشی از اطلاعات شود، آیا از برگشت دوره به وضعیت قبل اطمینان دارید ؟", "برگشت دوره"))
            {
                periodService.RollBackPeriodState(exp => pmsController.BeginInvokeOnDispatcher(() =>
                {
                    if (exp != null)
                        pmsController.HandleException(exp);
                    else
                    {
                        pmsController.Publish(new UpdatePeriodListArgs());
                        pmsController.GetCurrentPeriod();
                    }
                }), period.Id);
            }

        }
    }
}

