using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class PeriodJobsDTO
    {

        private long periodId;
        //private string periodName;
        private PageResultDTO<JobInPeriodDTO> jobs;

        public long PeriodId
        {
            get { return periodId; }
            set { this.SetField(p => p.PeriodId, ref periodId, value); }
        }

        //public string PeriodName
        //{
        //    get { return periodName; }
        //    set { this.SetField(p => p.PeriodName, ref periodName, value); }
        //}

        public PageResultDTO<JobInPeriodDTO> Jobs
        {
            get { return jobs; }
            set { this.SetField(p => p.Jobs, ref jobs, value); }
        }
       
    }
}
