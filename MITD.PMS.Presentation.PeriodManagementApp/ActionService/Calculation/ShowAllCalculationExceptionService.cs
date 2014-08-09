using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;

namespace MITD.PMS.Presentation.PeriodManagementApp
{
    public class ShowAllCalculationExceptionService : IActionService<CalculationListVM>
    {
        private readonly IPeriodController periodController;
        private readonly IPMSController pmsController;
        private readonly ICalculationServiceWrapper calculationService;

        public ShowAllCalculationExceptionService(IPMSController pmsController, ICalculationServiceWrapper calculationService, IPeriodController periodController)
        {
            this.pmsController = pmsController;
            this.calculationService = calculationService;
            this.periodController = periodController;
        }


        public void DoAction(CalculationListVM vm)
        {
            var calc = vm.SelectedCalculation;
            calculationService.GetCalculation((res, exp) => pmsController.BeginInvokeOnDispatcher(() =>
            {
                if (exp == null)
                {
                    if (res != null)
                        periodController.ShowCalculationExceptionListView(res);
                    else
                        pmsController.ShowMessage("اطلاعات  محاسبه  جهت ارسال به صفحه نمایش خطاهای محاسبه معتبر نمی باشد");
                }
                else
                    pmsController.HandleException(exp);
            }), vm.Period.Id, calc.Id);
        }

        


    }
}

