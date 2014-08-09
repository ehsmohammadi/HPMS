using MITD.Domain.Model;

namespace MITD.PMS.Domain.Model.Policies
{
    public class PolicyId:IValueObject<PolicyId>
    {
        private readonly long id;
        
        #region Properties
        public long Id
        {
            get { return id; }
        } 
        #endregion

        #region Constructors
        // for Or mapper
        protected PolicyId()
        {

        }

        public PolicyId(long id)
        {
            this.id = id;
        } 
        #endregion

        #region IValueObject Member
        public bool SameValueAs(PolicyId other)
        {
            return Id.Equals(other.Id);
        } 
        #endregion

        #region Object Override 
        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            var other = (PolicyId)obj;
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

        public static bool operator ==(PolicyId left, PolicyId right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(PolicyId left, PolicyId right)
        {
            return !(left == right);
        }

        #endregion
    }
}
