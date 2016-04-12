using MITD.Core;
using MITD.PMS.Application.Contracts;
using MITD.PMSSecurity.Application.Contracts;

namespace MITD.PMS.Application
{
    public interface IInquiryServiceFactory : IService
    {
        IServiceLifeCycleManager<IInquiryService> Create();
        void Release(IServiceLifeCycleManager<IInquiryService> inquiryServiceFactory);
    }

    public interface IUnitInquiryServiceFactory : IService
    {
        IServiceLifeCycleManager<IUnitInquiryService> Create();
        void Release(IServiceLifeCycleManager<IUnitInquiryService> inquiryServiceFactory);
    }

    public interface IJobPositionServiceFactory : IService
    {
        IServiceLifeCycleManager<IJobPositionService> Create();
        void Release(IServiceLifeCycleManager<IJobPositionService> serviceManager);
    }

    public interface IPeriodServiceFactory:IService
    {
        IServiceLifeCycleManager<IPeriodService> Create();
        void Release(IServiceLifeCycleManager<IPeriodService> periodService);
    }

    public interface IUnitServiceFactory : IService
    {
        IServiceLifeCycleManager<IUnitService> Create();
        void Release(IServiceLifeCycleManager<IUnitService> unitService);
    }

    public interface IJobServiceFactory : IService
    {
        IServiceLifeCycleManager<IJobService> Create();
        void Release(IServiceLifeCycleManager<IJobService> jobService);
    }

    public interface IJobIndexServiceFactory : IService
    {
        IServiceLifeCycleManager<IJobIndexService> Create();
        void Release(IServiceLifeCycleManager<IJobIndexService> jobService);
    }

    public interface IEmployeeServiceFactory : IService
    {
        IServiceLifeCycleManager<IEmployeeService> Create();
        void Release(IServiceLifeCycleManager<IEmployeeService> jobService);
    }

}
