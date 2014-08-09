using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MITD.Core
{
    internal class GeneralException:Exception, IException
    {
        public GeneralException(int code, string message):base(message)
        {
            Code = code;
        }

        public int Code { get; private set; }
    }
}
