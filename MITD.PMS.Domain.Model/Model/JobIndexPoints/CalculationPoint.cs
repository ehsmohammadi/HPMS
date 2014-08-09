using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.Domain.Model;
using MITD.PMS.Domain.Model.Calculations;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Domain.Model.Periods;

namespace MITD.PMS.Domain.Model.JobIndexPoints
{
    [Serializable]
    public abstract class CalculationPoint : IEntity<CalculationPoint>
    {
        protected readonly CalculationPointId id;
        protected readonly byte[] rowVersion;
        protected decimal value;
        protected readonly PeriodId periodId;
        protected readonly Calculations.CalculationId calculationId;
        private readonly bool isFinal;
        private readonly string name;

        protected CalculationPoint()
        {
        }
        protected CalculationPoint(CalculationPointId id,Period period, Calculation calculation, 
            string name, decimal value, bool isFinal=false):base()
        {
            this.value = value;
            if (id == null)
                throw new ArgumentNullException("calculationPointId");
            this.id = id;

            if (period == null || period.Id == null)
                throw new ArgumentNullException("period");
            this.periodId = period.Id;

            if (calculation == null || calculation.Id == null)
                throw new ArgumentNullException("employee");
            this.calculationId = calculation.Id;
            
            this.isFinal = isFinal;
            this.name = name;
        }
        public virtual CalculationPointId Id { get { return id; } }
        public virtual PeriodId PeriodId { get { return periodId; } }
        public virtual CalculationId CalculationId { get { return calculationId; } }
        public virtual decimal Value { get { return value; } }
        public virtual string Name { get { return name; } }
        public virtual bool IsFinal { get { return isFinal; } }


        public virtual void SetValue(decimal valueParam)
        {
            value = valueParam;
        }

        #region IEntity Member
        public virtual bool SameIdentityAs(CalculationPoint other)
        {
            return (other != null) && id.Equals(other.Id);
        }
        #endregion

        #region Object override
        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            var other = (CalculationPoint)obj;
            return SameIdentityAs(other);
        }
        public override int GetHashCode()
        {
            return id.GetHashCode();
        }
        public override string ToString()
        {
            return id.ToString();
        }
        #endregion
    }
}
