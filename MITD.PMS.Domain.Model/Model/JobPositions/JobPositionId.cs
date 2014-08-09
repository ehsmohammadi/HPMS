using MITD.Core.Builders;
using MITD.Domain.Model;
using MITD.PMS.Domain.Model.Periods;


namespace MITD.PMS.Domain.Model.JobPositions
{
    public class JobPositionId : ObjectWithDbId<long>, IValueObject<JobPositionId>
    {
        #region Fields
        #endregion

        #region Properties

        private readonly PeriodId periodId;
        public virtual PeriodId PeriodId
        {
            get { return periodId; }

        }

        private readonly SharedJobPositionId sharedJobPositionId;
        public virtual SharedJobPositionId SharedJobPositionId
        {
            get { return sharedJobPositionId; }

        }
        #endregion

        #region Constructors
        // for Or mapper
        public JobPositionId()
        {

        }

        public JobPositionId(PeriodId periodId, SharedJobPositionId sharedJobPositionId)
        {
            this.periodId = periodId;
            this.sharedJobPositionId = sharedJobPositionId;
        }
        #endregion

        #region IValueObject Member

        public bool SameValueAs(JobPositionId other)
        {
            return new EqualsBuilder()
                .Append(this.PeriodId, other.PeriodId)
                .Append(this.SharedJobPositionId, other.SharedJobPositionId).IsEquals();
        }

        #endregion

        #region Object Override
        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            var other = (JobPositionId)obj;
            return SameValueAs(other);
        }
        public override int GetHashCode()
        {
            return new HashCodeBuilder().Append(PeriodId).Append(SharedJobPositionId).ToHashCode();
        }

        public override string ToString()
        {
            return "unitid:" + SharedJobPositionId + ";periodId:" + periodId;
        }


        public static bool operator ==(JobPositionId left, JobPositionId right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(JobPositionId left, JobPositionId right)
        {
            return !(left == right);
        }


        #endregion
    }
}
