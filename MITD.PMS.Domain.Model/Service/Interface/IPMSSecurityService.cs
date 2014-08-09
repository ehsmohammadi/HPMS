using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Domain.Model.Periods;

namespace MITD.PMS.Domain.Service
{
    public interface IPMSSecurityService
    {
        Employee GetCurrentLoginEmployee(Period period);
    }
}
