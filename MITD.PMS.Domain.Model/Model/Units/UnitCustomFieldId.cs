using MITD.Core.Builders;
using MITD.Domain.Model;
using MITD.PMS.Domain.Model.Periods;

namespace MITD.PMS.Domain.Model.Units
{
    public class UnitCustomFieldId : ObjectWithDbId<long>, IValueObject<UnitCustomFieldId>
    {

        #region Properties

        private SharedUnitCustomFieldId sharedUnitCustomFieldId;
        public virtual SharedUnitCustomFieldId SharedUnitCustomFieldId
        {
            get { return sharedUnitCustomFieldId; } 
        }


        private SharedUnitId sharedUnitId;
        public virtual SharedUnitId SharedUnitId
        {
            get { return sharedUnitId; }
        }

        private PeriodId periodId;
        public virtual PeriodId PeriodId
        {
            get { return periodId; }
        }

        #endregion

        #region Constructors
        // for Or mapper
        protected UnitCustomFieldId()
        {

        }

        public UnitCustomFieldId(PeriodId periodId, SharedUnitCustomFieldId sharedUnitCustomFieldId, SharedUnitId sharedUnitId)
        {
            this.sharedUnitCustomFieldId = sharedUnitCustomFieldId;
            this.sharedUnitId = sharedUnitId;
            this.periodId = periodId;
        }
        #endregion

        #region IValueObject Member
        public bool SameValueAs(UnitCustomFieldId other)
        {
            return new EqualsBuilder()
                .Append(this.PeriodId, other.PeriodId)
                .Append(this.SharedUnitId, other.SharedUnitId)
                .Append(this.SharedUnitCustomFieldId, other.SharedUnitCustomFieldId).IsEquals();
            
        }
        #endregion

        #region Object Override
        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            var other = (UnitCustomFieldId)obj;
            return SameValueAs(other);
        }
        public override int GetHashCode()
        {
            return new HashCodeBuilder().Append(PeriodId).Append(SharedUnitId).Append(SharedUnitCustomFieldId).ToHashCode();
        }

        public override string ToString()
        {
            return "Unitid:" + SharedUnitId + ";periodId:" + periodId + ";SharedUnitCustomFieldId:" + SharedUnitCustomFieldId;
        }

        public static bool operator ==(UnitCustomFieldId left, UnitCustomFieldId right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(UnitCustomFieldId left, UnitCustomFieldId right)
        {
            return !(left == right);
        }
        #endregion
    }
}
