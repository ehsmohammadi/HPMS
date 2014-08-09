using MITD.Domain.Model;
using System;

namespace MITD.PMS.Domain.Model.Periods
{
    [Serializable]
    public class PeriodId : IValueObject<PeriodId>
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
        protected PeriodId()
        {

        }

        public PeriodId(long id)
        {
            this.id = id;
        } 
        #endregion

        #region IValueObject Member
        public virtual bool SameValueAs(PeriodId other)
        {
            return Id.Equals(other.Id);
        } 
        #endregion

        #region Object Override 
        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            var other = (PeriodId)obj;
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

        public static bool operator ==(PeriodId left, PeriodId right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(PeriodId left, PeriodId right)
        {
            return !(left == right);
        }
        #endregion
    }
}
