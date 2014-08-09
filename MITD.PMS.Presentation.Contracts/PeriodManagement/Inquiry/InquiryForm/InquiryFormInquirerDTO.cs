
using System.Collections.Generic;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class InquiryFormInquirerDTO
    {
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

        private string inquirerFname;
        public string InquirerFname
        {
            get { return inquirerFname; }
            set { this.SetField(p => p.InquirerFname, ref inquirerFname, value); }
        }

        private string inquirerLname;
        public string InquirerLname
        {
            get { return inquirerLname; }
            set { this.SetField(p => p.InquirerLname, ref inquirerLname, value); }
        }
        
        private List<JobIndexValueDTO> jobIndexValueList;
        public List<JobIndexValueDTO> JobIndexValueList
        {
            get { return jobIndexValueList; }
            set { this.SetField(p => p.JobIndexValueList, ref jobIndexValueList, value); }
        }

       
    }
}
