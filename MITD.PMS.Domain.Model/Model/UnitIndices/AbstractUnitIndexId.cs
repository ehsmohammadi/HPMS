using MITD.Domain.Model;

namespace MITD.PMS.Domain.Model.UnitIndices
{
    public class AbstractUnitIndexId : IValueObject<AbstractUnitIndexId>
    {
        #region Property and Back Field
        private readonly long id;
        public long Id { get { return id; } }
        #endregion

        #region Constructors
        protected AbstractUnitIndexId()
        {

        }

        public AbstractUnitIndexId(long id)
        {
            this.id = id;
        }
        #endregion

        #region IValueObject Member
        public bool SameValueAs(AbstractUnitIndexId other)
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
            return SameValueAs(obj as AbstractUnitIndexId);
        }
        public override string ToString()
        {
            return Id.ToString();
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }


        public static bool operator ==(AbstractUnitIndexId left, AbstractUnitIndexId right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(AbstractUnitIndexId left, AbstractUnitIndexId right)
        {
            return !(left == right);
        }

        #endregion
    }
}
