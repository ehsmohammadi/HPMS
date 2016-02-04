using System.Collections.Generic;
using MITD.Domain.Model;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Integration.Domain
{
    public class JobIndexConverted : IDomainEvent<JobIndexConverted>
    {
        private readonly List<JobIndexInPeriodDTO> jobIndexInperiodList;

        public List<JobIndexInPeriodDTO> JobIndexInperiodList
        {
            get { return jobIndexInperiodList; }
        }

        public JobIndexConverted(List<JobIndexInPeriodDTO> jobIndexInperiodList)
        {
            this.jobIndexInperiodList = jobIndexInperiodList;
        }

        public bool SameEventAs(JobIndexConverted other)
        {
            return true;
        }
    }
}
