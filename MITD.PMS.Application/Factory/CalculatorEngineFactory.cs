using MITD.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MITD.PMS.Application
{
    public class CalculatorEngineFactory:ICalculatorEngineFactory
    {
        public ICalculatorEngine Create()
        {
            return ServiceLocator.Current.GetInstance<ICalculatorEngine>();
        }

        public void Release(ICalculatorEngine engine)
        {
            ServiceLocator.Current.Release(engine);
        }
    }
}
