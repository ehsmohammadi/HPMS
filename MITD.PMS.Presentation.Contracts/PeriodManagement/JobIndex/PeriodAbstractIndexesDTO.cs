using System.Collections.Generic;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class PeriodAbstractIndexesDTO
    {

        private long periodId;
        private string periodName;
        private List<AbstractIndexInPeriodDTOWithActions> abstractIndexInPeriods;

        public long PeriodId
        {
            get { return periodId; }
            set { this.SetField(p => p.PeriodId, ref periodId, value); }
        }

        public string PeriodName
        {
            get { return periodName; }
            set { this.SetField(p => p.PeriodName, ref periodName, value); }
        }

        public List<AbstractIndexInPeriodDTOWithActions> AbstractIndexInPeriods
        {
            get { return abstractIndexInPeriods; }
            set { this.SetField(p => p.AbstractIndexInPeriods, ref abstractIndexInPeriods, value); }
        }
       
    }
}
