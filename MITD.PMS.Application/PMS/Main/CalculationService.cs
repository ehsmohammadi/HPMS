using MITD.Core;
using MITD.Domain.Repository;
using MITD.PMS.Application.Contracts;
using MITD.PMS.Common;
using MITD.PMS.Domain.Model.Calculations;
using MITD.PMS.Domain.Model.JobIndexPoints;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Domain.Model.Policies;
using MITD.PMS.Domain.Service;
using System;
using System.Transactions;
using MITD.PMS.Exceptions;

namespace MITD.PMS.Application
{
    public class CalculationService : ICalculationService
    {
        private readonly ICalculationRepository calculationRep;
        private readonly IJobIndexPointCalculatorProvider calculatorProvider;
        private readonly IPeriodRepository periodRep;
        private readonly IPolicyRepository PolicyRep;

        public CalculationService(ICalculationRepository calculationRep,
                                    IPeriodRepository periodRep, 
                                    IPolicyRepository PolicyRep,
                                    IJobIndexPointCalculatorProvider calculatorProvider)
        {
            this.calculationRep = calculationRep;
            this.periodRep = periodRep;
            this.PolicyRep = PolicyRep;
            this.calculatorProvider = calculatorProvider;
        }

        public Calculation AddCalculation(PolicyId policyId, PeriodId periodId, string name, string employeeIdDelimitedList)
        {
            try
            {
                using (var tr = new TransactionScope())
                {
                    var calculation = new Calculation(calculationRep.GetNextId(), periodRep.GetById(periodId),
                                                      PolicyRep.GetById(policyId), name, DateTime.Now,
                                                      employeeIdDelimitedList);
                    calculationRep.Add(calculation);
                    tr.Complete();
                    return calculation;
                }
            }
            catch (Exception exp)
            {
                var res = calculationRep.TryConvertException(exp);
                if (res == null)
                    throw;
                throw res;
            }
        }

        public void RunCalculation(CalculationId calculationId)
        {
            var calculator = calculatorProvider.GetCalculator(calculationId);
            using (var tr = new TransactionScope())
            {
                var calculation = calculationRep.GetById(calculationId);
                calculation.Run(calculator);
                tr.Complete();
            }
        }

        public CalculationStateReport GetCalculationState(CalculationId calculationId)
        {
            var calculator = calculatorProvider.GetCalculator(calculationId);
            Calculation calculation;
            using (var tr = new TransactionScope())
            {
                calculation = calculationRep.GetById(calculationId);
                tr.Complete();
            }
            if (calculation.State == CalculationState.Running)
            {
                return new CalculationStateReport(calculation, calculator.Progress, calculator.Messages);
            }
            else
            {
                return new CalculationStateReport(calculation, null, null);
            }

        }
        public Calculation GetCalculation(CalculationId calculationId)
        {
            Calculation calculation;
            using (var tr = new TransactionScope())
            {
                calculation = calculationRep.GetById(calculationId);
                tr.Complete();
            }
            return calculation;
        }


        public void StopCalculation(CalculationId calculationId)
        {
            var calculator = calculatorProvider.GetCalculator(calculationId);
            using (var tr = new TransactionScope())
            {
                var calculation = calculationRep.GetById(calculationId);
                calculation.Stop(calculator);
                tr.Complete();
            }
        }

        public void PauseCalculation(CalculationId calculationId)
        {
            var calculator = calculatorProvider.GetCalculator(calculationId);
            using (var tr = new TransactionScope())
            {
                var calculation = calculationRep.GetById(calculationId);
                calculation.Pause(calculator);
                tr.Complete();
            }
        }

        public void DeleteCalculation(CalculationId calculationId)
        {
            try
            {
                using (var tr = new TransactionScope())
                {
                    var calculation = calculationRep.GetById(calculationId);
                    if (calculation.IsDeterministic )
                        throw new CalculationException((int)ApiExceptionCode.CouldNotDeleteDeterministicCalculation, ApiExceptionCode.CouldNotDeleteDeterministicCalculation.DisplayName); 
                    if (calculation.State == CalculationState.Running)
                        throw new CalculationInvalidStateOperationException(typeof(Calculation).Name, CalculationState.Running.DisplayName, System.Reflection.MethodBase.GetCurrentMethod().Name);
                    
                    calculationRep.Delete(calculation);
                    tr.Complete();
                }
            }
            catch (Exception exp)
            {
                var res = calculationRep.TryConvertException(exp);
                if (res == null)
                    throw;
                throw res;
            }
        }

        public Calculation GetDeterministicCalculation(PeriodId periodId)
        {
            var period = periodRep.GetById(periodId);
            return CalculationDeterministicManagerService.GetDeterministicCalculation(period, calculationRep);
        }

        public void ChangeDeterministicStatus(CalculationId calculationId, bool isDeterministic)
        {
            using (var scope = new TransactionScope())
            {
                var calculation = calculationRep.GetById(calculationId);
                var period = periodRep.GetById(calculation.PeriodId);
                calculation.ChangeDeterministicStatus(isDeterministic, calculationRep, period);
                scope.Complete();
            }
        }
    }
}
