using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.Domain.Model;

namespace MITD.PMSSecurity.Domain.Model.AccessPermissions
{
    public class AccessPermission : IEntity<AccessPermission>
    {

        #region Properties
        private readonly AccessPermissionId id;
        public virtual AccessPermissionId Id { get { return id; } }


        #endregion
       


        #region IEntity Member

        public virtual bool SameIdentityAs(AccessPermission other)
        {
            return (other != null) && Id.Equals(other.Id);
        }



        #endregion

        #region Object override

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            var other = (AccessPermission)obj;
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
