using MITD.PMS.Domain.Service;
using MITD.PMS.Exceptions;

namespace MITD.PMS.Domain.Model.Periods
{
    public class PeriodInitializingForInquiryState : PeriodState 
    {
        public PeriodInitializingForInquiryState()
            : base("3", "PeriodInitializingForInquiryState")
        {

        }

        internal override void CompleteIntializingForInquiry(Period period, IPeriodManagerService periodManagerService)
        {
            period.State = new PeriodInitializeInquiryCompletedState();
        }

        internal override InquiryInitializingProgress GetInitializeInquiryProgress(Period period, IPeriodManagerService periodManagerService)
        {
            return periodManagerService.GetInitializeInquiryProgress(period);
        }

       

        
    }
}
