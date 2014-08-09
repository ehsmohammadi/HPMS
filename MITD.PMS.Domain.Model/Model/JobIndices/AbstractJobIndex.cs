using System;
using MITD.Domain.Model;
using MITD.PMS.Domain.Model.Periods;

namespace MITD.PMS.Domain.Model.JobIndices
{
    public class AbstractJobIndex : IEntity<AbstractJobIndex>
    {

        #region Fields

        protected readonly byte[] rowVersion;

        #endregion

        #region Properties

        protected  AbstractJobIndexId id;
        public virtual AbstractJobIndexId Id
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
        public AbstractJobIndex()
        {
            //For OR mapper
        }

        public AbstractJobIndex(AbstractJobIndexId abstractJobIndexId, PeriodId periodId)
        {
            if (abstractJobIndexId == null)
                throw new ArgumentNullException("abstractJobIndexId");
            if (periodId == null)
                throw new ArgumentNullException("periodId");
            
            id = abstractJobIndexId;
            this.periodId = periodId;
        }

        #endregion

        #region Public Methods
        //AddCustomField(CustomField);
        //RemoveCustomField(CustomField)

        #endregion

        #region IEntity Member

        public virtual bool SameIdentityAs(AbstractJobIndex other)
        {
            return (other != null) && Id.Equals(other.Id);
        }

        #endregion

        #region Object override

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            var other = (AbstractJobIndex)obj;
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
