using System;
using MITD.Core.Builders;
using MITD.Domain.Model;
using MITD.PMS.Domain.Model.Periods;


namespace MITD.PMS.Domain.Model.Jobs
{
    public class JobId:ObjectWithDbId<long>, IValueObject<JobId>
    {
        #region Fields

        #endregion

        #region Properties

        private readonly PeriodId periodId;
        public virtual PeriodId PeriodId
        {
            get { return periodId; }
            
        }

        private readonly SharedJobId sharedJobId;
        public virtual SharedJobId SharedJobId
        {
            get { return sharedJobId; }

        }
        #endregion


         #region Constructors
        // for Or mapper
        public JobId()
        {

        }

        public JobId(PeriodId periodId,SharedJobId sharedJobId)
        {
            this.periodId = periodId;
            this.sharedJobId = sharedJobId;
        } 
        #endregion

        #region IValueObject Member
        public bool SameValueAs(JobId other)
        {
            return new EqualsBuilder()
                .Append(this.PeriodId, other.PeriodId)
                .Append(this.SharedJobId, other.SharedJobId).IsEquals();
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
            return new HashCodeBuilder().Append(PeriodId).Append(SharedJobId).ToHashCode();
        }

        public override string ToString()
        {
            return "jobid:"+SharedJobId+";periodId:"+ periodId;
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
