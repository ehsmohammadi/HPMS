using MITD.Core;
using MITD.PMS.Domain.Model.Calculations;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Domain.Model.Policies;

namespace MITD.PMS.Application.Contracts
{
    public interface ICalculationService : IService
    {
        Calculation AddCalculation(PolicyId policyId, PeriodId periodId, 
            string name, string employeeIdDelimitedList);

        void RunCalculation(CalculationId calculationId);

        CalculationStateReport GetCalculationState(CalculationId calculationId);

        Calculation GetCalculation(CalculationId calculationId);

        void StopCalculation(CalculationId calculationId);

        void PauseCalculation(CalculationId calculationId);

        void DeleteCalculation(CalculationId calculationId);
        Calculation GetDeterministicCalculation(PeriodId periodId);
        void ChangeDeterministicStatus(CalculationId calculationId, bool isDeterministic);
    }
}
