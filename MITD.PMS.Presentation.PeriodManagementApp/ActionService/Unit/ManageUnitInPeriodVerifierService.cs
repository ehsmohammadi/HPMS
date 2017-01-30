using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;


namespace MITD.PMS.Presentation.PeriodManagementApp
{
    public class ManageUnitInPeriodVerifierService : IActionService<UnitInPeriodTreeVM>
    {
        private readonly IPeriodController periodController;

        public ManageUnitInPeriodVerifierService(IPeriodController periodController)
        {
            this.periodController = periodController;
        }
        
        public void DoAction(UnitInPeriodTreeVM vm)
        {
            if (vm.Period != null && vm.SelectedUnitInPeriod != null)
            {
                periodController.ShowUnitInPeriodVerifierView(vm.Period, vm.SelectedUnitInPeriod.Data,
                    ActionType.ManageUnitInPeriodVerifier);
                
            }
        }


    }


    
}

