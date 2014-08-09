using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public class AddClaimService : IActionService<EmployeeClaimListVM>, IActionService<CalculationResultVM> 
    {
        private readonly IPeriodController periodController;

        public AddClaimService(IPeriodController periodController)
        {
            this.periodController = periodController;
        }


        public void DoAction(EmployeeClaimListVM vm)
        {
            showClaim();
        }

        private void showClaim()
        {
            var claim = new ClaimDTO();
            periodController.ShowClaimView(claim, ActionType.AddClaim);
        }


        public void DoAction(CalculationResultVM dto)
        {
            showClaim();
        }
    }
}

