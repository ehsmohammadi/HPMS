using MITD.Core;
using MITD.PMS.Domain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MITD.PMS.Domain.Model.Calculations
{
    internal class CalculationInitState : CalculationState 
    {
        public CalculationInitState()
            : base("1", "CalculationInitState")
        {
        }

        internal override void Run(Calculation calculation, IJobIndexPointCalculator calculator)
        {
            calculator.Start(calculation);
            calculation.State = new CalculationRunningState();
        }
    }
}
