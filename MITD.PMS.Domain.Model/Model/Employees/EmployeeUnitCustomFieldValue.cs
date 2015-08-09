using System;
using System.Collections.Generic;
using System.Linq;
using MITD.Core.Builders;
using MITD.Domain.Model;
using MITD.PMS.Domain.Model.Units;


namespace MITD.PMS.Domain.Model.Employees
{
    public class EmployeeUnitCustomFieldValue :ObjectWithDbId<long> ,IValueObject<EmployeeUnitCustomFieldValue>
    {

        #region Properties

        private readonly UnitCustomFieldId unitCustomFieldId;
        public virtual UnitCustomFieldId UnitCustomFieldId
        {
            get { return unitCustomFieldId; }
        }

        private readonly string unitCustomFieldValue;
        public virtual string UnitCustomFieldValue
        {
            get { return unitCustomFieldValue; }
        }



        #endregion

        #region Constructors
        // for Or mapper
        protected EmployeeUnitCustomFieldValue()
        {

        }

        public EmployeeUnitCustomFieldValue(UnitCustomFieldId unitCustomFieldId, string unitCustomFieldValue)
        {
            this.unitCustomFieldId = unitCustomFieldId;
            this.unitCustomFieldValue = unitCustomFieldValue;
        }
        #endregion

        #region IValueObject Member
        public virtual bool SameValueAs(EmployeeUnitCustomFieldValue other)
        {
            var builder = new EqualsBuilder();

            builder.Append(this.UnitCustomFieldId, other.UnitCustomFieldId);
            builder.Append(this.UnitCustomFieldValue, other.UnitCustomFieldValue);


            return builder.IsEquals();


        }
        #endregion

        #region Object Override
        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            var other = (EmployeeUnitCustomFieldValue)obj;
            return SameValueAs(other);
        }
        public override int GetHashCode()
        {
            return new HashCodeBuilder().Append(UnitCustomFieldId).Append(UnitCustomFieldValue).ToHashCode();
        }

        public override string ToString()
        {
            return "UnitCustomFieldId:" + UnitCustomFieldId + ";UnitCustomFieldValue:" + UnitCustomFieldValue ;
        }
        #endregion
    }
}
