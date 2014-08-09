using MITD.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MITD.PMS.Domain.Model.Calculations
{
    public class CalculationForPathCompleted : IDomainEvent<CalculationForPathCompleted>
    {
        public CalculationForPathCompleted()
        {
        }
        public bool SameEventAs(CalculationForPathCompleted other)
        {
            return true;
        }
    }
}
