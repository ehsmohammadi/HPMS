using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;
using MITD.Presentation;

namespace MITD.PMS.Presentation.PeriodManagementApp
{
    public class DeletePeriodCalculationService : IActionService<CalculationListVM>
    {
        private readonly IPMSController pmsController;
        private readonly ICalculationServiceWrapper calculationService;

        public DeletePeriodCalculationService(IPMSController pmsController, ICalculationServiceWrapper calculationService)
        {
            this.pmsController = pmsController;
            this.calculationService = calculationService;
        }


        public void DoAction(CalculationListVM vm)
        {
            if (vm.SelectedCalculation != null)
            {
                if (pmsController.ShowConfirmationBox("آیا از عملیات حذف محاسبه دوره اطمینان دارید؟", "حذف محاسبه"))
                {
                    calculationService.DeleteCalculation((res, exp) => pmsController.BeginInvokeOnDispatcher(()=>
                        {
                            if (exp == null)
                            {
                                pmsController.ShowMessage("عملیات حذف محاسبه با موفقیت انجام شد");
                                pmsController.Publish(new UpdateCalculationListArgs());
                            }
                            else if (exp != null)
                            {
                                pmsController.HandleException(exp);
                            }
                        })
                        , pmsController.CurrentPriod.Id, vm.SelectedCalculation.Id);
                }

            }
            else
                pmsController.ShowMessage("اطلاعات محاسبه جهت حذف معتبر نمی باشد");
        }


    }
}

