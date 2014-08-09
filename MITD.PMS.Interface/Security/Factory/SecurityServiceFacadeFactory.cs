using MITD.Core;
using MITD.PMS.Domain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MITD.PMS.Interface
{
    public static class SecurityServiceFacadeFactory 
    {
        public static ISecurityServiceFacade Create()
        {
            return ServiceLocator.Current.GetInstance<ISecurityServiceFacade>();
        }

        public static void Release(ISecurityServiceFacade instance)
        {
            ServiceLocator.Current.Release(instance);
        }
    }

    public static class LogServiceFacadeFactory
    {
        public static IServiceLifeCycleManager<ILogServiceFacade> Create()
        {
            return ServiceLocator.Current.GetInstance<IServiceLifeCycleManager<ILogServiceFacade>>();
        }

        public static void Release(IServiceLifeCycleManager<ILogServiceFacade> instance)
        {
            ServiceLocator.Current.Release(instance);
        }
    }
}
