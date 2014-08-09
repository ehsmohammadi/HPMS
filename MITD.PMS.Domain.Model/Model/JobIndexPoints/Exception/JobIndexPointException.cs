using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.PMS.Exceptions;

namespace MITD.PMS.Domain.Model.JobIndexPoints
{
    public class JobIndexPointException : PMSException
    {
        public JobIndexPointException(string message):base(message)
        {
        }

        public JobIndexPointException(string message,Exception ex)
            : base(message,ex)
        {
        }
        
        public JobIndexPointException(int code, string message)
            : base(code, message)
        {
        }

        public JobIndexPointException(int code,string message, Exception ex)
            : base(code,message, ex)
        {
        }
    }
}
