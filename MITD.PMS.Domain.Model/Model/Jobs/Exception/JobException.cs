using System;
using MITD.PMS.Exceptions;

namespace MITD.PMS.Domain.Model.Jobs
{
    public class JobException : PMSException
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
