
using MITD.Domain.Model;

namespace MITD.PMS.Domain.Model.UnitIndices
{
    public class SharedUnitIndexId : IValueObject<SharedUnitIndexId>
    {

        #region Properties
        
        private readonly long id;
        public long Id { get { return id; } }
        
        #endregion

        #region Constructors
        // for Or mapper
        protected SharedUnitIndexId()
        {

        }

        public SharedUnitIndexId(long id)
        {
            this.id = id;
        }
        #endregion

        #region IValueObject Member
        public bool SameValueAs(SharedUnitIndexId other)
        {
            return Id.Equals(other.Id);
        }
        #endregion

        #region Object Override
        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            var other = (SharedUnitIndexId)obj;
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


        public static bool operator ==(SharedUnitIndexId left, SharedUnitIndexId right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(SharedUnitIndexId left, SharedUnitIndexId right)
        {
            return !(left == right);
        }

        #endregion
    }
}
