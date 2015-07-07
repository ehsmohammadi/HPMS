using System;
using MITD.Domain.Model;
using MITD.PMS.Domain.Model.Periods;

namespace MITD.PMS.Domain.Model.UnitIndices
{
    public class AbstractUnitIndex : IEntity<AbstractUnitIndex>
    {

        #region Fields

        protected readonly byte[] rowVersion;

        #endregion

        #region Properties

        protected  AbstractUnitIndexId id;
        public virtual AbstractUnitIndexId Id
        {
            get { return id; }
        }


        protected  PeriodId periodId;
        public virtual PeriodId PeriodId
        {
            get { return periodId; }

        }




        #endregion

        #region Constructors
        public AbstractUnitIndex()
        {
            //For OR mapper
        }

        public AbstractUnitIndex(AbstractUnitIndexId abstractUnitIndexId, PeriodId periodId)
        {
            if (abstractUnitIndexId == null)
                throw new ArgumentNullException("abstractUnitIndexId");
            if (periodId == null)
                throw new ArgumentNullException("periodId");
            
            id = abstractUnitIndexId;
            this.periodId = periodId;
        }

        #endregion

        #region Public Methods
        //AddCustomField(CustomField);
        //RemoveCustomField(CustomField)

        #endregion

        #region IEntity Member

        public virtual bool SameIdentityAs(AbstractUnitIndex other)
        {
            return (other != null) && Id.Equals(other.Id);
        }

        #endregion

        #region Object override

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            var other = (AbstractUnitIndex)obj;
            return SameIdentityAs(other);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override string ToString()
        {
            return Id.ToString();
        }

        #endregion
    }
}
