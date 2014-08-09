using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.PMSAdmin.Exceptions;

namespace MITD.PMSAdmin.Domain.Model.Units
{
    public class UnitException : PMSAdminException
    {
        public UnitException(string message):base(message)
        {
        }

        public UnitException(string message,Exception ex)
            : base(message,ex)
        {
        }
        public UnitException(int code,string message, Exception ex)
            : base(code,message, ex)
        {
        }
    }
}
