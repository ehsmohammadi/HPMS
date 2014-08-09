using System.Collections.Generic;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class InquirySubjectWithInquirersDTO
    {
        private string employeeNo;
        public string EmployeeNo
        {
            get { return employeeNo; }
            set { this.SetField(p => p.EmployeeNo, ref employeeNo, value); }
        }

        private string employeeName;
        public string EmployeeName
        {
            get { return employeeName; }
            set { this.SetField(p => p.EmployeeName, ref employeeName, value); }
        }

        //private long employeeJobPositionId;
        //public long EmployeeJobPositionId
        //{
        //    get { return employeeJobPositionId; }
        //    set { this.SetField(p => p.EmployeeJobPositionId, ref employeeJobPositionId, value); }
        //}

        //private string employeeJobPositionName;
        //public string EmployeeJobPositionName
        //{
        //    get { return employeeJobPositionName; }
        //    set { this.SetField(p => p.EmployeeJobPositionName, ref employeeJobPositionName, value); }
        //}

    }
}
