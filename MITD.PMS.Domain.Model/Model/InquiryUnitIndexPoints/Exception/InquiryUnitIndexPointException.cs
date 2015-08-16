using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.PMS.Exceptions;

namespace MITD.PMS.Domain.Model.InquiryUnitIndexPoints
{
    public class InquiryUnitIndexPointException : PMSException
    {
        public InquiryUnitIndexPointException(string message):base(message)
        {
        }

        public InquiryUnitIndexPointException(string message,Exception ex)
            : base(message,ex)
        {
        }
        public InquiryUnitIndexPointException(int code,string message, Exception ex)
            : base(code,message, ex)
        {
        }
    }
}
