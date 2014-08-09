using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.PMS.Common;
using MITD.PMS.Domain.Model.Calculations;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Exceptions;
using MITD.Core;

namespace MITD.PMS.Domain.Service
{
    public static class CalculationDeterministicManagerService
    {
        private static Object lockObject = new object();

        public static void SetDeterministic(ICalculationRepository calculationRep, Calculation calculation)
        {
            lock (lockObject)
            {
                bool hasDeterministic = calculationRep.GetAll(calculation.PeriodId).Any(p => p.Id != calculation.Id && p.IsDeterministic);
                if (!hasDeterministic)
                    calculation.SetDeterministic();
                else
                    throw new PeriodException((int)ApiExceptionCode.ExceedViolationInDeterministicCalculationInPeriod, ApiExceptionCode.ExceedViolationInDeterministicCalculationInPeriod.DisplayName);

            }
        }

        public static Calculation GetDeterministicCalculation(Period period, ICalculationRepository calculationRep)
        {
            var calc = calculationRep.GetDeterministicCalculation(period);
            if(calc==null)
                throw new  CalculationException((int)ApiExceptionCode.DoesNotExistAnyDeterministicCalculationInPeriod,ApiExceptionCode.DoesNotExistAnyDeterministicCalculationInPeriod.DisplayName);
            return calc;
        }
    }
}
