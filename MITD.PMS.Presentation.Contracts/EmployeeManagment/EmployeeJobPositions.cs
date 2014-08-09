using MITD.Presentation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class EmployeeJobPositions
    {
        private long employeeId;
        private string fullName;
        private string lastName;

        public long EmployeeId
        {
            get { return employeeId; }
            set { this.SetField(p => p.EmployeeId, ref employeeId, value); }
        }

        public string FullName
        {
            get { return fullName; }
            set { this.SetField(p => p.FullName, ref fullName, value); }
        }

        //public List<Dto>  
        //{
        //    get { return lastName; }
        //    set { this.SetField(p => p.LastName, ref lastName, value); }
        //}


        public List<int> ActionCodes
        {
            get;
            set;
        }
    }

}
