using MITD.Core.Builders;
using MITD.Domain.Model;

namespace MITD.PMSSecurity.Domain
{
    public class PMSAction : IEntity<PMSAction>
    {

        public PMSAction()          
        {
        }


        #region IEntity Member

        public virtual bool SameIdentityAs(PMSAction other)
        {
            return (other != null) && Id.Equals(other.Id);
        }


        #endregion

        #region Object override

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            var other = (PMSAction)obj;
            return SameIdentityAs(other);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override string ToString()
        {
            return Id.ToString();
        }

        #endregion

      
    }
}