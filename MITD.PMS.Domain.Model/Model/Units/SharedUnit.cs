using System;
using System.Collections.Generic;
using MITD.Domain.Model;
using MITD.PMS.Domain.Model.Units;

namespace MITD.PMS.Domain.Model.Units
{
    public class SharedUnit: IEntity<SharedUnit>
    { 
        #region Fields
       
       
        #endregion

        #region Properties
        
        private readonly SharedUnitId id;
        public virtual SharedUnitId Id
        {
            get { return id; }
            
        }

        private readonly string name;
        public virtual string Name { get { return name; } }

        private readonly string dictionaryName;
        public virtual string DictionaryName { get { return dictionaryName; } }

        private readonly Guid transferId;
        public virtual Guid TransferId { get { return transferId; } }        

        #endregion

        #region Constructors
        public SharedUnit()
        {
           //For OR mapper
        }

        public SharedUnit(SharedUnitId id, 
                           string name, 
                           string dictionaryName )
        {
            if (id == null)
                throw new ArgumentNullException("id");
            this.id = id;
            this.name = name;
            this.dictionaryName = dictionaryName;
            
        }

       
       
        #endregion

        #region Public Methods
       
        #endregion

        #region IEntity Member

        public virtual bool SameIdentityAs(SharedUnit other)
        {
            return (other != null) && Id.Equals(other.Id);
        }


        #endregion

        #region Object override

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            var other = (SharedUnit)obj;
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
