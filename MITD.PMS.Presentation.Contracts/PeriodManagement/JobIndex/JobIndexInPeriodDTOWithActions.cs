using MITD.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class JobIndexInPeriodDTOWithActions : AbstractIndexInPeriodDTOWithActions
    {
        private long jobIndexId;
        public long JobIndexId
        {
            get { return jobIndexId; }
            set
            {
                this.SetField(p => p.JobIndexId, ref jobIndexId, value);
            }
        }

        private bool isInquireable;
        public bool IsInquireable
        {
            get { return isInquireable; }
            set
            {
                this.SetField(p => p.IsInquireable, ref isInquireable, value);
            }
        }

        
    }
}
