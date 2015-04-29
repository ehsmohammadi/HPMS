using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;

namespace MITD.PMS.Presentation.PeriodManagementApp
{
    public class RunPeriodCalculationService : IActionService<CalculationListVM>
    {
        private readonly IPeriodController periodController;
        private readonly IPMSController pmsController;
        private readonly ICalculationServiceWrapper calculationService;

        public RunPeriodCalculationService(IPMSController pmsController, ICalculationServiceWrapper calculationService, IPeriodController periodController)
        {
            this.pmsController = pmsController;
            this.calculationService = calculationService;
            this.periodController = periodController;
        }


        public void DoAction(CalculationListVM vm)
        {
            var period = vm.SelectedCalculation;
            if (period == null)
            {
                pmsController.ShowMessage("محاسبه ای انتخاب نشده است");
                return;
            }
            if(pmsController.ShowConfirmationBox("آیا می خواهید محاسبه انجام شود؟","اجرای محاسبه"))
            {
                calculationService.ChangeCalculationState((exp) => pmsController.BeginInvokeOnDispatcher(()=>
                    {
                        if (exp != null)
                            pmsController.HandleException(exp);
                        else
                        {
                            pmsController.Publish(new UpdateCalculationListArgs());
                            var action = new ShowPeriodCalculationStateService(periodController, pmsController, calculationService);
                            action.DoAction(vm);
                        }

                    }), pmsController.CurrentPriod.Id,vm.SelectedCalculation.Id, new CalculationStateDTO { State = (int)CalculationStateEnum.Running});
            }
            

        }
    }
}

