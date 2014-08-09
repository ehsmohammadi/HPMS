using MITD.Core;
using MITD.PMS.Domain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MITD.PMS.Application
{
    public class CalculationDataProviderFactory:ICalculationDataProviderFactory
    {
        public Domain.Service.ICalculationDataProvider Create()
        {
            return ServiceLocator.Current.GetInstance<ICalculationDataProvider>();
        }

        public void Release(Domain.Service.ICalculationDataProvider provider)
        {
            ServiceLocator.Current.Release(provider);
        }
    }
}
