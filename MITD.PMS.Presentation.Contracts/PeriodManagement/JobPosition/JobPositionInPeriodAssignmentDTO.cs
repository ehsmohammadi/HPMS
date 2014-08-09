using System;
using System.ComponentModel.DataAnnotations;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class JobPositionInPeriodAssignmentDTO 
    {

        private long jobPositionId;
        [Range(1, Int64.MaxValue, ErrorMessage = "انتخاب پست الزامی است")]
        public long JobPositionId
        {
            get { return jobPositionId; }
            set { this.SetField(p => p.JobPositionId, ref jobPositionId, value); }
        }

        private long periodId;
        [Range(1, Int64.MaxValue, ErrorMessage = "انتخاب دوره الزامی است")]
        public long PeriodId
        {
            get { return periodId; }
            set { this.SetField(p => p.PeriodId, ref periodId, value); }
        }

        private long unitId;
        [Range(1, Int64.MaxValue, ErrorMessage = "انتخاب واحد الزامی است")]
        public long UnitId
        {
            get { return unitId; }
            set { this.SetField(p => p.UnitId, ref unitId, value); }
        }

        private long jobId;
        [Range(1, Int64.MaxValue, ErrorMessage = "انتخاب شغل الزامی است")]
        public long JobId
        {
            get { return jobId; }
            set { this.SetField(p => p.JobId, ref jobId, value); }
        }

        private long? parentJobPositionId;
        public long? ParentJobPositionId
        {
            get { return parentJobPositionId; }
            set { this.SetField(p => p.ParentJobPositionId, ref parentJobPositionId, value); }
        }

        

        

        
    }
}
