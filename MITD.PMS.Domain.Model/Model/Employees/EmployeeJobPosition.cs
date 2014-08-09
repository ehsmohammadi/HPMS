using System;
using System.Collections.Generic;
using System.Linq;
using MITD.Core.Builders;
using MITD.Domain.Model;
using MITD.PMS.Domain.Model.Jobs;
using MITD.PMS.Domain.Model.JobPositions;

namespace MITD.PMS.Domain.Model.Employees
{
    public class EmployeeJobPosition : IValueObject<EmployeeJobPosition>
    {
        private readonly long dbId;
       
        private readonly string employeeNo;

        #region Properties

        private readonly Employee employee;
        public virtual Employee Employee
        {
            get { return employee; }
        }


        private readonly JobPositionId jobPositionId;
        public virtual JobPositionId JobPositionId
        {
            get { return jobPositionId; }
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

        private int jobPositionWeight;
        public virtual int JobPositionWeight
        {
            get { return jobPositionWeight; }
        }

        private int workTimePercent;
        public virtual int WorkTimePercent
        {
            get { return workTimePercent; }
        }

        private readonly IList<EmployeeJobCustomFieldValue> employeeJobCustomFieldValues = new List<EmployeeJobCustomFieldValue>();
 

        public virtual IReadOnlyList<EmployeeJobCustomFieldValue> EmployeeJobCustomFieldValues
        {
            get { return employeeJobCustomFieldValues.ToList().AsReadOnly(); }
        }

        #endregion

        #region Constructors
        // for Or mapper
        protected EmployeeJobPosition()
        {

        }

        public EmployeeJobPosition(Employee employee, JobPosition jobPosition, DateTime fromDate, DateTime toDate,int workTimePercent,int jobPositionWeight, List<EmployeeJobCustomFieldValue> employeeJobCustomFieldValues)
        {
            if (jobPosition == null)
                throw new ArgumentNullException("jobPosition");
            if (jobPosition.Id == null)
                throw new ArgumentNullException("jobPosition.Id");
            if (employee == null)
                throw new ArgumentNullException("employee");

            jobPositionId = jobPosition.Id;
            this.workTimePercent = workTimePercent;
            this.jobPositionWeight = jobPositionWeight;
            this.fromDate = fromDate;
            this.toDate = toDate.Date;
            this.employee = employee;
            employeeNo = employee.Id.EmployeeNo;
            if (employeeJobCustomFieldValues != null)
                this.employeeJobCustomFieldValues = employeeJobCustomFieldValues;
        }
        #endregion

        #region IValueObject Member
        public virtual bool SameValueAs(EmployeeJobPosition other)
        {
            var builder = new EqualsBuilder();

            builder.Append(this.JobPositionId, other.JobPositionId);
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
            var other = (EmployeeJobPosition)obj;
            return SameValueAs(other);
        }
        public override int GetHashCode()
        {
            return new HashCodeBuilder().Append(JobPositionId).Append(FromDate.Date).Append(ToDate.Date).ToHashCode();
        }

        public override string ToString()
        {
            return "JobPositionId:" + JobPositionId + ";FromDate:" + FromDate.Date + ";ToDate:" + ToDate.Date;
        }
        #endregion
    }
}
