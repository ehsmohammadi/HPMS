using MITD.Core;
using MITD.PMS.Application.Contracts;
using MITD.PMS.Domain.Model.Calculations;
using MITD.PMS.Domain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MITD.PMS.Application
{
    public class JobIndexPointCalculatorProvider : IJobIndexPointCalculatorProvider
    {
        object providerLock = new object();
        private System.Collections.Concurrent.ConcurrentDictionary<CalculationId, JobIndexPointCalculator> calculators =
                new System.Collections.Concurrent.ConcurrentDictionary<CalculationId, JobIndexPointCalculator>();

        public Domain.Service.IJobIndexPointCalculator GetCalculator(Domain.Model.Calculations.CalculationId calculationId)
        {
            lock (providerLock)
            {
                if (calculators.Any(i => !i.Key.Equals(calculationId) && i.Value.State == CalculatorState.Running))
                    throw new Exception("تنها اجرای یک محاسبه امکان پذیر است.");

                return calculators.AddOrUpdate(calculationId,
                    id => (JobIndexPointCalculator)ServiceLocator.Current.GetInstance<IJobIndexPointCalculator>(),
                    (id, old) =>
                    {
                        if (old != null)
                        {
                            if (old.State == CalculatorState.Running)
                                return old;
                            else ServiceLocator.Current.Release(old);
                        }
                        return (JobIndexPointCalculator)ServiceLocator.Current.GetInstance<IJobIndexPointCalculator>();
                    });
            }
        }
    }
}
