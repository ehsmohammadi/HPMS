using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;


namespace MITD.PMS.Presentation.PeriodManagementApp
{
    public class ManageJobPositionInPeriodInquiryService : IActionService<JobPositionInPeriodTreeVM>
    {
        private readonly IPeriodController periodController;

        public ManageJobPositionInPeriodInquiryService(IPeriodController periodController)
        {
            this.periodController = periodController;
        }


        public void DoAction(JobPositionInPeriodTreeVM vm)
        {
            if (vm.Period != null && vm.SelectedJobPositionInPeriod != null)
            {
                periodController.ShowJobPositionInPeriodInquiryView(vm.Period, vm.SelectedJobPositionInPeriod.Data,
                    ActionType.ManageJobPositionInPeriodInquiry);
            }
        }


    }


    
}

