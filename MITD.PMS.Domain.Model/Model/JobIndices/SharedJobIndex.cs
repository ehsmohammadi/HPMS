using System;
using System.Collections.Generic;
using MITD.Domain.Model;
using MITD.PMS.Domain.Model.JobIndices;

namespace MITD.PMS.Domain.Model.JobIndices
{
    public class SharedJobIndex: IEntity<SharedJobIndex>
    { 
        #region Fields
       
       
        #endregion

        #region Properties
        
        private readonly SharedJobIndexId id;
        public virtual SharedJobIndexId Id
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
        protected SharedJobIndex()
        {
           //For OR mapper
        }

        public SharedJobIndex(SharedJobIndexId id, 
                           string name, 
                           string dictionaryName )
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
            
        }

       
       
        #endregion

        #region Public Methods
       
        #endregion

        #region IEntity Member

        public virtual bool SameIdentityAs(SharedJobIndex other)
        {
            return (other != null) && Id.Equals(other.Id);
        }


        #endregion

        #region Object override

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            var other = (SharedJobIndex)obj;
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
