using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;

namespace MITD.PMS.Presentation.PeriodManagementApp
{
    public class ShowCalculationExceptionService : IActionService<CalculationExceptionListVM>
    {
        private readonly IPeriodController periodController;
        private readonly IPMSController pmsController;
        private readonly ICalculationServiceWrapper calculationService;

        public ShowCalculationExceptionService(IPMSController pmsController, ICalculationServiceWrapper calculationService, IPeriodController periodController)
        {
            this.pmsController = pmsController;
            this.calculationService = calculationService;
            this.periodController = periodController;
        }


        public void DoAction(CalculationExceptionListVM vm)
        {
            var calcExp = vm.SelectedCalculationException;
            calculationService.GetCalculationException((res, exp) => pmsController.BeginInvokeOnDispatcher(() =>
            {
                if (exp == null)
                {
                    if (res != null)
                        periodController.ShowCalculationExceptionView(res);
                    else
                        pmsController.ShowMessage("اطلاعات خطای محاسبه  جهت ارسال به صفحه نمایش معتبر نمی باشد");
                }
                else
                    pmsController.HandleException(exp);
            }), vm.Calculation.PeriodId,calcExp.CalculationId,calcExp.Id);
        }

        


    }
}

