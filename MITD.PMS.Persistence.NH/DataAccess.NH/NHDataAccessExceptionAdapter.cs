using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.PMS.Domain.Exceptions;

namespace MITD.PMS.Persistence.NH.DataAccess.NH
{
    public class NHDataAccessExceptionAdapter : IDataAccessExceptionAdapter
    {
        public Exception Convert(Exception exp)
        {
            throw new Exception("Business Exp occured");
        }
    }
}
