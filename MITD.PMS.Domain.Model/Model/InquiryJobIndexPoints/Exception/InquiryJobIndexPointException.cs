using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.PMS.Exceptions;

namespace MITD.PMS.Domain.Model.InquiryJobIndexPoints
{
    public class InquiryJobIndexPointException : PMSException
    {
        public InquiryJobIndexPointException(string message):base(message)
        {
        }

        public InquiryJobIndexPointException(string message,Exception ex)
            : base(message,ex)
        {
        }
        public InquiryJobIndexPointException(int code,string message, Exception ex)
            : base(code,message, ex)
        {
        }
    }
}
