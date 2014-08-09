using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.Core.Builders;
using MITD.Domain.Model;
using MITD.PMS.Domain.Model.Periods;

namespace MITD.PMS.Domain.Model.Jobs
{
    public class JobCustomFieldId : ObjectWithDbId<long>, IValueObject<JobCustomFieldId>
    {

        #region Properties

        private SharedJobCustomFieldId sharedJobCustomFieldId;
        public virtual SharedJobCustomFieldId SharedJobCustomFieldId
        {
            get { return sharedJobCustomFieldId; } 
        }


        private SharedJobId sharedJobId;
        public virtual SharedJobId SharedJobId
        {
            get { return sharedJobId; }
        }

        private PeriodId periodId;
        public virtual PeriodId PeriodId
        {
            get { return periodId; }
        }

        #endregion

        #region Constructors
        // for Or mapper
        protected JobCustomFieldId()
        {

        }

        public JobCustomFieldId(PeriodId periodId, SharedJobCustomFieldId sharedJobCustomFieldId, SharedJobId sharedJobId)
        {
            this.sharedJobCustomFieldId = sharedJobCustomFieldId;
            this.sharedJobId = sharedJobId;
            this.periodId = periodId;
        }
        #endregion

        #region IValueObject Member
        public bool SameValueAs(JobCustomFieldId other)
        {
            return new EqualsBuilder()
                .Append(this.PeriodId, other.PeriodId)
                .Append(this.SharedJobId, other.SharedJobId)
                .Append(this.SharedJobCustomFieldId, other.SharedJobCustomFieldId).IsEquals();
            
        }
        #endregion

        #region Object Override
        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            var other = (JobCustomFieldId)obj;
            return SameValueAs(other);
        }
        public override int GetHashCode()
        {
            return new HashCodeBuilder().Append(PeriodId).Append(SharedJobId).Append(SharedJobCustomFieldId).ToHashCode();
        }

        public override string ToString()
        {
            return "jobid:" + SharedJobId + ";periodId:" + periodId + ";SharedJobCustomFieldId:" + SharedJobCustomFieldId;
        }

        public static bool operator ==(JobCustomFieldId left, JobCustomFieldId right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(JobCustomFieldId left, JobCustomFieldId right)
        {
            return !(left == right);
        }
        #endregion
    }
}
