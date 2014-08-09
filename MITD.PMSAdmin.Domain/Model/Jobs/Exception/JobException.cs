using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.PMSAdmin.Exceptions;

namespace MITD.PMSAdmin.Domain.Model.Jobs
{
    public class JobException : PMSAdminException
    {
        public JobException(string message):base(message)
        {
        }

        public JobException(string message,Exception ex)
            : base(message,ex)
        {
        }
        public JobException(int code,string message, Exception ex)
            : base(code,message, ex)
        {
        }
    }
}
