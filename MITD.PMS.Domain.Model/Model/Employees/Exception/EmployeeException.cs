using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.PMS.Exceptions;

namespace MITD.PMS.Domain.Model.Employees
{
    public class EmployeeException : PMSException
    {
        public EmployeeException(string message):base(message)
        {
        }

        public EmployeeException(string message,Exception ex)
            : base(message,ex)
        {
        }
        public EmployeeException(int code,string message, Exception ex)
            : base(code,message, ex)
        {
        }

        public EmployeeException(int code, string message)
            : base(code, message)
        {
        }
    }
}
