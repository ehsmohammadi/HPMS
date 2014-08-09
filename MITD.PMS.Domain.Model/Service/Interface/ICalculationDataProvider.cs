using MITD.Core;
using MITD.PMS.Domain.Model;
using MITD.PMS.Domain.Model.Calculations;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Domain.Model.JobIndexPoints;
using MITD.PMS.RuleContracts;
using Employee = MITD.PMS.Domain.Model.Employees.Employee;

namespace MITD.PMS.Domain.Service
{
    public interface ICalculationDataProvider : IService
    {
        CalculationData Provide(Employee employee, out PMSReport.Domain.Model.CalculationData employeeData, Calculation calculation, bool withCalculationPoint, CalculatorSession pathNo);

        CalculationPointPersistanceHolder
            Convert(RuleResult points, PMSReport.Domain.Model.CalculationData employeeData,
            PMS.Domain.Model.Employees.Employee employee, PMS.Domain.Model.Periods.Period period,
            PMS.Domain.Model.Calculations.Calculation calculation);
    }
}
