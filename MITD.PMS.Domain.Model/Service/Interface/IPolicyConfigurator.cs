using MITD.Domain.Model;
using MITD.PMS.Domain.Model.Policies;

namespace MITD.PMS.Domain.Service
{
    public interface IPolicyConfigurator:IConfigurator
    {
        void Config(Policy policy);
    }
}
