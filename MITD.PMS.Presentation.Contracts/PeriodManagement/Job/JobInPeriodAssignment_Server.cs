using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class JobInPeriodAssignment : DTOBase
    {
        private List<JobInPrdField> jobInPrdFields;
        public List<JobInPrdField> JobInPrdFields
        {
            get { return jobInPrdFields; }
            set { this.SetField(p => p.JobInPrdFields, ref jobInPrdFields, value); }
        }
    }
}
