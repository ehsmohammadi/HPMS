
using System.Collections.Generic;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class InquirySubjectDTO : IActionDTO 
    {
        private string employeeNo;
        public string EmployeeNo
        {
            get { return employeeNo; }
            set { this.SetField(p => p.EmployeeNo, ref employeeNo, value); }
        }

        private long jobPositionId;
        public long JobPositionId
        {
            get { return jobPositionId; }
            set { this.SetField(p => p.JobPositionId, ref jobPositionId, value); }
        }

        private string jobPositionName;
        public string JobPositionName
        {
            get { return jobPositionName; }
            set { this.SetField(p => p.JobPositionName, ref jobPositionName, value); }
        }

        private string fullName;
        public string FullName
        {
            get { return fullName; }
            set { this.SetField(p => p.FullName, ref fullName, value); }
        }


        private long inquirerJobPositionId;
        public long InquirerJobPositionId
        {
            get { return inquirerJobPositionId; }
            set { this.SetField(p => p.InquirerJobPositionId, ref inquirerJobPositionId, value); }
        }

        private string inquirerJobPositionName;
        public string InquirerJobPositionName
        {
            get { return inquirerJobPositionName; }
            set { this.SetField(p => p.InquirerJobPositionName, ref inquirerJobPositionName, value); }
        }



        private bool isInquired;
        public bool IsInquired
        {
            get { return isInquired; }
            set { this.SetField(p => p.IsInquired, ref isInquired, value); }
        }

        public List<int> ActionCodes
        {
            get; set;
        }
    }
}
