using MITD.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class EmployeeUnitsDTO : DTOBase
    {
        private List<EmployeeUnitAssignmentDTO> employeeJobPositionAssignmentList;
        public List<EmployeeUnitAssignmentDTO> EmployeeJobPositionAssignmentList
        {
            get { return employeeJobPositionAssignmentList; }
            set { this.SetField(p => p.EmployeeJobPositionAssignmentList, ref employeeJobPositionAssignmentList, value); }
        }
    }
}
