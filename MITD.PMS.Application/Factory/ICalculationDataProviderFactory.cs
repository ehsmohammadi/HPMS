using MITD.Core;
using MITD.PMS.Domain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MITD.PMS.Application
{
    public interface ICalculationDataProviderFactory:IService
    {
        ICalculationDataProvider Create();
        void Release(ICalculationDataProvider provider);
    }
}
