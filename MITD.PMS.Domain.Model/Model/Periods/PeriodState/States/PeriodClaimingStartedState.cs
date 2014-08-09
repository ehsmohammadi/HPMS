using MITD.PMS.Domain.Model.Jobs;
using MITD.PMS.Domain.Service;

namespace MITD.PMS.Domain.Model.Periods
{
    public class PeriodClaimingStartedState : PeriodState
    {
        public PeriodClaimingStartedState()
            : base("7", "PeriodClaimingStartedState")
        {

        }

        internal override InquiryInitializingProgress GetInitializeInquiryProgress(Period period, IPeriodManagerService periodManagerService)
        {
            return periodManagerService.GetCompletedInitializeInquiryProgress(period);
        }

        internal override void RollBack(Period period, IPeriodManagerService periodManagerService)
        {
            periodManagerService.DeleteAllCalims(period);
            period.State = new PeriodInquiryCompletedState();
        }

        internal override void FinishClaiming(Period period, IPeriodManagerService periodManagerService)
        {
            period.State = new PeriodClaimingFinishedState();
        }

        internal override void CheckAddClaim()
        {
        }
        internal override void CheckReplyClaim()
        {
        }
        internal override void CheckCancelClaim()
        {
        }

        
        //internal override void CheckModifyingEmployeeCustomFieldsAndValues()
        //{
        //}
      

    }
}
