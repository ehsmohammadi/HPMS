using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.Domain.Model;
using MITD.PMS.Domain.Model.Calculations;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Domain.Model.Periods;

namespace MITD.PMS.Domain.Model.CalculationExceptions
{
    public  class EmployeeCalculationException : IEntity<EmployeeCalculationException>
    {
        #region Fields

        private readonly byte[] rowVersion;
        private readonly EmployeeCalculationExceptionId id;
        private readonly CalculationId calculationId;
        private readonly EmployeeId employeeId;
        private readonly int calculationPathNo;
        protected readonly string message;

        #endregion

        #region Properties

        public virtual EmployeeCalculationExceptionId Id { get { return id; } }
        public virtual EmployeeId EmployeeId { get { return employeeId; } }
        public virtual int CalculationPathNo { get { return calculationPathNo; } }
        public virtual string Message { get { return message; } }
        public virtual CalculationId CalculationId { get { return calculationId; } }

        #endregion

        #region Constructors
        protected EmployeeCalculationException()
        {
        }
        public EmployeeCalculationException(EmployeeCalculationExceptionId employeeCalculationExceptionId, Calculation calculation, EmployeeId employeeId, int calculationPathNo, string message)
        {
            if (employeeCalculationExceptionId == null)
                throw new ArgumentNullException("employeeCalculationExceptionId");
            this.id = employeeCalculationExceptionId;
            if(calculation == null)
                throw new ArgumentNullException("calculation");
            if (employeeId == null)
                throw new ArgumentNullException("employeeId");

            this.employeeId = employeeId;
            this.calculationId = calculation.Id;
            this.calculationPathNo = calculationPathNo;
            if (message.Length > 1024)
                this.message = message.Substring(0, 1023);
            else
                this.message = message;
        }
        #endregion

        #region IEntity Member

        public virtual bool SameIdentityAs(EmployeeCalculationException other)
        {
            return (other != null) && Id.Equals(other.Id);
        }

        #endregion

        #region Object override

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            var other = (EmployeeCalculationException)obj;
            return SameIdentityAs(other);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override string ToString()
        {
            return Id.ToString();
        }

        #endregion

       
    }
}
