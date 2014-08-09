using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.PMS.Domain.Model.JobPositions;
using MITD.PMS.Domain.Model.Employees;

namespace MITD.PMSReport.Domain.Model
{
    public class EmployeeIdWithJobPositionId
    {
        public EmployeeId EmployeeId { get; set; }
        public JobPositionId JobPositionId { get; set; }
    }
}
