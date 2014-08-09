using System;
using System.Collections.Generic;
using System.Linq;
using MITD.Core.Builders;
using MITD.Domain.Model;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Domain.Model.Jobs;

namespace MITD.PMS.Domain.Model.JobPositions
{
    public class JobPositionEmployee : IValueObject<JobPositionEmployee>
    {
        private readonly long dbId;
        private readonly byte[] rowVersion;

        #region Properties

        private EmployeeId employeeId;
        public virtual EmployeeId EmployeeId
        {
            get { return employeeId; }
        }

        private readonly JobPosition jobPosition;
        public virtual JobPosition JobPosition
        {
            get { return jobPosition; }
        }

        private DateTime fromDate;
        public virtual DateTime FromDate
        {
            get { return fromDate; }
        }

        private DateTime toDate;
        public virtual DateTime ToDate
        {
            get { return toDate; }
        }

        private readonly IDictionary<JobCustomFieldId, string> employeeJobCustomFieldValues;
        public virtual IReadOnlyDictionary<JobCustomFieldId, string> EmployeeJobCustomFieldValues
        {
            get { return employeeJobCustomFieldValues.ToDictionary(e => e.Key, e => e.Value); }
        }

        #endregion

        #region Constructors
        // for Or mapper
        protected JobPositionEmployee()
        {

        }

        public JobPositionEmployee(Employee employee,JobPosition jobPosition,DateTime fromDate,DateTime toDate,IDictionary<JobCustomField,string> employeeJobCustomFieldValues )
        {
            if (jobPosition == null)
                throw new ArgumentNullException("jobPosition");
            if(jobPosition.Id==null)
                throw new ArgumentException("jobPosition.Id");
            this.jobPosition = jobPosition;
            this.fromDate = fromDate;
            this.toDate = toDate.Date;
            
            if (employee == null || employee.Id== null)
                throw new ArgumentNullException("employee");
            this.employeeId = employee.Id;
            this.employeeJobCustomFieldValues = employeeJobCustomFieldValues.ToDictionary(e=>e.Key.Id,e=>e.Value);
        }
        #endregion

        #region IValueObject Member
        public virtual bool SameValueAs(JobPositionEmployee other)
        {
            var builder = new EqualsBuilder();
            builder.Append(this.EmployeeId, other.EmployeeId);
            builder.Append(this.JobPosition, other.JobPosition);
            builder.Append(this.FromDate.Date, other.FromDate.Date);
            builder.Append(this.ToDate.Date, other.ToDate.Date);

            return builder.IsEquals();


        }
        #endregion

        #region Object Override
        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            var other = (JobPositionEmployee)obj;
            return SameValueAs(other);
        }
        public override int GetHashCode()
        {
            return new HashCodeBuilder().Append(JobPosition).Append(FromDate.Date).Append(ToDate.Date).ToHashCode();
        }

        public override string ToString()
        {
            return "JobPositionId:" + JobPosition + ";FromDate:" + FromDate.Date + ";ToDate:" + ToDate.Date;
        }
        #endregion
    }
}
