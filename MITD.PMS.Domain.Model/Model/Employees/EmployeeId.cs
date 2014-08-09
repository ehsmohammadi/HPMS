using MITD.Core.Builders;
using MITD.Domain.Model;
using MITD.PMS.Domain.Model.Periods;
using System;

namespace MITD.PMS.Domain.Model.Employees
{
    [Serializable]
    public class EmployeeId:ObjectWithDbId<long>,IValueObject<EmployeeId>
    {
        private readonly PeriodId periodId;
        private readonly string employeeNo;
        
        #region Properties
        public PeriodId PeriodId
        {
            get { return periodId; }
        }
        public string EmployeeNo
        {
            get { return employeeNo; }
        }
        #endregion

        #region Constructors
        // for Or mapper
        protected EmployeeId()
        {

        }

        public EmployeeId(string employeeNo, PeriodId periodId)
        {
            this.employeeNo = employeeNo;
            this.periodId = periodId;
        } 
        #endregion

        #region IValueObject Member
        public bool SameValueAs(EmployeeId other)
        {
            return new EqualsBuilder()
                .Append(this.PeriodId, other.PeriodId)
                .Append(this.EmployeeNo, other.EmployeeNo).IsEquals();
        } 
        #endregion

        #region Object Override 
        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            var other = (EmployeeId)obj;
            return SameValueAs(other);
        }
        public override int GetHashCode()
        {
            return new HashCodeBuilder().Append(PeriodId).Append(EmployeeNo).ToHashCode();
        }

        public override string ToString()
        {
            return "PeriodId:" + PeriodId + ";EmployeeNo:" + EmployeeNo;
        }

        public static bool operator ==(EmployeeId left, EmployeeId right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(EmployeeId left, EmployeeId right)
        {
            return !(left == right);
        }

        #endregion
    }
}
