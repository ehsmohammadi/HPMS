using System;
using MITD.Domain.Model;

namespace MITD.PMS.Domain.Model.Jobs
{
    public class SharedJobCustomField: IEntity<SharedJobCustomField>
    { 
        #region Fields
        
       
        #endregion

        #region Properties

        private readonly SharedJobCustomFieldId id;
        public virtual SharedJobCustomFieldId Id
        {
            get { return id; }
            
        }

        private readonly string name;
        public virtual string Name { get { return name; } }

        private readonly string dictionaryName;
        public virtual string DictionaryName { get { return dictionaryName; } }

        private readonly long minValue;
        public virtual long MinValue
        {
            get { return minValue; }

        }
        private readonly long maxValue;
        public virtual long MaxValue
        {
            get { return maxValue; }

        }
        
        private readonly string typeId;
        public virtual string TypeId
        {
            get { return typeId; }

        }
        #endregion

        #region Constructors
        public SharedJobCustomField()
        {
           //For OR mapper
        }

        public SharedJobCustomField(SharedJobCustomFieldId id, 
                           string name,
                           string dictionaryName, long minValue, long maxValue, string typeId)
        {
            if (id == null)
                throw new ArgumentNullException("id");
            this.id = id;
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("name");
            this.name = name;
            
            if (string.IsNullOrWhiteSpace(dictionaryName))
                throw new ArgumentNullException("dictionaryName");
            this.dictionaryName = dictionaryName;
            this.minValue = minValue;
            this.maxValue = maxValue;
            this.typeId = typeId;


        }

       
       
        #endregion

        #region Public Methods
       
        #endregion

        #region IEntity Member

        public virtual bool SameIdentityAs(SharedJobCustomField other)
        {
            return (other != null) && Id.Equals(other.Id);
        }


        #endregion

        #region Object override

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            var other = (SharedJobCustomField)obj;
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
