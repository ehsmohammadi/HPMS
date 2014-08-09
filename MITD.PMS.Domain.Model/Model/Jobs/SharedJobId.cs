
using System;
using MITD.Domain.Model;

namespace MITD.PMS.Domain.Model.Jobs
{
    public class SharedJobId : IValueObject<SharedJobId>
    {

        #region Properties
        
        private readonly long id;
        public long Id { get { return id; } }
        
        #endregion

        #region Constructors
        // for Or mapper
        public SharedJobId()
        {

        }

        public SharedJobId(long id)
        {
            this.id = id;
        }
        #endregion

        #region IValueObject Member
        public bool SameValueAs(SharedJobId other)
        {
            return Id.Equals(other.Id);
        }
        #endregion

        #region Object Override
        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            var other = (SharedJobId)obj;
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

        public static bool operator ==(SharedJobId left, SharedJobId right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(SharedJobId left, SharedJobId right)
        {
            return !(left == right);
        }

        #endregion
    }
}
