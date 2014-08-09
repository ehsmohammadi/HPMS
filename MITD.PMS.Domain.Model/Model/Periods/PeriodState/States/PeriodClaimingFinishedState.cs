using System.Runtime.InteropServices;
using MITD.Core;
using MITD.PMS.Common;
using MITD.PMS.Domain.Model.Claims;
using MITD.PMS.Domain.Model.Jobs;
using MITD.PMS.Domain.Service;
using MITD.PMS.Exceptions;

namespace MITD.PMS.Domain.Model.Periods
{
    public class PeriodClaimingFinishedState : PeriodState
    {
        public PeriodClaimingFinishedState()
            : base("8", "PeriodClaimingFinishedState")
        {

        }

        internal override InquiryInitializingProgress GetInitializeInquiryProgress(Period period, IPeriodManagerService periodManagerService)
        {
            return periodManagerService.GetCompletedInitializeInquiryProgress(period);
        }

        internal override void RollBack(Period period, IPeriodManagerService periodManagerService)
        {
            if (!periodManagerService.HasDeterministicCalculation(period))
                throw new PeriodException((int)ApiExceptionCode.CouldNotStartClaimingWithoutAnyDeterministicCalculation
                    , ApiExceptionCode.CouldNotStartClaimingWithoutAnyDeterministicCalculation.DisplayName); 

            period.State = new PeriodClaimingStartedState();
        }

        internal override void Close(Period period, IPeriodManagerService periodManagerService)
        {
            if (!periodManagerService.HasDeterministicCalculation(period))
                throw new PeriodException((int)ApiExceptionCode.CouldNotClosePeriodWithoutAnyDeterministicCalculation
                    , ApiExceptionCode.CouldNotClosePeriodWithoutAnyDeterministicCalculation.DisplayName); 
            
            if (periodManagerService.HasOpenClaim(period))
                throw new PeriodException((int)ApiExceptionCode.CouldNotClosePeriodWithOpenClaims
                    , ApiExceptionCode.CouldNotClosePeriodWithOpenClaims.DisplayName); 
                 

            period.State = new PeriodClosedState();
            period.DeActive();
        }

        internal override void CheckReplyClaim()
        {
        }

        internal override void CheckCreatingCalculation()
        {
        }

        internal override void CheckChangeCalculationDeterministicStatus()
        {
        }

    }
}
