using MITD.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class EmployeeJobPositionsDTO : DTOBase
    {
        private List<EmployeeJobPositionAssignmentDTO> employeeJobPositionAssignmentList;
        public List<EmployeeJobPositionAssignmentDTO> EmployeeJobPositionAssignmentList
        {
            get { return employeeJobPositionAssignmentList; }
            set { this.SetField(p => p.EmployeeJobPositionAssignmentList, ref employeeJobPositionAssignmentList, value); }
        }
    }
}
