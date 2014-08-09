using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.PMS.Exceptions;

namespace MITD.PMS.Domain.Model.Periods
{
    public class PeriodException : PMSException
    {
        public PeriodException(string message):base(message)
        {
        }

        public PeriodException(string message,Exception ex)
            : base(message,ex)
        {
        }
        public PeriodException(int code,string message, Exception ex)
            : base(code,message, ex)
        {
        }

        public PeriodException(int code, string message)
            : base(code, message)
        {
        }
    }
}
