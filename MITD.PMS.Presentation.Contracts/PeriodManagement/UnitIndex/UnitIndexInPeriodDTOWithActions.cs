using MITD.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class UnitIndexInPeriodDTOWithActions : AbstractUnitIndexInPeriodDTOWithActions
    {
        private long unitIndexId;
        public long UnitIndexId
        {
            get { return unitIndexId; }
            set
            {
                this.SetField(p => p.UnitIndexId, ref unitIndexId, value);
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
