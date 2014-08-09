using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;

namespace MITD.PMS.Presentation.PeriodManagementApp
{
    public class InitializeForInquiryService : IActionService<PeriodListVM>
    {
        private readonly IPMSController pmsController;
        private readonly IPeriodServiceWrapper periodService;
        private readonly IPeriodController periodController;

        public InitializeForInquiryService(IPMSController pmsController, IPeriodServiceWrapper periodService, IPeriodController periodController)
        {
            this.pmsController = pmsController;
            this.periodService = periodService;
            this.periodController = periodController;
        }


        public void DoAction(PeriodListVM vm)
        {
            var period = vm.SelectedPeriod;
            if (period == null)
            {
                pmsController.ShowMessage("دوره ای انتخاب نشده است");
                return;
            }
            if (pmsController.ShowConfirmationBox("آیا می خواهید دوره انتخاب شده را برای نظرسنجی آماده کنید ؟", "آماده سازی برای نظرسنجی"))
            {
                periodService.ChangePeriodState(exp => pmsController.BeginInvokeOnDispatcher(() =>
                {
                    if (exp != null)
                        pmsController.HandleException(exp);
                    else
                    {
                        pmsController.Publish(new UpdatePeriodListArgs());
                        periodController.ShowPeriodStatusView(period, ActionType.GetPeriodInitializingInquiryStatus);
                    }

                }), period.Id, new PeriodStateDTO { State = (int)PeriodStateEnum.InitializingForInquiry });
            }

        }
    }
}

