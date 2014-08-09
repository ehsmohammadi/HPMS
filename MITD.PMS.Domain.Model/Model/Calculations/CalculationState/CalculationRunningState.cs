using MITD.PMS.Domain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MITD.PMS.Domain.Model.Calculations
{
    internal class CalculationRunningState : CalculationState 
    {
        public CalculationRunningState()
            : base("2", "CalculationRunningState")
        {
        }

        internal override void Pause(Calculation calculation, IJobIndexPointCalculator calculator)
        {
            calculator.Pause(calculation);
            calculation.State = new CalculationPausedState();
        }

        internal override void Stop(Calculation calculation, IJobIndexPointCalculator calculator)
        {
            calculator.Stop(calculation);
            calculation.State = new CalculationStoppedState();
        }
        
        internal override void Finished(Calculation calculation, IJobIndexPointCalculator calculator)
        {
            if(calculator.CalculatorSession.HasEmployeeCalculationFailed)
                calculation.State = new CalculationNotCompletedState();
            else
                calculation.State = new CalculationCompletedState();
        }

    }
}
