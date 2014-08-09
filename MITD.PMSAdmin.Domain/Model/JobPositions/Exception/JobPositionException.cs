using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.PMSAdmin.Exceptions;

namespace MITD.PMSAdmin.Domain.Model.JobPositions
{
    public class JobPositionException : PMSAdminException
    {
        public JobPositionException(string message):base(message)
        {
        }

        public JobPositionException(string message,Exception ex)
            : base(message,ex)
        {
        }
        public JobPositionException(int code,string message, Exception ex)
            : base(code,message, ex)
        {
        }
    }
}
