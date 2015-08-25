
using System.Collections.Generic;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class InquiryUnitDTO  
    {
        private string employeeNo;
        public string EmployeeNo
        {
            get { return employeeNo; }
            set { this.SetField(p => p.EmployeeNo, ref employeeNo, value); }
        }

       

        private string indexName;
        public string IndexName
        {
            get { return indexName; }
            set { this.SetField(p => p.IndexName, ref indexName, value); }
        }

        private string fullName;
        public string FullName
        {
            get { return fullName; }
            set { this.SetField(p => p.FullName, ref fullName, value); }
        }


    
    }
}
