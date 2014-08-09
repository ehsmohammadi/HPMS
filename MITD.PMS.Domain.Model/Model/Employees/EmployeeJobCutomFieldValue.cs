using System;
using System.Collections.Generic;
using System.Linq;
using MITD.Core.Builders;
using MITD.Domain.Model;
using MITD.PMS.Domain.Model.Jobs;
using MITD.PMS.Domain.Model.JobPositions;

namespace MITD.PMS.Domain.Model.Employees
{
    public class EmployeeJobCustomFieldValue :ObjectWithDbId<long> ,IValueObject<EmployeeJobCustomFieldValue>
    {

        #region Properties

        private readonly JobCustomFieldId jobCustomFieldId;
        public virtual JobCustomFieldId JobCustomFieldId
        {
            get { return jobCustomFieldId; }
        }

        private readonly string jobCustomFieldValue;
        public virtual string JobCustomFieldValue
        {
            get { return jobCustomFieldValue; }
        }



        #endregion

        #region Constructors
        // for Or mapper
        protected EmployeeJobCustomFieldValue()
        {

        }

        public EmployeeJobCustomFieldValue(JobCustomFieldId jobCustomFieldId, string jobCustomFieldValue)
        {
            this.jobCustomFieldId = jobCustomFieldId;
            this.jobCustomFieldValue = jobCustomFieldValue;
        }
        #endregion

        #region IValueObject Member
        public virtual bool SameValueAs(EmployeeJobCustomFieldValue other)
        {
            var builder = new EqualsBuilder();

            builder.Append(this.JobCustomFieldId, other.JobCustomFieldId);
            builder.Append(this.JobCustomFieldValue, other.JobCustomFieldValue);


            return builder.IsEquals();


        }
        #endregion

        #region Object Override
        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            var other = (EmployeeJobCustomFieldValue)obj;
            return SameValueAs(other);
        }
        public override int GetHashCode()
        {
            return new HashCodeBuilder().Append(JobCustomFieldId).Append(JobCustomFieldValue).ToHashCode();
        }

        public override string ToString()
        {
            return "JobCustomFieldId:" + JobCustomFieldId + ";JobCustomFieldValue:" + JobCustomFieldValue ;
        }
        #endregion
    }
}
