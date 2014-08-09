using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.PMS.Exceptions;

namespace MITD.PMS.Domain.Model.JobIndices
{
    public class JobIndexException : PMSException
    {
        public JobIndexException(string message):base(message)
        {
        }

        public JobIndexException(string message,Exception ex)
            : base(message,ex)
        {
        }
        public JobIndexException(int code,string message, Exception ex)
            : base(code,message, ex)
        {
        }
    }
}
