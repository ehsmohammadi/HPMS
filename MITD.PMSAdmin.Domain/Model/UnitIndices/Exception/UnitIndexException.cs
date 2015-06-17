using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.PMSAdmin.Exceptions;

namespace MITD.PMSAdmin.Domain.Model.UnitIndices
{
    public class UnitIndexException : PMSAdminException
    {
        public UnitIndexException(string message):base(message)
        {
        }

        public UnitIndexException(string message,Exception ex)
            : base(message,ex)
        {
        }
        public UnitIndexException(int code,string message, Exception ex)
            : base(code,message, ex)
        {
        }
    }
}
