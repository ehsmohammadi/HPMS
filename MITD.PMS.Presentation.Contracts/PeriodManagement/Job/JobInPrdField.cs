using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class JobInPrdField:CustomFieldDTO
    {
        private long jobInPeriodId;

        public long JobInPeriodId
        {
            get { return jobInPeriodId; }
            set { this.SetField(p => p.JobInPeriodId, ref jobInPeriodId, value); }
        } 
    }
}
