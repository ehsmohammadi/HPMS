using MITD.Domain.Model;
using MITD.PMS.Domain.Model.Jobs;

namespace MITD.PMS.Domain.Model.Periods
{
    public class InitializeInquiryCompleted : IDomainEvent<InitializeInquiryCompleted>
    {
        public Period Period
        {
            get;
            private set;
        }

       

        public InitializeInquiryCompleted(Period period)
        {
            Period = period;
            
        }
        public bool SameEventAs(InitializeInquiryCompleted other)
        {
            return true;
        }
    }

    public class JobJobIndicesHasbeenChanged : IDomainEvent<JobJobIndicesHasbeenChanged>
    {
        public Job Job
        {
            get;
            private set;
        }

        public JobJobIndicesHasbeenChanged(Job job)
        {
            Job = job;
        }
        public bool SameEventAs(JobJobIndicesHasbeenChanged other)
        {
            return true;
        }
    }

    public class CopyBasicDataCompleted : IDomainEvent<CopyBasicDataCompleted>
    {
        public Period Period
        {
            get;
            private set;
        }

        public PeriodState State
        {
            get;
            private set;
        }

        public CopyBasicDataCompleted(Period period, PeriodState state)
        {
            Period = period;
            State = state;
        }
        public bool SameEventAs(CopyBasicDataCompleted other)
        {
            return true;
        }
    }
}
