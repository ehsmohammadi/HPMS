
using MITD.Domain.Model;

namespace MITD.PMS.Domain.Model.JobIndices
{
    public class SharedJobIndexId : IValueObject<SharedJobIndexId>
    {

        #region Properties
        
        private readonly long id;
        public long Id { get { return id; } }
        
        #endregion

        #region Constructors
        // for Or mapper
        protected SharedJobIndexId()
        {

        }

        public SharedJobIndexId(long id)
        {
            this.id = id;
        }
        #endregion

        #region IValueObject Member
        public bool SameValueAs(SharedJobIndexId other)
        {
            return Id.Equals(other.Id);
        }
        #endregion

        #region Object Override
        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            var other = (SharedJobIndexId)obj;
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


        public static bool operator ==(SharedJobIndexId left, SharedJobIndexId right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(SharedJobIndexId left, SharedJobIndexId right)
        {
            return !(left == right);
        }

        #endregion
    }
}
