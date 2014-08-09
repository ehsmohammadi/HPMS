using System.Runtime.Serialization;
using MITD.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MITD.PMS.Presentation.Contracts
{
    [KnownType(typeof(JobPositionDTOWithActions))]
    public partial class JobPositionInPeriodDTO
    {

        private long jobPositionid;
        public long JobPositionId
        {
            get { return jobPositionid; }
            set { this.SetField(p => p.JobPositionId, ref jobPositionid, value); }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { this.SetField(p => p.Name, ref name, value); }
        }

        private long? parentId;
        public long? ParentId
        {
            get { return parentId; }
            set { this.SetField(p => p.ParentId, ref parentId, value); }
        }
    }
}
