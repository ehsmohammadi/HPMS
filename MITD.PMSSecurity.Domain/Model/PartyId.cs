using System;
using MITD.Domain.Model;

namespace MITD.PMSSecurity.Domain
{
    public class PartyId : ObjectWithDbId<long>,IValueObject<PartyId>
    { 

        #region Properties

        private readonly string partyName;
        public string PartyName { get { return partyName; } } 
        #endregion

        #region Constructors
        // for Or mapper
        protected PartyId()
        {

        }

        public PartyId(string partyName)
        {
            if (string.IsNullOrWhiteSpace(partyName))
                throw new ArgumentNullException("partyName is null or whiteSpace");
            this.partyName = partyName;
        } 
        #endregion

        #region IValueObject Member
        public bool SameValueAs(PartyId other)
        {
            return PartyName.ToLower().Equals(other.partyName.ToLower());
        } 
        #endregion

        #region Object Override 
        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            var other = (PartyId)obj;
            return SameValueAs(other);
        }
        public override int GetHashCode()
        {
            return PartyName.GetHashCode();
        }

        public override string ToString()
        {
            return PartyName.ToString();
        }

        public static bool operator ==(PartyId left, PartyId right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(PartyId left, PartyId right)
        {
            return !(left == right);
        }

        #endregion
    }
}
