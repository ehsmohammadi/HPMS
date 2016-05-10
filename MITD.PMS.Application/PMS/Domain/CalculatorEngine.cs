using MITD.Core;
using MITD.PMS.Domain.Model.CalculationExceptions;
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
using System.Threading.Tasks;
using System.Transactions;

namespace MITD.PMS.Application
{
    public class CalculatorEngine : ICalculatorEngine
    {
        private IPolicyRepository policyRep;
        private IPeriodRepository periodRep;
        private IEmployeeRepository empRep;
        private ICalculationDataProviderFactory calculationDataProviderFactory;
        private readonly ICalculationExceptionRepository calcExpRep;
        private ICalculationRepository calcRep;
        private IJobIndexPointRepository jipRep;

        public CalculatorEngine(IPolicyRepository policyRep,
            IPeriodRepository periodRep,
            ICalculationRepository calcRep,
            IJobIndexPointRepository jipRep,
            IEmployeeRepository empRep, ICalculationDataProviderFactory calculationDataProviderFactory,ICalculationExceptionRepository calcExpRep)
        {
            this.policyRep = policyRep;
            this.periodRep = periodRep;
            this.empRep = empRep;
            this.calcRep = calcRep;
            this.jipRep = jipRep;
            this.calculationDataProviderFactory = calculationDataProviderFactory;
            this.calcExpRep = calcExpRep;
        }

        public void fetchPolicyAndEmployees(Calculation calculation, bool doResume, int pathNo, out Policy policy, out Dictionary<int, IList<Employee>> employees, out Period period)
        {
            using (var transaction = new TransactionScope())
            {
                policy = policyRep.GetById(calculation.PolicyId);
                if (policy is RuleEngineBasedPolicy)
                {
                    var dummy = (policy as RuleEngineBasedPolicy).RuleFunctions;
                    var dummy2 = (policy as RuleEngineBasedPolicy).Rules;
                }
                period = periodRep.GetById(calculation.PeriodId);

                var en = calculation.EmployeeIdList.Select(i => i.EmployeeNo).ToList();
                if (!doResume)
                {
                    employees = empRep.FindInListWithPath(en, calculation.PeriodId, pathNo);
                }
                else
                {
                    employees = empRep.FindRemainingEmployeesOfCalculation(en, calculation.PeriodId, calculation.Id, pathNo);
                }
                transaction.Complete();
            }
        }

        public CalculationPointPersistanceHolder CalculateIndices(Calculation calculation, Policy policy, Period period, Employee employee, IEventPublisher publisher, CalculatorSession calculationSession)
        {
            CalculationPointPersistanceHolder pointsHolder;
            //using (var transaction = new TransactionScope())
            //{
                var provider = calculationDataProviderFactory.Create();
                try
                {
                    pointsHolder = policy.CalculateFor(DateTime.Now, employee, period, calculation, provider, publisher, calculationSession);
                }
                finally
                {
                    calculationDataProviderFactory.Release(provider);
                }
            //    transaction.Complete();
            //}
            return pointsHolder;
        }

        public void UpdateCalculationResult(Calculation calculation, CalculationProgress progress,
            IJobIndexPointCalculator calculator, List<string> messages)
        {
            using (var transaction = new TransactionScope())
            {
                var calc = calcRep.GetById(calculation.Id);

                if (progress.LastCalculatedEmployeeId != null)
                    calc.UpdateCalculationResult(progress.TotalItem, progress.ItemsCalculated, messages, progress.LastCalculatedPath.Value, empRep.GetBy(progress.LastCalculatedEmployeeId));
                else
                    calc.UpdateCalculationResult(progress.TotalItem, progress.ItemsCalculated, messages);

                calc.Finish(calculator);
                transaction.Complete();
            }

        }

        public void UpdateCompileResult(CalculationId id, string libraryText, Dictionary<string, Core.RuleEngine.Model.RuleBase> rules)
        {
            using (var transaction = new TransactionScope())
            {
                var calc = calcRep.GetById(id);
                calc.UpdateCompileResult(libraryText, rules);
                transaction.Complete();
            }
        }

        public void AddEmployeePoints(List<CalculationPoint> points)
        {
            using (var transaction = new TransactionScope())
            {
                foreach (var point in points)
                {
                    jipRep.Add(point);
                }
                transaction.Complete();
            }
        }

        public void UpdateEmployeePoints(Dictionary<CalculationPointId, decimal> points)
        {
            using (var transaction = new TransactionScope())
            {
                foreach (var point in points)
                {
                    var employeePoint = jipRep.GetById(point.Key);
                    employeePoint.SetValue(point.Value);
                }
                transaction.Complete();
            }
        }

        public void AddUpdateCalculationPoint(List<SummaryCalculationPoint> points)
        {
            using (var transaction = new TransactionScope())
            {
                foreach (var point in points)
                {
                    var calcPoint = jipRep.GetById(point.Id);
                    if (calcPoint != null)
                        calcPoint.SetValue(point.Value);
                    else
                        jipRep.Add(point);

                }
                transaction.Complete();
            }
        }

        public List<SummaryCalculationPoint> GetCalculationPiontBy(CalculationId calcId)
        {
            return jipRep.GetCalculationPointBy(calcId);
        }

        public void AddCalculationException(CalculationId calculationId, EmployeeId employeeId, int calculationPathNo, string messages)
        {
            using (var transaction = new TransactionScope())
            {
                var clac=calcRep.GetById(calculationId);
                var empCalcExp = new EmployeeCalculationException(calcExpRep.GetNextId(), clac, employeeId,
                    calculationPathNo, messages);
                calcExpRep.Add(empCalcExp);             
                transaction.Complete();
            }
        }

        public void DeleteCalculationException(Calculation calculation)
        {
            using (var transaction = new TransactionScope())
            {
                calcExpRep.DeleteAll(calculation.Id);
                transaction.Complete();
            }
        }
    }
}
