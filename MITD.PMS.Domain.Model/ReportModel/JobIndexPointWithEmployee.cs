using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Domain.Model.JobIndexPoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MITD.PMSReport.Domain.Model
{
    public class JobIndexPointWithEmployee
    {
        public EmployeePoint JobIndexPoint { get; set; }
        public Employee Employee { get; set; }
    }
}
