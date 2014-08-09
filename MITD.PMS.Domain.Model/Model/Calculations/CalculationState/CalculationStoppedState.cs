using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MITD.PMS.Domain.Model.Calculations
{
    public class CalculationStoppedState : CalculationState 
    {
        public CalculationStoppedState()
            : base("4", "CalculationStopedState")
        {
        }

        
    }
}
