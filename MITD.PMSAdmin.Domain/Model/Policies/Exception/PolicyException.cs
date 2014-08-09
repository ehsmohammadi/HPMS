using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.PMSAdmin.Exceptions;

namespace MITD.PMSAdmin.Domain.Model.Policies
{
    public class PolicyException : PMSAdminException
    {
        public PolicyException(string message):base(message)
        {
        }

        public PolicyException(string message,Exception ex)
            : base(message,ex)
        {
        }
        public PolicyException(int code,string message, Exception ex)
            : base(code,message, ex)
        {
        }
    }
}
