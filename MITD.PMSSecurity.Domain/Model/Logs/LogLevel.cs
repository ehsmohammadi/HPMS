using System;
using MITD.Core;
#if SILVERLIGHT 

namespace MITD.PMS.Presentation.Contracts
{
    public class LogLevel : Enumeration
    {

#else
using MITD.Domain.Model;

namespace MITD.PMSSecurity.Domain.Logs
{
    public class LogLevel : Enumeration, IValueObject<LogLevel>
    {
#endif
        public static readonly LogLevel Information = new LogLevel("1", "Information");
        public static readonly LogLevel Warning = new LogLevel("2", "Warning");
        public static readonly LogLevel Error = new LogLevel("3", "Error");
        public static readonly LogLevel AccessControl = new LogLevel("4", "AccessControl");

        public LogLevel(string value, string displayName)
            : base(value, displayName)
        {
        }

        public bool SameValueAs(LogLevel other)
        {
            return Equals(other);
        }

        public static explicit operator int(LogLevel x)
        {
            if (x == null)
                throw new InvalidCastException();
            
            return Convert.ToInt32(x.Value);

        }

        public static implicit operator LogLevel(int val)
        {
            return Enumeration.FromValue<LogLevel>(val.ToString());
        }

    }
}
