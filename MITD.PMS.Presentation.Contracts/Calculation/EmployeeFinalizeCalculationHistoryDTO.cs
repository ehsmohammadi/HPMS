using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class EmployeeFinalizeCalculationHistoryDTO 
    {
        private long employeeId;
        public long EmployeeId
        {
            get { return employeeId; }
            set { this.SetField(p => p.EmployeeId, ref employeeId, value); }
        }

        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set { this.SetField(p => p.FirstName, ref firstName, value); }
        }


        private List<EmployeeFinalizeCalculationInPeriodDTO> employeeFinalizeCalculationList;
        public List<EmployeeFinalizeCalculationInPeriodDTO> EmployeeFinalizeCalculationList
        {
            get { return employeeFinalizeCalculationList; }
            set { this.SetField(p => p.employeeFinalizeCalculationList, ref employeeFinalizeCalculationList, value); }
        }

      
    }
}
