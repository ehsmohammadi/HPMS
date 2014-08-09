using System;
using System.Collections.Generic;
using MITD.Domain.Model;
using MITD.PMS.Domain.Model.JobIndices;

namespace MITD.PMS.Domain.Model.JobIndices
{
    public class SharedJobIndexCustomField: IEntity<SharedJobIndexCustomField>
    { 
        #region Fields

        
       
        #endregion

        #region Properties
        
        private readonly SharedJobIndexCustomFieldId id;
        public virtual SharedJobIndexCustomFieldId Id
        {
            get { return id; }
            
        }

        private readonly string name;
        public virtual string Name { get { return name; } }

        private readonly string dictionaryName;
        public virtual string DictionaryName { get { return dictionaryName; } }

        private readonly long minValue;
        public virtual long MinValue { get { return minValue; } }

        private readonly long maxValue;
        public virtual long MaxValue { get { return maxValue; } }

     

        #endregion

        #region Constructors
        public SharedJobIndexCustomField()
        {
           //For OR mapper
        }

        public SharedJobIndexCustomField(SharedJobIndexCustomFieldId id, 
                           string name, 
                           string dictionaryName ,long minValue, long maxValue)
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
          

        }

       
       
        #endregion

        #region Public Methods
       
        #endregion

        #region IEntity Member

        public virtual bool SameIdentityAs(SharedJobIndexCustomField other)
        {
            return (other != null) && Id.Equals(other.Id);
        }


        #endregion

        #region Object override

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            var other = (SharedJobIndexCustomField)obj;
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
