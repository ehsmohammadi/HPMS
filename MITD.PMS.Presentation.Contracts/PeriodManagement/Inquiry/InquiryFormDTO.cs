
using System.Collections.Generic;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class InquiryFormDTO
    {
        private long periodId;
        public long PeriodId
        {
            get { return periodId; }
            set { this.SetField(p => p.PeriodId, ref periodId, value); }
        }



        private string inquirerEmployeeNo;
        public string InquirerEmployeeNo
        {
            get { return inquirerEmployeeNo; }
            set { this.SetField(p => p.InquirerEmployeeNo, ref inquirerEmployeeNo, value); }
        }

        private long inquirerJobPositionId;
        public long InquirerJobPositionId
        {
            get { return inquirerJobPositionId; }
            set { this.SetField(p => p.InquirerJobPositionId, ref inquirerJobPositionId, value); }
        }

        private long jobPositionId;
        public long JobPositionId
        {
            get { return jobPositionId; }
            set { this.SetField(p => p.JobPositionId, ref jobPositionId, value); }
        }

        private string inquirySubjectEmployeeNo;
        public string InquirySubjectEmployeeNo
        {
            get { return inquirySubjectEmployeeNo; }
            set { this.SetField(p => p.InquirerEmployeeNo, ref inquirySubjectEmployeeNo, value); }
        }

        private List<JobIndexValueDTO> jobIndexValueList;
        public List<JobIndexValueDTO> JobIndexValueList
        {
            get { return jobIndexValueList; }
            set { this.SetField(p => p.JobIndexValueList, ref jobIndexValueList, value); }
        }

       
    }
}
