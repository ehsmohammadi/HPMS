using MITD.Core.Builders;
using MITD.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MITD.PMS.Domain.Model.Calculations
{
    public class CalculationExceptionReady : IDomainEvent<CalculationExceptionReady>
    {
        private readonly string  messages;
        private readonly Calculations.CalculationId calculationId;
        private readonly Employees.EmployeeId employeeId;
        private readonly int pathNo;
        
        //public CalculationPointPersistanceHolder PointsHolder { get { return pointsHolder; } }

        public Employees.EmployeeId EmployeeId { get { return employeeId; } }
        public Calculations.CalculationId CalculationId { get { return calculationId; } }
        public int CalculationPathNo { get { return pathNo; } }
        public string Messages { get { return messages; } }

        public CalculationExceptionReady(Calculations.CalculationId calculationId, Employees.EmployeeId employeeId, int pathNo,Exception ex)
        {
            messages = ex.Message + "\n"+ex.StackTrace;
            this.calculationId = calculationId;
            this.employeeId = employeeId;
            this.pathNo = pathNo;
        }
        public bool SameEventAs(CalculationExceptionReady other)
        {
            return true;
        }
    }
}
