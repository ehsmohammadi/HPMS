using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.Core;
using MITD.Domain.Model;

namespace MITD.PMS.Domain.Model.JobPositions
{
    public class JobPositionLevel : Enumeration, IValueObject<JobPositionLevel>
    {
        public static readonly JobPositionLevel Parents = new JobPositionLevel("1", "Parents");
        public static readonly JobPositionLevel Siblings = new JobPositionLevel("2", "Siblings");
        public static readonly JobPositionLevel Childs = new JobPositionLevel("3", "Childs");
        public static readonly JobPositionLevel None = new JobPositionLevel("4", "None");

        protected JobPositionLevel(string value, string name)
            : base(value, name)
        {
        }

        public bool SameValueAs(JobPositionLevel other)
        {
            return Equals(other);
        }
        public static bool operator ==(JobPositionLevel left, JobPositionLevel right)
        {
            return object.Equals(left, right);
        }
        public static bool operator !=(JobPositionLevel left, JobPositionLevel right)
        {
            return !(left == right);
        }

        public static explicit operator int(JobPositionLevel x)
        {
            if (x == null)
            {
                //throw new InvalidCastException();
                return -1;

            }

            return Convert.ToInt32(x.Value);

        }

        public static implicit operator JobPositionLevel(int val)
        {
            return Enumeration.FromValue<JobPositionLevel>(val.ToString());
        }



       
    }
}
