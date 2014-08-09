using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.Domain.Model;

namespace MITD.PMSAdmin.Domain.Model.Jobs
{
    public class JobId : IValueObject<JobId>
    { 

        #region Properties
        private readonly long id;
        public long Id { get { return id; } }
        #endregion

        #region Constructors
        // for Or mapper
        public JobId()
        {

        }

        public JobId(long id)
        {
            this.id = id;
        } 
        #endregion

        #region IValueObject Member
        public bool SameValueAs(JobId other)
        {
            return Id.Equals(other.Id);
        } 
        #endregion

        #region Object Override 
        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            var other = (JobId)obj;
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

        public static bool operator ==(JobId left, JobId right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(JobId left, JobId right)
        {
            return !(left == right);
        }

        #endregion
    }
}
