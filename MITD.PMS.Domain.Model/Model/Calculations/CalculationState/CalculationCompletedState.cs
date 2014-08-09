using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.PMS.Domain.Service;

namespace MITD.PMS.Domain.Model.Calculations
{
    public class CalculationCompletedState : CalculationState 
    {
        public CalculationCompletedState()
            : base("5", "CalculationCompletedState")
        {
        }

        internal override void ChangeDeterministicStatus(Calculation calculation, bool isDeterministic,ICalculationRepository calcRep)
        {
            if (isDeterministic)
                CalculationDeterministicManagerService.SetDeterministic(calcRep, calculation);
            else
                calculation.UnsetDeterministic();
        }
    }
}
