using System;
using System.ComponentModel.DataAnnotations;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class JobInPeriodAssignment
    {

        
        private long periodId;
        private long jobId;



        [Range(1, Int64.MaxValue, ErrorMessage = "انتخاب دوره الزامی است")]
        public long PeriodId
        {
            get { return periodId; }
            set { this.SetField(p => p.PeriodId, ref periodId, value); }
        }

       [Range(1, Int64.MaxValue, ErrorMessage = "انتخاب شغل الزامی است")]
        public long JobId
        {
            get { return jobId; }
            set { this.SetField(p => p.JobId, ref jobId, value); }
        }


       
    }
}
