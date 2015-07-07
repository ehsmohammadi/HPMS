using System;
using MITD.Domain.Model;

namespace MITD.PMS.Domain.Model.Units
{
    public class UnitCustomField: EntityWithDbId<long,UnitCustomFieldId>, IEntity<UnitCustomField>
    { 
        #region Fields

        private readonly byte[] rowVersion;
        #endregion

        #region Properties

        
        public virtual UnitCustomFieldId Id
        {
            get { return id; }
            
        }

        private readonly SharedUnitCustomField sharedUnitCustomField;
        public virtual SharedUnitCustomField SharedUnitCustomField
        {
            get { return sharedUnitCustomField; }

        }

        #region customField Temp Properties
        public virtual string Name
        {
            get { return SharedUnitCustomField.Name; }

        }
        public virtual string DictionaryName
        {
            get { return SharedUnitCustomField.DictionaryName; }

        }
        public virtual long MinValue
        {
            get { return SharedUnitCustomField.MinValue; }

        }
        public virtual long MaxValue
        {
            get { return SharedUnitCustomField.MaxValue; }

        }
        public virtual string TypeId
        {
            get { return SharedUnitCustomField.TypeId; }

        }
        #endregion

        #endregion

        #region Constructors
        protected UnitCustomField()
        {
           //For OR mapper
        }

        public UnitCustomField(UnitCustomFieldId id,SharedUnitCustomField customField)
        {
            if (id == null)
                throw new ArgumentNullException("id");
            this.id = id;
            
            if (customField == null)
                throw new ArgumentNullException("customField");
            sharedUnitCustomField = customField;
        }

       
       
        #endregion

        #region Public Methods
       
        #endregion

        #region IEntity Member

        public virtual bool SameIdentityAs(UnitCustomField other)
        {
            return (other != null) && Id.Equals(other.Id);
        }


        #endregion

        #region Object override

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            var other = (UnitCustomField)obj;
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
