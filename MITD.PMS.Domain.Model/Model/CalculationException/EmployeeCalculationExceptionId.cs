using MITD.Domain.Model;
using System;

namespace MITD.PMS.Domain.Model.CalculationExceptions
{

    public class EmployeeCalculationExceptionId : IValueObject<EmployeeCalculationExceptionId>
    {
        private long id;
        #region Properties
        public long Id
        {
            get { return id; }
        }
        #endregion

        #region Constructors
        // for Or mapper
        protected EmployeeCalculationExceptionId()
        {

        }

        public EmployeeCalculationExceptionId(long id)
        {
            this.id = id;
        }
        #endregion

        #region IValueObject Member
        public virtual bool SameValueAs(EmployeeCalculationExceptionId other)
        {
            return Id.Equals(other.Id);
        }
        #endregion

        #region Object Override
        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            var other = (EmployeeCalculationExceptionId)obj;
            return SameValueAs(other);
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override string ToString()
        {
            return Id.ToString();
        }

        public static bool operator ==(EmployeeCalculationExceptionId left, EmployeeCalculationExceptionId right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(EmployeeCalculationExceptionId left, EmployeeCalculationExceptionId right)
        {
            return !(left == right);
        }
        #endregion
    }
}
