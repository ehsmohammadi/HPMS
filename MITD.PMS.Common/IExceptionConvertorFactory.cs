using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.Core;

namespace MITD.Core
{
    public interface IExceptionConvertorFactory<T> where T : class
    {
        IExceptionConvertor<T> CreateExceptionConvertors();
    }
}
