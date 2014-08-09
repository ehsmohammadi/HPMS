using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MITD.Core;
using MITD.PMS.Common;

namespace MITD.PMS.Exceptions
{
    [Serializable]
    public class PMSException :Exception,IException
    {
        public PMSException()
            : base("Operation Failed")
        {
            Code = (int)ApiExceptionCode.Unknown;
        }

        public PMSException(string message)
            : base(message)
        {
            Code = (int)ApiExceptionCode.Unknown;
        }
        public PMSException(int code, string message)
            : base(message)
        {
            Code = code;
        }


        public PMSException(string message, Exception ex)
            : base(message,ex)
        {
            Code = (int)ApiExceptionCode.Unknown;
        }

        public PMSException(int code, string message, Exception ex)
            : base(message,ex)
        {
            Code = code;

        }

        public int Code { get; private set; }
    }

   




}
