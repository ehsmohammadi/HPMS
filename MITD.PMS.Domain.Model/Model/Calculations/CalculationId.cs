using MITD.Domain.Model;
using System;

namespace MITD.PMS.Domain.Model.Calculations
{
    [Serializable]
    public class CalculationId:IValueObject<CalculationId>
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
        protected CalculationId()
        {

        }

        public CalculationId(long id)
        {
            this.id = id;
        } 
        #endregion

        #region IValueObject Member
        public virtual bool SameValueAs(CalculationId other)
        {
            return Id.Equals(other.Id);
        } 
        #endregion

        #region Object Override 
        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            var other = (CalculationId)obj;
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

        public static bool operator ==(CalculationId left, CalculationId right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(CalculationId left, CalculationId right)
        {
            return !(left == right);
        }
        #endregion
    }
}
