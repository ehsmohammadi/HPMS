
using MITD.Domain.Model;

namespace MITD.PMS.Domain.Model.Units
{
    public class SharedUnitId : IValueObject<SharedUnitId>
    {

        #region Properties
     
        private readonly long id;
        public long Id { get { return id; } }
        
        #endregion

        #region Constructors
        // for Or mapper
        public SharedUnitId()
        {

        }

        public SharedUnitId(long id)
        {
            this.id = id;
        }
        #endregion

        #region IValueObject Member
        public bool SameValueAs(SharedUnitId other)
        {
            return Id.Equals(other.Id);
        }
        #endregion

        #region Object Override
        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            var other = (SharedUnitId)obj;
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


        public static bool operator ==(SharedUnitId left, SharedUnitId right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(SharedUnitId left, SharedUnitId right)
        {
            return !(left == right);
        }

        #endregion
    }
}
