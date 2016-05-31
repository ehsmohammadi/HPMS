using MITD.PMS.Domain.Service;
using MITD.PMS.Exceptions;

namespace MITD.PMS.Domain.Model.Periods
{
    public class PeriodClosedState : PeriodState
    {
        public PeriodClosedState()
            : base("9", "PeriodClosedState")
        {

        }

        internal override InquiryInitializingProgress GetInitializeInquiryProgress(Period period, IPeriodManagerService periodManagerService)
        {
            return periodManagerService.GetCompletedInitializeInquiryProgress(period);
        }

        internal override void RollBack(Period period, IPeriodManagerService periodManagerService)
        {
            periodManagerService.ChangeActiveStatus(period, true);
            period.State = new PeriodConfirmedState();
        }

        internal override void CheckShowingInquirySubject()
        {
            
        }


    }
}
