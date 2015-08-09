using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.Core.Builders;
using MITD.Domain.Model;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Domain.Model.Units;
using MITD.PMS.Domain.Model.Jobs;


namespace MITD.PMS.Domain.Model.Units
{
    public class UnitEmployee : IValueObject<UnitEmployee>
   {
        private readonly long dbId;
        private readonly byte[] rowVersion;

        #region Properties

        private EmployeeId employeeId;
        public virtual EmployeeId EmployeeId
        {
            get { return employeeId; }
        }

        private readonly Unit _unit;
        public virtual Unit Unit
        {
            get { return _unit; }
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

        private readonly IDictionary<UnitCustomFieldId, string> employeeUnitCustomFieldValues;
        public virtual IReadOnlyDictionary<UnitCustomFieldId, string> EmployeeUnitCustomFieldValues
        {
            get { return employeeUnitCustomFieldValues.ToDictionary(e => e.Key, e => e.Value); }
        }

        #endregion

        #region Constructors
        // for Or mapper
        protected UnitEmployee()
        {

        }

        public UnitEmployee(Employee employee, Unit unit, DateTime fromDate, DateTime toDate, IDictionary<UnitCustomField, string> employeeUnitCustomFieldValues)
        {
            if (unit == null)
                throw new ArgumentNullException("Unit");
            if (unit.Id == null)
                throw new ArgumentException("Unit.Id");
            this._unit = unit;
            this.fromDate = fromDate;
            this.toDate = toDate.Date;
            
            if (employee == null || employee.Id== null)
                throw new ArgumentNullException("employee");
            this.employeeId = employee.Id;
            this.employeeUnitCustomFieldValues = employeeUnitCustomFieldValues.ToDictionary(e => e.Key.Id, e => e.Value);
        }
        #endregion

        #region IValueObject Member
        public virtual bool SameValueAs(UnitEmployee other)
        {
            var builder = new EqualsBuilder();
            builder.Append(this.EmployeeId, other.EmployeeId);
            builder.Append(this.Unit, other.Unit);
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
            var other = (UnitEmployee)obj;
            return SameValueAs(other);
        }
        public override int GetHashCode()
        {
            return new HashCodeBuilder().Append(Unit).Append(FromDate.Date).Append(ToDate.Date).ToHashCode();
        }

        public override string ToString()
        {
            return "UnitId:" + Unit + ";FromDate:" + FromDate.Date + ";ToDate:" + ToDate.Date;
        }
        #endregion
   }
}
