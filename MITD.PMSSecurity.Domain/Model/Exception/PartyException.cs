using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.PMSSecurity.Exceptions;

namespace MITD.PMSSecurity.Domain
{
    public class PartyException : PMSSecurityException
    {
        public PartyException(string message):base(message)
        {
        }

        public PartyException(string message,Exception ex)
            : base(message,ex)
        {
        }
        public PartyException(int code,string message, Exception ex)
            : base(code,message, ex)
        {
        }

        public PartyException(int code, string message)
            : base(code, message)
        {
        }
    }
}
