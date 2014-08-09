using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;

namespace MITD.PMS.Presentation.PeriodManagementApp
{
    public class ModifyPeriodService : IActionService<PeriodListVM>
    {
        private readonly IPeriodController periodController;
        private readonly IPMSController pmsController;
        private readonly IPeriodServiceWrapper periodService;

        public ModifyPeriodService(IPMSController pmsController, IPeriodServiceWrapper periodService, IPeriodController periodController)
        {
            this.pmsController = pmsController;
            this.periodService = periodService;
            this.periodController = periodController;
        }


        public void DoAction(PeriodListVM vm)
        {
            periodService.GetPeriod((res, exp) =>pmsController.BeginInvokeOnDispatcher(()=>
            {
                if (exp == null)
                {
                    if (res != null)
                        periodController.ShowPeriodView(res, ActionType.ModifyPeriod);
                    else
                        pmsController.ShowMessage("اطلاعات دوره جهت ارسال به صفحه ویرایش معتبر نمی باشد");
                }
                else
                    pmsController.HandleException(exp);

            }), vm.SelectedPeriod.Id);
        }
    }
}

