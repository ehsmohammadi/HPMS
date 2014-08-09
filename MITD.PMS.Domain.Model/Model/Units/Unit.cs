using System.Data;
using MITD.Domain.Model;
using System;
using MITD.PMS.Domain.Model.Periods;

namespace MITD.PMS.Domain.Model.Units
{
    public class Unit : EntityWithDbId<long,UnitId>, IEntity<Unit>
    {
        #region Fields

        private readonly byte[] rowVersion;
        private readonly SharedUnit sharedUnit;

        #endregion

        #region Properties

        public virtual UnitId Id
        {
            get { return id; }

        }
       
        public virtual string Name { get { return sharedUnit.Name; } }

        public virtual string DictionaryName { get { return sharedUnit.DictionaryName; } }

        public virtual SharedUnit SharedUnit { get { return sharedUnit; } }

        private readonly Unit parent;
        public virtual Unit Parent
        {
            get { return parent; }
        }

        #endregion

        #region Constructors
        protected Unit()
        {
            //For OR mapper
        }

        public Unit(Period period, SharedUnit sharedUnit,Unit parent)
        {
            if (period == null || period.Id == null)
                throw new ArgumentNullException("period");
            if(sharedUnit==null || sharedUnit.Id==null)
                throw new ArgumentNullException("sharedUnit");
            period.CheckAssigningUnit();
            id = new UnitId(period.Id,sharedUnit.Id);
            this.sharedUnit = sharedUnit;
            this.parent = parent;
        }



        #endregion

        #region Public Methods

        #endregion

        #region IEntity Member

        public virtual bool SameIdentityAs(Unit other)
        {
            return (other != null) && Id.Equals(other.Id);
        }

        #endregion

        #region Object override

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            var other = (Unit)obj;
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
