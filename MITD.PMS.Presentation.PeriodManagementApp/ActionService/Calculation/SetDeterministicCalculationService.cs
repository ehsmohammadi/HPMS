using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;

namespace MITD.PMS.Presentation.PeriodManagementApp
{
    public class SetDeterministicCalculationService : IActionService<CalculationListVM>
    {
        private readonly IPeriodController periodController;
        private readonly IPMSController pmsController;
        private readonly ICalculationServiceWrapper calculationService;

        public SetDeterministicCalculationService(IPMSController pmsController, ICalculationServiceWrapper calculationService, IPeriodController periodController)
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
            if(pmsController.ShowConfirmationBox("آیا می خواهید محاسبه انجام شده را قطعی کنید؟","قطعی کردن محاسبه"))
            {
                calculationService.GetCalculation((calc, exp) => pmsController.BeginInvokeOnDispatcher(() =>
                {
                    if (exp != null)
                        pmsController.HandleException(exp);
                    else
                    {
                        calc.PutActionName = "ChangeCalculationDeterministicStatus";
                        calc.IsDeterministic = true;
                        calc.EmployeeIdList = "";

                        calculationService.ChangeDeterministicCalculation((expUpdate) => pmsController.BeginInvokeOnDispatcher(() =>
                        {
                            if (expUpdate != null)
                                pmsController.HandleException(expUpdate);
                            else
                            {
                                pmsController.ShowMessage("محاسبه انتخاب شده  قطعی گردید");
                                pmsController.Publish(new UpdateCalculationListArgs());
                            }

                        }), calc);
                    }

                }), vm.Period.Id, vm.SelectedCalculation.Id);
            }
            

        }
    }
}

