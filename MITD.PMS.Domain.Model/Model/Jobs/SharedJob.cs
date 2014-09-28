using System;
using MITD.Domain.Model;

namespace MITD.PMS.Domain.Model.Jobs
{
    public class SharedJob: IEntity<SharedJob>
    { 
        #region Fields
       
       
        #endregion

        #region Properties
        
        private readonly SharedJobId id;
        public virtual SharedJobId Id
        {
            get { return id; }
            
        }

        private readonly string name;
        public virtual string Name { get { return name; } }

        private readonly string dictionaryName;
        public virtual string DictionaryName { get { return dictionaryName; } }
      
        
        #endregion

        #region Constructors
        public SharedJob()
        {
           //For OR mapper
        }

        public SharedJob(SharedJobId id, 
                           string name, 
                           string dictionaryName )
        {
            if (id == null)
                throw new ArgumentNullException("id");
            this.id = id;
            if(string.IsNullOrWhiteSpace(name))
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

        public virtual bool SameIdentityAs(SharedJob other)
        {
            return (other != null) && Id.Equals(other.Id);
        }


        #endregion

        #region Object override

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            var other = (SharedJob)obj;
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
