using MITD.PMS.Domain.Model.Calculations;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Domain.Model.JobIndices;
using MITD.PMS.Domain.Model.JobPositions;
using MITD.PMS.Domain.Model.Periods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MITD.PMS.Domain.Model.JobIndexPoints
{
    public abstract class JobPositionPoint : EmployeePoint
    {
        private readonly JobPositionId jobPositionId;

        public virtual JobPositionId JobPositionId
        {
            get { return jobPositionId; }
        }

        protected JobPositionPoint()
            : base()
        {
        }
        public JobPositionPoint(CalculationPointId id, Period period, Employee employee,
            Calculation calculation, JobPosition jobPosition, string name, decimal value, bool isFinal = false)
            : base(id, period, employee, calculation, name, value, isFinal)
        {
            this.jobPositionId = jobPosition.Id;
        }

    }
}
