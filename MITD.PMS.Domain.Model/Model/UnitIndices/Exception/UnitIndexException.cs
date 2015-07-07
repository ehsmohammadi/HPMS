using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.PMS.Exceptions;

namespace MITD.PMS.Domain.Model.UnitIndices
{
    public class UnitIndexException : PMSException
    {
        public UnitIndexException(string message):base(message)
        {
        }

        public UnitIndexException(string message,Exception ex)
            : base(message,ex)
        {
        }
        public UnitIndexException(int code, string message, Exception ex)
            : base(code,message, ex)
        {
        }
    }
}
