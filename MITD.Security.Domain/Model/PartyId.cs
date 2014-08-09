using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.Domain.Model;

namespace MITD.Security.Domain.Model
{
    public class PartyId : IValueObject<PartyId>
    {
        #region Properties

        private readonly long id;

        public long Id
        {
            get { return id; }
        }

        #endregion

        #region Constructors

        // for Or mapper
        public PartyId()
        {

        }

        public PartyId(long id)
        {
            this.id = id;
        }

        #endregion

        #region IValueObject Member

        public bool SameValueAs(PartyId other)
        {
            return Id.Equals(other.Id);
        }

        #endregion

        #region Object Override

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            var other = (PartyId) obj;
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

        #endregion
    }
}
