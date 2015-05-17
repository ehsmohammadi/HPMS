using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;

namespace MITD.PMS.Presentation.PeriodManagementApp
{
    public class ModifyPeriodCalculationService : IActionService<CalculationListVM>
    {
        private readonly IPeriodController periodController;
        private readonly IPMSController pmsController;
        private readonly ICalculationServiceWrapper calculationService;

        public ModifyPeriodCalculationService(IPeriodController periodController
            , IPMSController pmsController, ICalculationServiceWrapper calculationService)
        {
            this.periodController = periodController;
            this.pmsController = pmsController;
            this.calculationService = calculationService;
        }


        public void DoAction(CalculationListVM vm)
        {
            //calculationService.GetCalculationExec((res, exp) =>
            //{
            //    if (exp == null)
            //    {
            //        if (res != null)
            //            periodController.ShowPeriodCalculationExecView(res, ActionType.ModifyPeriodCalculationExec);
            //        else
            //            pmsController.ShowMessage("اطلاعات محاسبه جهت ارسال به صفحه ویرایش معتبر نمی باشد");
            //    }
            //    else
            //        pmsController.HandleException(exp);

            //}, vm.SelectedCalculation.Id);



        }


    }



}

