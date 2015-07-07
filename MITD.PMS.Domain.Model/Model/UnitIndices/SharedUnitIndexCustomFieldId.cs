using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.Domain.Model;

namespace MITD.PMS.Domain.Model.UnitIndices
{
    public class SharedUnitIndexCustomFieldId : IValueObject<SharedUnitIndexCustomFieldId>
    {

        #region Properties

        private readonly long id;
        public long Id { get { return id; } }

        #endregion

        #region Constructors
        // for Or mapper
        protected SharedUnitIndexCustomFieldId()
        {

        }

        public SharedUnitIndexCustomFieldId(long id)
        {
            this.id = id;
        }
        #endregion

        #region IValueObject Member
        public bool SameValueAs(SharedUnitIndexCustomFieldId other)
        {
            return Id.Equals(other.Id);
        }
        #endregion

        #region Object Override
        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            var other = (SharedUnitIndexCustomFieldId)obj;
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


        public static bool operator ==(SharedUnitIndexCustomFieldId left, SharedUnitIndexCustomFieldId right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(SharedUnitIndexCustomFieldId left, SharedUnitIndexCustomFieldId right)
        {
            return !(left == right);
        }


        #endregion
    }
}
