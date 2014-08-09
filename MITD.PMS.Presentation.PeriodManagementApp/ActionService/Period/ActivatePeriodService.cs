using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;

namespace MITD.PMS.Presentation.PeriodManagementApp
{
    public class ActivatePeriodService : IActionService<PeriodListVM>
    {
      
        private readonly IPMSController pmsController;
        private readonly IPeriodServiceWrapper periodService;

        public ActivatePeriodService(IPMSController pmsController, IPeriodServiceWrapper periodService)
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
            if (pmsController.ShowConfirmationBox("آیا می خواهید دوره انتخاب شده را فعال کنید ؟", "فعال سازی دوره"))
            {
                var periodDto = new PeriodDTO()
                {
                    Id = period.Id,
                    StartDate = period.StartDate,
                    EndDate = period.EndDate,
                    Name = period.Name,
                    PutActionName = "ChangeActiveStatus",
                    ActiveStatus = true
                };

                periodService.ChangePeriodActiveStatus(exp => pmsController.BeginInvokeOnDispatcher(() =>
                {
                    if (exp != null)
                        pmsController.HandleException(exp);
                    else
                    {
                        pmsController.Publish(new UpdatePeriodListArgs());
                        pmsController.GetCurrentPeriod();
                        //var action = new ShowPeriodCalculationStateService(periodController, pmsController, calculationService);
                        //action.DoAction(vm);
                    }

                }), periodDto);
            }

        }
    }
}

