using MITD.PMS.Domain.Model.Calculations;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Domain.Model.JobPositions;
using MITD.PMS.Domain.Model.Periods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MITD.PMS.Domain.Model.JobIndexPoints
{
    public class SummaryJobPositionPoint : JobPositionPoint
    {
        protected SummaryJobPositionPoint()
            : base()
        {
        }
        public SummaryJobPositionPoint(CalculationPointId id, Period period, Employee employee,
            Calculation calculation, JobPosition jobPosition, string name, decimal value, bool isFinal = false)
            : base(id, period, employee, calculation, jobPosition, name, value, isFinal)
        {
        }
    }
}
