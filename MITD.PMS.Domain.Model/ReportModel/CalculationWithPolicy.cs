using MITD.PMS.Domain.Model.Calculations;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Domain.Model.Policies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MITD.PMSReport.Domain.Model
{
    public class CalculationWithPolicyAndPeriod
    {
        public Calculation Calculation { get; set; }
        public Policy Policy { get; set; }
        public Period Period { get; set; }
    }
}
