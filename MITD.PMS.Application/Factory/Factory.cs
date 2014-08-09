using MITD.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.PMS.Application.Contracts;
using MITD.PMSSecurity.Application.Contracts;

namespace MITD.PMS.Application
{
    public class InquiryServiceFactory : IInquiryServiceFactory
    {
        public IServiceLifeCycleManager<IInquiryService> Create()
        {
            return ServiceLocator.Current.GetInstance<IServiceLifeCycleManager<IInquiryService>>();
        }

        public void Release(IServiceLifeCycleManager<IInquiryService> inquiryService)
        {
            ServiceLocator.Current.Release(inquiryService);
        }
    }

    public class JobPositionServiceFactory : IJobPositionServiceFactory
    {
        public IServiceLifeCycleManager<IJobPositionService> Create()
        {
            return ServiceLocator.Current.GetInstance<IServiceLifeCycleManager<IJobPositionService>>();
        }

        public void Release(IServiceLifeCycleManager<IJobPositionService> serviceManager)
        {
            ServiceLocator.Current.Release(serviceManager);
        }
    }
    public class PeriodServiceFactory : IPeriodServiceFactory
    {

        public IServiceLifeCycleManager<IPeriodService> Create()
        {
            return ServiceLocator.Current.GetInstance<IServiceLifeCycleManager<IPeriodService>>();
        }

        public void Release(IServiceLifeCycleManager<IPeriodService> periodService)
        {
            ServiceLocator.Current.Release(periodService);
        }
    }

    public class UnitServiceFactory : IUnitServiceFactory
    {
        public IServiceLifeCycleManager<IUnitService> Create()
        {
            return ServiceLocator.Current.GetInstance<IServiceLifeCycleManager<IUnitService>>();
        }

        public void Release(IServiceLifeCycleManager<IUnitService> unitService)
        {
            ServiceLocator.Current.Release(unitService);
        }
    }

    public class JobServiceFactory : IJobServiceFactory
    {
        public IServiceLifeCycleManager<IJobService> Create()
        {
            return ServiceLocator.Current.GetInstance<IServiceLifeCycleManager<IJobService>>();
        }

        public void Release(IServiceLifeCycleManager<IJobService> jobService)
        {
            ServiceLocator.Current.Release(jobService);
        }
    }


    public class JobIndexServiceFactory : IJobIndexServiceFactory
    {
        public IServiceLifeCycleManager<IJobIndexService> Create()
        {
            return ServiceLocator.Current.GetInstance<IServiceLifeCycleManager<IJobIndexService>>();
        }

        public void Release(IServiceLifeCycleManager<IJobIndexService> jobIndexService)
        {
            ServiceLocator.Current.Release(jobIndexService);
        }
    }


    public static class LogServiceFactory
    {
        public static IServiceLifeCycleManager<ILogService> Create()
        {
            return ServiceLocator.Current.GetInstance<IServiceLifeCycleManager<ILogService>>();
        }

        public static void Release(IServiceLifeCycleManager<ILogService> logService)
        {
            ServiceLocator.Current.Release(logService);
        }
    }

    public class EmployeeServiceFactory : IEmployeeServiceFactory
    {
        public IServiceLifeCycleManager<IEmployeeService> Create()
        {
            return ServiceLocator.Current.GetInstance<IServiceLifeCycleManager<IEmployeeService>>();
        }

        public void Release(IServiceLifeCycleManager<IEmployeeService> employeeService)
        {
            ServiceLocator.Current.Release(employeeService);
        }
    }
}
