using MITD.Domain.Model;

namespace MITD.PMS.Domain.Model.Jobs
{
    public class SharedJobCustomFieldId : IValueObject<SharedJobCustomFieldId>
    {

        #region Properties

        private readonly long id;
        public long Id { get { return id; } }

        #endregion

        #region Constructors
        // for Or mapper
        protected SharedJobCustomFieldId()
        {

        }

        public SharedJobCustomFieldId(long id)
        {
            this.id = id;
        }
        #endregion

        #region IValueObject Member
        public bool SameValueAs(SharedJobCustomFieldId other)
        {
            return Id.Equals(other.Id);
        }
        #endregion

        #region Object Override
        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            var other = (SharedJobCustomFieldId)obj;
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

        public static bool operator ==(SharedJobCustomFieldId left, SharedJobCustomFieldId right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(SharedJobCustomFieldId left, SharedJobCustomFieldId right)
        {
            return !(left == right);
        }

        #endregion
    }
}
