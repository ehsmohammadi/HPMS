using MITD.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MITD.PMSSecurity.Domain.Service;

namespace MITD.PMSSecurity.Domain.Service
{
    public class LoggerServiceFactory : ILoggerServiceFactory
    {
        public ILoggerService Create(string key)
        {
            return ServiceLocator.Current.GetInstance<ILoggerService>(key);
        }

        public void Release(ILoggerService loggerService)
        {
            ServiceLocator.Current.Release(loggerService);
        }
    }

}
