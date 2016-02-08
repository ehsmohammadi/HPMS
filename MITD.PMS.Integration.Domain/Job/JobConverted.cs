using System.Collections.Generic;
using MITD.Domain.Model;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Integration.Domain
{
    public class JobConverted : IDomainEvent<JobConverted>
    {
        private readonly List<JobInPeriodDTO> jobInperiodList;

        public List<JobInPeriodDTO> JobInperiodList
        {
            get { return jobInperiodList; }
        }

        public JobConverted(List<JobInPeriodDTO> jobInperiodList)
        {
            this.jobInperiodList = jobInperiodList;
        }

        public bool SameEventAs(JobConverted other)
        {
            return true;
        }
    }
}
