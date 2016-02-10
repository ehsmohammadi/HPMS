using System.Collections.Generic;
using MITD.Domain.Model;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Integration.Domain
{
    public class JobPositionConverted : IDomainEvent<JobPositionConverted>
    {
        private readonly List<JobPositionInPeriodAssignmentDTO> jobPositionInperiodList;

        public List<JobPositionInPeriodAssignmentDTO> JobPositionInperiodList
        {
            get { return jobPositionInperiodList; }
        }

        public JobPositionConverted(List<JobPositionInPeriodAssignmentDTO> jobPositionInperiodList)
        {
            this.jobPositionInperiodList = jobPositionInperiodList;
        }

        public bool SameEventAs(JobPositionConverted other)
        {
            return true;
        }
    }
}
