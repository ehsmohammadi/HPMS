using MITD.Core;
using MITD.PMS.Domain.Model.Calculations;

namespace MITD.PMS.Application.Contracts
{
    public interface IJobIndexPointCalculatorService
    {
        bool Start(CalculationId calculationId);
        bool Stop(CalculationId calculationId);
    }
}
