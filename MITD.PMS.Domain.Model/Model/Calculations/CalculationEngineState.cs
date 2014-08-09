using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MITD.PMS.Domain.Model.Calculations
{
    public class CalculationEngineState
    {

        public string StateName
        {
            get;
            set;
        }

        public int Percent
        {
            get;
            set;
        }
        public List<string> MessageList
        {
            get;
            set;
        }

    }
}
