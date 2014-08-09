using System;
using MITD.Core.Builders;
using MITD.Domain.Model;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Domain.Model.Calculations;
using MITD.PMS.Domain.Model.JobIndices;


namespace MITD.PMS.Domain.Model.JobIndexPoints
{
    [Serializable]
    public class CalculationPointId : IValueObject<CalculationPointId>
    {
        #region Fields
        private readonly long id;
        #endregion

        #region Properties
        public long Id { get { return id; } }
        #endregion

        #region Constructors
        // for Or mapper
        protected CalculationPointId()
        {

        }

        public CalculationPointId(long id)
        {
            this.id = id;
        }
        #endregion

        #region IValueObject Member
        public virtual bool SameValueAs(CalculationPointId other)
        {
            return new EqualsBuilder()
                .Append(this.id, other.Id).IsEquals();
        }
        #endregion

        #region Object Override
        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            var other = (CalculationPointId)obj;
            return SameValueAs(other);
        }
        public override int GetHashCode()
        {
            return new HashCodeBuilder()
                .Append(id).ToHashCode();
        }

        public override string ToString()
        {
            return id.ToString();
        }

        public static bool operator ==(CalculationPointId left, CalculationPointId right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(CalculationPointId left, CalculationPointId right)
        {
            return !(left == right);
        }

        #endregion
    }
}
