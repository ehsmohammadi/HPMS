using System;
using MITD.Domain.Model;
using MITD.PMSAdmin.Exceptions;

namespace MITD.PMSAdmin.Domain.Model.CustomFieldTypes
{
    public class CustomFieldType : IEntity<CustomFieldType>
    {
         
        #region Fields

        private readonly byte[] rowVersion;

        #endregion

        #region Properties

        private readonly CustomFieldTypeId id;
        private readonly string name;
        private readonly string dictionaryName;
        private readonly long minValue;
        private readonly long maxValue;
        private readonly EntityTypeEnum entityId;
        private readonly string typeId;

        public virtual CustomFieldTypeId Id
        {
            get { return id; }
        }

        

        public virtual string Name
        {
            get { return name; }

        }

        public virtual string DictionaryName
        {
            get { return dictionaryName; }

        }

        public virtual long MinValue
        {
            get { return minValue; }

        }

        public virtual long MaxValue
        {
            get { return maxValue; }

        }

        public virtual EntityTypeEnum EntityId
        {
            get { return entityId; }

        }

        public virtual string TypeId
        {
            get { return typeId; }

        }

        #endregion

        #region Constructors
        protected CustomFieldType()
        {
            //For OR mapper
        }

        public CustomFieldType(CustomFieldTypeId customFieldTypeId, string name, string dictionaryName,
                             long minValue, long maxValue, EntityTypeEnum entityId, string typeId)
        {
            
            if (customFieldTypeId == null)
                throw new ArgumentNullException("customFieldTypeId");
            id = customFieldTypeId;
            if (string.IsNullOrWhiteSpace(name))
                throw new CustomFieldTypeArgumentException("CustomFieldType","Name");
            this.name = name;
            if (string.IsNullOrWhiteSpace(name))
                throw new CustomFieldTypeArgumentException("CustomFieldType", "DictionaryName");
            this.dictionaryName = dictionaryName;
            if (minValue > maxValue)
                throw new CustomFieldTypeCompareException("CustomFieldType", "MinValue","MaxValue");
            this.minValue = minValue;
            this.maxValue = maxValue;

            this.entityId = entityId;
            this.typeId = typeId;
        }

        #endregion

        #region IEntity Member

        public virtual bool SameIdentityAs(CustomFieldType other)
        {
            return (other != null) && Id.Equals(other.Id);
        }

        #endregion

        #region Object override

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            var other = (CustomFieldType)obj;
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
