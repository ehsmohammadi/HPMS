using System;
using System.Collections.Generic;
using MITD.Domain.Model;
using MITD.PMS.Domain.Model.Jobs;

namespace MITD.PMS.Domain.Model.Jobs
{
    public class JobCustomField: EntityWithDbId<long,JobCustomFieldId>, IEntity<JobCustomField>
    { 
        #region Fields

        private readonly byte[] rowVersion;
        #endregion

        #region Properties

        
        public virtual JobCustomFieldId Id
        {
            get { return id; }
            
        }

        private readonly SharedJobCustomField sharedJobCustomField;
        public virtual SharedJobCustomField SharedJobCustomField
        {
            get { return sharedJobCustomField; }

        }

        #region customField Temp Properties
        public virtual string Name
        {
            get { return SharedJobCustomField.Name; }

        }
        public virtual string DictionaryName
        {
            get { return SharedJobCustomField.DictionaryName; }

        }
        public virtual long MinValue
        {
            get { return SharedJobCustomField.MinValue; }

        }
        public virtual long MaxValue
        {
            get { return SharedJobCustomField.MaxValue; }

        }
        public virtual string TypeId
        {
            get { return SharedJobCustomField.TypeId; }

        }
        #endregion

        #endregion

        #region Constructors
        protected JobCustomField()
        {
           //For OR mapper
        }

        public JobCustomField(JobCustomFieldId id,SharedJobCustomField customField)
        {
            if (id == null)
                throw new ArgumentNullException("id");
            this.id = id;
            
            if (customField == null)
                throw new ArgumentNullException("customField");
            sharedJobCustomField = customField;
        }

       
       
        #endregion

        #region Public Methods
       
        #endregion

        #region IEntity Member

        public virtual bool SameIdentityAs(JobCustomField other)
        {
            return (other != null) && Id.Equals(other.Id);
        }


        #endregion

        #region Object override

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            var other = (JobCustomField)obj;
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
