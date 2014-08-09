using System;
using System.Collections.Generic;
using System.Linq;
using MITD.Domain.Model;
using MITD.PMSAdmin.Domain.Model.CustomFieldTypes;
using MITD.PMSAdmin.Exceptions;

namespace MITD.PMSAdmin.Domain.Model.Jobs
{
    public class Job: IEntity<Job>
    { 
        #region Fields

        private readonly byte[] rowVersion;

        #endregion

        #region Properties
        
        private readonly JobId id;
        public virtual JobId Id
        {
            get { return id; }
            
        }

        private IList<CustomFieldTypeId> customFieldTypeIdList=new List<CustomFieldTypeId>();
        public virtual IReadOnlyList<CustomFieldTypeId> CustomFieldTypeIdList
        {
            get { return customFieldTypeIdList.ToList().AsReadOnly(); }
        }

        private string name;
        public virtual string Name { get { return name; } }

        private string dictionaryName;
        public virtual string DictionaryName { get { return dictionaryName; } }
      
        
        #endregion

        #region Constructors
        protected Job()
        {
           //For OR mapper
        }

        public Job(JobId jobId, string name, string dictionaryName )
        {
            if (jobId == null)
                throw new ArgumentNullException("jobId");
            this.id = jobId;
            if (string.IsNullOrWhiteSpace(name))
                throw new JobArgumentException("Job", "Name");
            this.name = name;
            if (string.IsNullOrWhiteSpace(name))
                throw new JobArgumentException("Job", "DictionaryName");
            this.dictionaryName = dictionaryName;

        }

       
       
        #endregion

        #region Public Methods

        public virtual void Update(string name, string dictionaryName,List<CustomFieldType> customFieldTypes)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new JobArgumentException("Job", "Name");
            this.name = name;
            if (string.IsNullOrWhiteSpace(name))
                throw new JobArgumentException("Job", "DictionaryName");
            this.dictionaryName = dictionaryName;

            foreach (var customFieldType in customFieldTypes)
            {
                if (!customFieldTypeIdList.Contains(customFieldType.Id))
                    AssignCustomField(customFieldType);
            }
            var customFieldTypeIdListClon = new List<CustomFieldTypeId>(customFieldTypeIdList);
            for (int i = 0; i < customFieldTypeIdListClon.Count; i++)
            {
                if (!customFieldTypes.Select(c => c.Id).Contains(customFieldTypeIdListClon[i]))
                    RemoveCustomField(customFieldTypeIdListClon[i]);
            }


        }

        public virtual void AssignCustomFields(List<CustomFieldType> customFieldTypes)
        {
            if (customFieldTypes == null)
                return;
            foreach (var customFieldType in customFieldTypes)
            {
                if (!customFieldTypeIdList.Contains(customFieldType.Id))
                    AssignCustomField(customFieldType);
            }

            for (int i = 0; i < customFieldTypeIdList.Count; i++)
            {
                if (!customFieldTypes.Select(c => c.Id).Contains(customFieldTypeIdList[i]))
                    RemoveCustomField(customFieldTypeIdList[i]);
            }
        }
        
        public virtual void AssignCustomField(CustomFieldType customFieldType)
        {
            if (customFieldTypeIdList == null)
                customFieldTypeIdList = new List<CustomFieldTypeId>();

            if (customFieldType == null)
                throw new JobArgumentException("Job", "CustomFieldType");
            if (customFieldType.EntityId != EntityTypeEnum.Job)
                throw new JobArgumentException("Job", "CustomFieldType");

            customFieldTypeIdList.Add(customFieldType.Id);
        }

        public virtual void RemoveCustomField(CustomFieldTypeId customFieldId)
        {
            if (customFieldTypeIdList == null)
                customFieldTypeIdList = new List<CustomFieldTypeId>();

            customFieldTypeIdList.Remove(customFieldId);
        }
        
        public virtual bool IsValidCustomFields(IList<CustomFieldType> customFieldList)
        {
            foreach (var customFieldType in customFieldList)
            {
                if (!customFieldTypeIdList.Contains(customFieldType.Id))
                    return false;
            }
            return true;
        }
        #endregion

        #region IEntity Member

        public virtual bool SameIdentityAs(Job other)
        {
            return (other != null) && Id.Equals(other.Id);
        }


      
        #endregion

        #region Object override

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            var other = (Job)obj;
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
