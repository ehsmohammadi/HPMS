using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.PMSAdmin.Exceptions;

namespace MITD.PMSAdmin.Domain.Model.JobIndices
{
    public class JobIndexException : PMSAdminException
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
