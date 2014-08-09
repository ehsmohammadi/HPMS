using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.Domain.Model;

namespace MITD.PMSAdmin.Domain.Model.Units
{
    public class UnitId : IValueObject<UnitId>
    {
         
        #region Properties
        private readonly long id;
        public long Id { get { return id; } }
        #endregion

        #region Constructors
        // for Or mapper
        public UnitId()
        {

        }

        public UnitId(long id)
        {
            this.id = id;
        } 
        #endregion

        #region IValueObject Member
        public virtual bool SameValueAs(UnitId other)
        {
            return Id.Equals(other.Id);
        } 
        #endregion

        #region Object Override 
        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            var other = (UnitId)obj;
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

        public static bool operator ==(UnitId left, UnitId right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(UnitId left, UnitId right)
        {
            return !(left == right);
        }

        #endregion
    }
}
