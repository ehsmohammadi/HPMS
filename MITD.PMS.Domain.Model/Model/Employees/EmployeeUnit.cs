using System;
using System.Collections.Generic;
using System.Linq;
using MITD.Core.Builders;
using MITD.Domain.Model;
using MITD.PMS.Domain.Model.Jobs;
using MITD.PMS.Domain.Model.Units;

namespace MITD.PMS.Domain.Model.Employees
{
    public class EmployeeUnit : IValueObject<EmployeeUnit>
    {
        private readonly long dbId;
       
        private readonly string employeeNo;

        #region Properties

        private readonly Employee employee;
        public virtual Employee Employee
        {
            get { return employee; }
        }


        private readonly UnitId unitId;
        public virtual UnitId UnitId
        {
            get { return unitId; }
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

        private int unitWeight;
        public virtual int UnitWeight
        {
            get { return unitWeight; }
        }

        private int workTimePercent;
        public virtual int WorkTimePercent
        {
            get { return workTimePercent; }
        }

        private readonly IList<EmployeeUnitCustomFieldValue> employeeUnitCustomFieldValues = new List<EmployeeUnitCustomFieldValue>();


        public virtual IReadOnlyList<EmployeeUnitCustomFieldValue> EmployeeUnitCustomFieldValues
        {
            get { return employeeUnitCustomFieldValues.ToList().AsReadOnly(); }
        }

        #endregion

        #region Constructors
        // for Or mapper
        protected EmployeeUnit()
        {

        }

        public EmployeeUnit(Employee employee, Unit unit, DateTime fromDate, DateTime toDate, int workTimePercent, int unitWeight, List<EmployeeUnitCustomFieldValue> employeeUnitCustomFieldValue)
        {
            if (unit == null)
                throw new ArgumentNullException("unit");
            if (unit.Id == null)
                throw new ArgumentNullException("unit.Id");
            if (employee == null)
                throw new ArgumentNullException("employee");

            unitId = unit.Id;
            this.workTimePercent = workTimePercent;
            this.unitWeight = unitWeight;
            this.fromDate = fromDate;
            this.toDate = toDate.Date;
            this.employee = employee;
            employeeNo = employee.Id.EmployeeNo;
            if (employeeUnitCustomFieldValue != null)
                this.employeeUnitCustomFieldValues = employeeUnitCustomFieldValue;
        }
        #endregion

        #region IValueObject Member
        public virtual bool SameValueAs(EmployeeUnit other)
        {
            var builder = new EqualsBuilder();

            builder.Append(this.UnitId, other.UnitId);
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
            var other = (EmployeeUnit)obj;
            return SameValueAs(other);
        }
        public override int GetHashCode()
        {
            return new HashCodeBuilder().Append(UnitId).Append(FromDate.Date).Append(ToDate.Date).ToHashCode();
        }

        public override string ToString()
        {
            return "UnitId:" + UnitId + ";FromDate:" + FromDate.Date + ";ToDate:" + ToDate.Date;
        }
        #endregion
    }
}
