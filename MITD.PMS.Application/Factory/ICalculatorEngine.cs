using MITD.Core;
using MITD.PMS.Domain.Model.Calculations;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Domain.Model.JobIndexPoints;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Domain.Model.Policies;
using MITD.PMS.Domain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MITD.PMS.Application
{
    public interface ICalculatorEngine: IService
    {
        void fetchPolicyAndEmployees(Calculation calculation, bool doResume, int pathNo, out Policy policy, out Dictionary<int, IList<Employee>> employees, out Period period);
        CalculationPointPersistanceHolder CalculateIndices(Calculation calculation, Policy policy, Period period, Employee employee, IEventPublisher publisher, CalculatorSession pathNo);
        void UpdateCalculationResult(Calculation calculation, CalculationProgress progress,
            IJobIndexPointCalculator calculator, List<string> messages);
        void UpdateCompileResult(CalculationId id, string libraryText, Dictionary<string, Core.RuleEngine.Model.RuleBase> rules);
        void AddEmployeePoints(List<CalculationPoint> points);
        void UpdateEmployeePoints(Dictionary<CalculationPointId, decimal> points);
        void AddUpdateCalculationPoint(List<SummaryCalculationPoint> points);
        List<SummaryCalculationPoint> GetCalculationPiontBy(CalculationId calcId);
        void AddCalculationException(CalculationId calculationId, EmployeeId employeeId, int calculationPathNo, string messages);
        void DeleteCalculationException(Calculation calculation);
    }
}
