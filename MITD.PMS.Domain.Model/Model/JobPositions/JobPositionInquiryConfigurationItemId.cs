using MITD.Core.Builders;
using MITD.Domain.Model;
using MITD.PMS.Domain.Model.Employees;

namespace MITD.PMS.Domain.Model.JobPositions
{
    public class JobPositionInquiryConfigurationItemId :ObjectWithDbId<long>, IValueObject<JobPositionInquiryConfigurationItemId>
    {
        #region Properties

        private readonly JobPositionId inquirerJobPositionId;
        public virtual JobPositionId InquirerJobPositionId
        {
            get { return inquirerJobPositionId; }
        }

        private readonly EmployeeId inquirerId;
        public virtual EmployeeId InquirerId
        {
            get { return inquirerId; }
        }

        private readonly JobPositionId inquirySubjectJobPositionId;
        public virtual JobPositionId InquirySubjectJobPositionId
        {
            get { return inquirySubjectJobPositionId; }
        }

        private readonly EmployeeId inquirySubjectId;
        public virtual EmployeeId InquirySubjectId
        {
            get { return inquirySubjectId; }
        }
        
        #endregion

        #region Constructors
        // for Or mapper
        protected JobPositionInquiryConfigurationItemId()
        {

        }

        public JobPositionInquiryConfigurationItemId(JobPositionId inquirerJobPositionId, EmployeeId inquirerId,
            JobPositionId inquirySubjectJobPositionId, EmployeeId inquirySubjectId)
        {
            this.inquirerId = inquirerId;
            this.inquirySubjectJobPositionId = inquirySubjectJobPositionId;
            this.inquirySubjectId = inquirySubjectId;
            this.inquirerJobPositionId = inquirerJobPositionId;
        }

        #endregion

        #region IValueObject Member
        public bool SameValueAs(JobPositionInquiryConfigurationItemId other)
        {
            return new EqualsBuilder().Append(this.InquirerId, other.InquirerId)
                .Append(this.InquirySubjectJobPositionId, other.InquirySubjectJobPositionId)
                .Append(this.InquirySubjectId, other.InquirySubjectId).IsEquals();
        }
        #endregion

        #region Object Override
        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            var other = (JobPositionInquiryConfigurationItemId)obj;
            return SameValueAs(other);
        }
        public override int GetHashCode()
        {
            return new HashCodeBuilder().Append(this.InquirerId)
                .Append(InquirySubjectJobPositionId)
                .Append(InquirySubjectId).GetHashCode();
        }

        public override string ToString()
        {
            return "InquirerId:" + InquirerId 
                + ";JobPositionId:" + InquirySubjectJobPositionId 
                + ";InquirySubjectId" + InquirySubjectId;
        }


        public static bool operator ==(JobPositionInquiryConfigurationItemId left, JobPositionInquiryConfigurationItemId right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(JobPositionInquiryConfigurationItemId left, JobPositionInquiryConfigurationItemId right)
        {
            return !(left == right);
        }


        #endregion
    }
}
