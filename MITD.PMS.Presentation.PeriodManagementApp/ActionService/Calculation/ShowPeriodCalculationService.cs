using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public class ShowPeriodCalculationService : IActionService<CalculationListVM>
    {
        private readonly IPeriodController periodController;
        private readonly ICalculationServiceWrapper calculationService;
        private readonly IPMSController pmsController;

        public ShowPeriodCalculationService(IPeriodController periodController,
            ICalculationServiceWrapper calculationService,IPMSController pmsController)
        {
            this.periodController = periodController;
            this.calculationService = calculationService;
            this.pmsController = pmsController;
        }


        public void DoAction(CalculationListVM vm)
        {
            var calculation = vm.SelectedCalculation;
            if (calculation == null)
            {
                return;
            }
            calculationService.GetCalculation((res, exp) =>
                {
                    if (exp == null)
                    {
                        periodController.ShowPeriodCalculationView(res,ActionEnum.ModifyCalculation);
                    }
                    else
                    {
                       pmsController.HandleException(exp); 
                    }
                    
                },calculation.Id);
           
        }


    }
}

