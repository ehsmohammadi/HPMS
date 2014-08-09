using MITD.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MITD.PMS.Application
{
    public interface ICalculatorEngineFactory: IService
    {
        ICalculatorEngine Create();
        void Release(ICalculatorEngine engine);
    }
}
