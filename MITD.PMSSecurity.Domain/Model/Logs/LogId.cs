using System;
using MITD.Domain.Model;

namespace MITD.PMSSecurity.Domain.Logs
{
    public class LogId : ObjectWithDbId<long>,IValueObject<LogId>
    { 

        #region Properties

        private readonly Guid guid;
        public Guid Guid { get { return guid; } } 
        #endregion

        #region Constructors
        // for Or mapper
        public LogId()
        {

        }

        public LogId(Guid guid)
        {
            this.guid = guid;
        } 
        #endregion

        #region IValueObject Member
        public bool SameValueAs(LogId other)
        {
            return Guid.Equals(other.Guid);
        } 
        #endregion

        #region Object Override 
        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            var other = (LogId)obj;
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

        public static bool operator ==(LogId left, LogId right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(LogId left, LogId right)
        {
            return !(left == right);
        }

        #endregion
    }
}
