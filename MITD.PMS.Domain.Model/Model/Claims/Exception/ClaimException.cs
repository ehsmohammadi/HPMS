using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.PMS.Exceptions;

namespace MITD.PMS.Domain.Model.Claims
{
    public class ClaimException : PMSException
    {
        public ClaimException(string message):base(message)
        {
        }

        public ClaimException(string message,Exception ex)
            : base(message,ex)
        {
        }
        public ClaimException(int code,string message, Exception ex)
            : base(code,message, ex)
        {
        }

        public ClaimException(int code, string message)
            : base(code, message)
        {
        }
    }
}
