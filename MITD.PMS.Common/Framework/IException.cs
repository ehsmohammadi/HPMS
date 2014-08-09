using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MITD.Core
{
    public interface IException
    {
        int Code { get; }
        string Message{get; }
    }
}
