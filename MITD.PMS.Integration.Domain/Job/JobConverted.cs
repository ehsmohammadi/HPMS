using System.Collections.Generic;
using MITD.Domain.Model;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Integration.Domain
{
    public class JobConverted : IDomainEvent<JobConverted>
    {
        private readonly List<JobDTO> jobList;

        public List<JobDTO> JobList
        {
            get { return jobList; }
        }

        public JobConverted(List<JobDTO> jobInperiodList)
        {
            jobList = jobInperiodList;
        }

        public bool SameEventAs(JobConverted other)
        {
            return true;
        }
    }
}
