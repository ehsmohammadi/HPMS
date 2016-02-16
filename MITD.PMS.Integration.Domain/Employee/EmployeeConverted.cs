using System.Collections.Generic;
using MITD.Domain.Model;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Integration.Domain
{
    public class EmployeeConverted : IDomainEvent<EmployeeConverted>
    {
        private readonly List<EmployeeDTO> employeeList;

        public List<EmployeeDTO> EmployeeList
        {
            get { return employeeList; }
        }

        public EmployeeConverted(List<EmployeeDTO> employeeList)
        {
            this.employeeList = employeeList;
        }

        public bool SameEventAs(EmployeeConverted other)
        {
            return true;
        }
    }
}
