using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MITD.PMS.Domain.Model.Calculations
{
    internal class CalculationPausedState : CalculationState 
    {
        public CalculationPausedState()
            : base("3", "CalculationPausedState")
        {
        }

        internal override void Run(Calculation calculation, Service.IJobIndexPointCalculator calculator)
        {
            calculator.Resume(calculation);
            calculation.State = new CalculationRunningState();
        }

        internal override void Stop(Calculation calculation, Service.IJobIndexPointCalculator calculator)
        {
            calculator.Stop(calculation);
            calculation.State = new CalculationStoppedState();
        }
    }
}
