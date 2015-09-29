using System;
using MITD.Domain.Model;

namespace MITD.PMSSecurity.Domain.Model.AccessPermissions
{
    public class AccessPermissionId : ObjectWithDbId<long>,IValueObject<AccessPermissionId>
    { 

        #region Properties

        private readonly Guid guid;
        public Guid Guid { get { return guid; } } 
        #endregion

        #region Constructors

        public AccessPermissionId(Guid guid)
        {
            this.guid = guid;
        } 
        #endregion

        #region IValueObject Member
        public bool SameValueAs(AccessPermissionId other)
        {
            return Guid.Equals(other.Guid);
        } 
        #endregion

        #region Object Override 
        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            var other = (AccessPermissionId)obj;
            return SameValueAs(other);
        }
        public override int GetHashCode()
        {
            return Guid.GetHashCode();
        }

        public override string ToString()
        {
            return Guid.ToString();
        }

        public static bool operator ==(AccessPermissionId left, AccessPermissionId right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(AccessPermissionId left, AccessPermissionId right)
        {
            return !(left == right);
        }

        #endregion
    }
}
