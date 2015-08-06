using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Integration.PMS.Contract
{
    public interface IPMSDataPusher
    {
        void insertEmployee(EmployeeDTO employee);
    }
}
