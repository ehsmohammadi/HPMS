
using System.Collections.Generic;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class InquirerDTO
    {
        private string employeeNo;
        public string EmployeeNo
        {
            get { return employeeNo; }
            set { this.SetField(p => p.EmployeeNo, ref employeeNo, value); }
        }

        private string fullName;
        public string FullName
        {
            get { return fullName; }
            set { this.SetField(p => p.FullName, ref fullName, value); }
        }

        public string FullNameWithJobPositionName
        {
            get { return FullName + " - " + EmployeeJobPositionName; }
        }

        private long employeeJobPositionId;
        public long EmployeeJobPositionId
        {
            get { return employeeJobPositionId; }
            set { this.SetField(p => p.EmployeeJobPositionId, ref employeeJobPositionId, value); }
        }

        private string employeeJobPositionName;
        public string EmployeeJobPositionName
        {
            get { return employeeJobPositionName; }
            set { this.SetField(p => p.EmployeeJobPositionName, ref employeeJobPositionName, value); }
        }


        private bool isPermitted;
        public bool IsPermitted
        {
            get { return isPermitted; }
            set { this.SetField(p => p.IsPermitted, ref isPermitted, value); }
        }

    }
}
