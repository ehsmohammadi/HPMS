using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Domain.Model.Units;

namespace MITD.PMSReport.Domain.Model
{
    public class EmployeeIdWithUnitId
    {
        public EmployeeId EmployeeId { get; set; }
        public UnitId UnitId { get; set; }
    }
}
