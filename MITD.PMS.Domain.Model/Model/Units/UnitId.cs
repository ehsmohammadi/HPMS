using MITD.Core.Builders;
using MITD.Domain.Model;
using MITD.PMS.Domain.Model.Periods;


namespace MITD.PMS.Domain.Model.Units
{
    public class UnitId : ObjectWithDbId<long>, IValueObject<UnitId>
    {
        #region Fields

        #endregion

        #region Properties

        private readonly PeriodId periodId;
        public virtual PeriodId PeriodId
        {
            get { return periodId; }

        }

        private readonly SharedUnitId sharedUnitId;
        public virtual SharedUnitId SharedUnitId
        {
            get { return sharedUnitId; }

        }
        #endregion


        #region Constructors
        // for Or mapper
        public UnitId()
        {

        }

        public UnitId(PeriodId periodId, SharedUnitId sharedUnitId)
        {
            this.periodId = periodId;
            this.sharedUnitId = sharedUnitId;
        }
        #endregion

        #region IValueObject Member

        public bool SameValueAs(UnitId other)
        {
            return new EqualsBuilder()
                .Append(this.PeriodId, other.PeriodId)
                .Append(this.SharedUnitId, other.SharedUnitId).IsEquals();
        }

        #endregion

        #region Object Override
        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            var other = (UnitId)obj;
            return SameValueAs(other);
        }
        public override int GetHashCode()
        {
            return new HashCodeBuilder().Append(PeriodId).Append(SharedUnitId).ToHashCode();
        }

        public override string ToString()
        {
            return "unitid:" + SharedUnitId + ";periodId:" + periodId;
        }


        public static bool operator ==(UnitId left, UnitId right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(UnitId left, UnitId right)
        {
            return !(left == right);
        }

        #endregion


    }
}
