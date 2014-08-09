using MITD.Core;


namespace MITD.PMSSecurity.Domain.Service
{
    public interface ILoggerServiceFactory : IService
    {
        ILoggerService Create(string key);
        void Release(ILoggerService loggerService);
    }

    
}
