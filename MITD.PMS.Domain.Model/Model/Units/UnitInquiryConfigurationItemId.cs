using MITD.Core.Builders;
using MITD.Domain.Model;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Domain.Model.UnitIndices;

namespace MITD.PMS.Domain.Model.Units
{
    public class UnitInquiryConfigurationItemId :ObjectWithDbId<long>, IValueObject<UnitInquiryConfigurationItemId>
    {
        #region Properties

        private readonly AbstractUnitIndexId unitIndexIdUintPeriod;
        public virtual AbstractUnitIndexId UnitIndexIdUintPeriod
        {
            get { return unitIndexIdUintPeriod; }
        }
        
        private readonly UnitId inquirerUnitId;
        public virtual UnitId InquirerUnitId
        {
            get { return inquirerUnitId; }
        }

        private readonly EmployeeId inquirerId;
        public virtual EmployeeId InquirerId
        {
            get { return inquirerId; }
        }

        private readonly UnitId inquirySubjectUnitId;
        public virtual UnitId InquirySubjectUnitId
        {
            get { return inquirySubjectUnitId; }
        }

        private readonly EmployeeId inquirySubjectId;
        public virtual EmployeeId InquirySubjectId
        {
            get { return inquirySubjectId; }
        }
        
        #endregion

        #region Constructors
        // for Or mapper
        protected UnitInquiryConfigurationItemId()
        {

        }

        public UnitInquiryConfigurationItemId(UnitId inquirerUnitId, EmployeeId inquirerId,
            UnitId inquirySubjectUnitId,AbstractUnitIndexId abstractUnitIndexId)//, EmployeeId inquirySubjectId)
        {
            this.inquirerId = inquirerId;
            this.inquirySubjectUnitId = inquirySubjectUnitId;
           // this.inquirySubjectId = inquirySubjectId;
            this.inquirerUnitId = inquirerUnitId;
            this.unitIndexIdUintPeriod = abstractUnitIndexId;
        }

        #endregion

        #region IValueObject Member
        public bool SameValueAs(UnitInquiryConfigurationItemId other)
        {
            return new EqualsBuilder().Append(this.InquirerId, other.InquirerId)
                .Append(this.InquirySubjectUnitId, other.InquirySubjectUnitId)
                .Append(this.InquirySubjectId, other.InquirySubjectId).IsEquals();
        }
        #endregion

        #region Object Override
        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            var other = (UnitInquiryConfigurationItemId)obj;
            return SameValueAs(other);
        }
        public override int GetHashCode()
        {
            return new HashCodeBuilder().Append(this.InquirerId)
                .Append(InquirySubjectUnitId)
                .Append(InquirySubjectId).GetHashCode();
        }

        public override string ToString()
        {
            return "InquirerId:" + InquirerId
                + ";UnitId:" + InquirySubjectUnitId 
                + ";InquirySubjectId" + InquirySubjectId;
        }


        public static bool operator ==(UnitInquiryConfigurationItemId left, UnitInquiryConfigurationItemId right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(UnitInquiryConfigurationItemId left, UnitInquiryConfigurationItemId right)
        {
            return !(left == right);
        }


        #endregion
    }
}
