using System.Collections.Generic;
using MITD.Domain.Model;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Integration.Domain
{
    public class JobPositionConverted : IDomainEvent<JobPositionConverted>
    {
        private readonly List<JobPositionDTO> jobPositionList;

        public List<JobPositionDTO> JobPositionList
        {
            get { return jobPositionList; }
        }

        public JobPositionConverted(List<JobPositionDTO> jobPositionList)
        {
            this.jobPositionList = jobPositionList;
        }

        public bool SameEventAs(JobPositionConverted other)
        {
            return true;
        }
    }
}
