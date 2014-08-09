using MITD.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MITD.PMS.Domain.Model.Calculations
{
    public class CalculationCompleted : IDomainEvent<CalculationCompleted>
    {
        public CalculationCompleted()
        {
        }
        public bool SameEventAs(CalculationCompleted other)
        {
            return true;
        }
    }
}
