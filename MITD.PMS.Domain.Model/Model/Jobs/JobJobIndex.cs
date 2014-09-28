using MITD.Core.Builders;
using MITD.Domain.Model;
using MITD.PMS.Domain.Model.JobIndices;

namespace MITD.PMS.Domain.Model.Jobs
{
    public class JobJobIndex : IValueObject<JobJobIndex>,IJobJobIndex
    {

        #region Properties

        private readonly AbstractJobIndexId jobIndexId;
        public virtual AbstractJobIndexId JobIndexId { get { return jobIndexId; } }

        private bool showforTopLevel;
        public virtual bool ShowforTopLevel
        {
            get { return showforTopLevel; }
        }

        private bool showforSameLevel;
        public virtual bool ShowforSameLevel
        {
            get { return showforSameLevel; }
        }

        private bool showforLowLevel;
        public virtual bool ShowforLowLevel
        {
            get { return showforLowLevel; }
        }


        #endregion

        #region Constructors
        // for Or mapper
        protected JobJobIndex()
        {

        }

        public JobJobIndex(AbstractJobIndexId jobIndexId, bool showforTopLevel, bool showforSameLevel, bool showforLowLevel)
        {
            this.jobIndexId = jobIndexId;
            this.showforTopLevel = showforTopLevel;
            this.showforSameLevel = showforSameLevel;
            this.showforLowLevel = showforLowLevel;
        }
        #endregion

        #region IValueObject Member
        public bool SameValueAs(JobJobIndex other)
        {
            var builder = new EqualsBuilder();
            builder.Append(JobIndexId, other.JobIndexId);
            builder.Append(ShowforTopLevel, other.ShowforTopLevel);
            builder.Append(ShowforSameLevel, other.ShowforSameLevel);
            builder.Append(showforLowLevel, other.showforLowLevel);
            return builder.IsEquals();
        }
        #endregion

        #region Object Override
        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            var other = (JobJobIndex)obj;
            return SameValueAs(other);
        }
        public override int GetHashCode()
        {
            return
                new HashCodeBuilder().Append(JobIndexId)
                    .Append(ShowforTopLevel)
                    .Append(ShowforSameLevel)
                    .Append(ShowforLowLevel)
                    .ToHashCode();

        }

        public override string ToString()
        {
            return "JobIndexId:" + JobIndexId + ";ShowforTopLevel:" + ShowforTopLevel + ";ShowforSameLevel:" +
                   ShowforSameLevel + ";ShowforLowLevel:" + ShowforLowLevel;
        }

        public static bool operator ==(JobJobIndex left, JobJobIndex right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(JobJobIndex left, JobJobIndex right)
        {
            return !(left == right);
        }

        #endregion
    }



    #region Interface 
    public interface IJobJobIndex
    {
        AbstractJobIndexId JobIndexId { get; }
        bool ShowforTopLevel {get;}
        bool ShowforSameLevel { get; }
        bool ShowforLowLevel { get; }

    } 
    #endregion
}
