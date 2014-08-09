using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MITD.PMS.Domain.Model.Calculations
{
    public class CalculationNotCompletedState : CalculationState 
    {
        public CalculationNotCompletedState()
            : base("6", "CalculationNotCompletedState")
        {
        }

        internal override  void Run(Calculation calculation, Service.IJobIndexPointCalculator calculator)
        {
            calculator.Resume(calculation);
            calculation.State = new CalculationRunningState();
        }
    }
}
