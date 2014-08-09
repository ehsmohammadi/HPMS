using MITD.Domain.Model;

namespace MITD.PMSAdmin.Domain.Model.JobPositions
{
    public class JobPositionId : IValueObject<JobPositionId>
    {
 
        #region Properties
        private readonly long id;
        public long Id { get { return id; } }
        #endregion

        #region Constructors
        // for Or mapper
        public JobPositionId()
        {

        }

        public JobPositionId(long id)
        {
            this.id = id;
        }
        #endregion

        #region IValueObject Member
        public virtual bool SameValueAs(JobPositionId other)
        {
            return Id.Equals(other.Id);
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
            return Id.GetHashCode();
        }

        public override string ToString()
        {
            return Id.ToString();
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
