using MITD.Domain.Model;

namespace MITD.PMS.Domain.Model.JobIndices
{
    public class AbstractJobIndexId : IValueObject<AbstractJobIndexId>
    {
        #region Property and Back Field
        private readonly long id;
        public long Id { get { return id; } }
        #endregion

        #region Constructors
        protected AbstractJobIndexId()
        {

        }

        public AbstractJobIndexId(long id)
        {
            this.id = id;
        }
        #endregion

        #region IValueObject Member
        public bool SameValueAs(AbstractJobIndexId other)
        {
            return other != null && Id.Equals(other.Id);
        }
        #endregion

        #region Override Object
        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;
            if (obj == null || GetType() != obj.GetType())
                return false;
            return SameValueAs(obj as AbstractJobIndexId);
        }
        public override string ToString()
        {
            return Id.ToString();
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }


        public static bool operator ==(AbstractJobIndexId left, AbstractJobIndexId right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(AbstractJobIndexId left, AbstractJobIndexId right)
        {
            return !(left == right);
        }

        #endregion
    }
}
