using System.Collections.Generic;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Domain.Model.JobIndexPoints;
using MITD.PMS.Domain.Model.Periods;
using System;
using MITD.PMS.Domain.Model.Policies;
using MITD.Core;
using MITD.PMS.RuleContracts;
using MITD.PMS.Domain.Model.Calculations;
using Employee = MITD.PMS.Domain.Model.Employees.Employee;

namespace MITD.PMS.Domain.Service
{
    public interface IPolicyEngineService : IService, IDisposable
    {
        void SetupForCalculation(Policy policy, IEventPublisher publisher);
        CalculationPointPersistanceHolder CalculateFor(Employee employee, Period period, Calculation calculation, ICalculationDataProvider calculationDataProvider, CalculatorSession pathNo);
        bool HasBeenSetup { get; }
    }

    public interface IPolicyEngineService<T>: IPolicyEngineService where T: Policy
    {
        void SetupForCalculation(T policy, IEventPublisher publisher);
    }
}
