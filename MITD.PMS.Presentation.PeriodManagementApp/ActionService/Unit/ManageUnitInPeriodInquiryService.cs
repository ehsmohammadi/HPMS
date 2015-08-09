using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;


namespace MITD.PMS.Presentation.PeriodManagementApp
{
    public class ManageUnitInPeriodInquiryService : IActionService<UnitInPeriodTreeVM>
    {
        private readonly IPeriodController periodController;

        public ManageUnitInPeriodInquiryService(IPeriodController periodController)
        {
            this.periodController = periodController;
        }
        
        public void DoAction(UnitInPeriodTreeVM vm)
        {
            if (vm.Period != null && vm.SelectedUnitInPeriod != null)
            {
                periodController.ShowUnitInPeriodInquiryView(vm.Period, vm.SelectedUnitInPeriod.Data,
                    ActionType.ManageUnitInPeriodInquiry);
                
            }
        }


    }


    
}

