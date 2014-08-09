using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MITD.PMS.Domain.Exceptions
{
    public interface IDataAccessExceptionAdapter
    {
        Exception Convert(Exception exp);
    }
}
