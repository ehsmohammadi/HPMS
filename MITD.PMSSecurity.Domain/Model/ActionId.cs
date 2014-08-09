using MITD.Domain.Model;

namespace MITD.PMSSecurity.Domain
{
    public class ActionId : IValueObject<ActionId>
    {

        private readonly int id;
        public int Id { get { return id; } }

        public ActionId(int id)
        {
            this.id = id;
        }

        #region IValueObject Member
        public bool SameValueAs(ActionId other)
        {
            return Id.Equals(other.Id);
        }
        #endregion

        #region Object Override
        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            var other = (ActionId)obj;
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

        public static bool operator ==(ActionId left, ActionId right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ActionId left, ActionId right)
        {
            return !(left == right);
        }

        #endregion
    }
}