using MITD.Core;
using MITD.PMS.Domain.Service;

namespace MITD.PMS.Domain.Model.Periods
{
    public class PeriodConfirmationState : PeriodState
    {
        public PeriodConfirmationState()
            : base("7", "PeriodConfirmationState")
        {

        }

        internal override void Confirm(Period period, IPeriodManagerService periodManagerService)
        {
            periodManagerService.ConfirmEmployeePoint(period);
            period.State = new PeriodConfirmedState();
        }

        internal override void RollBack(Period period, IPeriodManagerService periodManagerService)
        {
            periodManagerService.DeleteEmployeePoint(period);
            period.State = new PeriodInquiryCompletedState();

        }

        internal override InquiryInitializingProgress GetInitializeInquiryProgress(Period period, IPeriodManagerService periodManagerService)
        {
            return periodManagerService.GetCompletedInitializeInquiryProgress(period);
        }

        internal override void CheckCreatingCalculation()
        {
            
        }
    }


}

