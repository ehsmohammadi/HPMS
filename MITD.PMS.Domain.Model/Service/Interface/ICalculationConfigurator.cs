using MITD.Domain.Model;
using MITD.PMS.Domain.Model.Calculations;

namespace MITD.PMS.Domain.Service
{
    public interface ICalculationConfigurator:IConfigurator
    {
        void Config(Calculation calculation);
    }
}
