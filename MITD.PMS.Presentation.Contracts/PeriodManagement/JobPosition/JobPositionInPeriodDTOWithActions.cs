using System.Collections.Generic;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class JobPositionInPeriodDTOWithActions:JobPositionInPeriodDTO,IActionDTO
    {
        public List<int> ActionCodes { get; set; }
        private string unitName;
        public string UnitName
        {
            get { return unitName; }
            set { this.SetField(p => p.UnitName, ref unitName, value); }
        }
        private string jobName;
        public string JobName
        {
            get { return jobName; }
            set { this.SetField(p => p.JobName, ref jobName, value); }
        }

        private long unitid;
        public long Unitid
        {
            get { return unitid; }
            set { this.SetField(p => p.Unitid, ref unitid, value); }
        }

        private long jobid;
        public long JobId
        {
            get { return jobid; }
            set { this.SetField(p => p.JobId, ref jobid, value); }
        }

    }
}
