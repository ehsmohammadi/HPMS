using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;

namespace MITD.PMS.Presentation.PeriodManagementApp
{
    public class ConfirmPeriodService : IActionService<PeriodListVM>
    {
      
        private readonly IPMSController pmsController;
        private readonly IPeriodServiceWrapper periodService;

        public ConfirmPeriodService(IPMSController pmsController, IPeriodServiceWrapper periodService)
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
            if (pmsController.ShowConfirmationBox("آیا می خواهید نمرات کارمندان را تایید کنید ؟", "تایید نهایی دوره و نمرات کارمندان"))
            {
                periodService.ChangePeriodState(exp => pmsController.BeginInvokeOnDispatcher(() =>
                {
                    if (exp != null)
                        pmsController.HandleException(exp);
                    else
                    {
                        pmsController.Publish(new UpdatePeriodListArgs());
                        //pmsController.GetCurrentPeriod();
                        //var action = new ShowPeriodCalculationStateService(periodController, pmsController, calculationService);
                        //action.DoAction(vm);
                    }

                }), period.Id, new PeriodStateDTO { State = (int)PeriodStateEnum.Confirmed });
            }

        }
    }
}

