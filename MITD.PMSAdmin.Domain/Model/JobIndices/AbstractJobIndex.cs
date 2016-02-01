using System;
using MITD.Domain.Model;
using MITD.PMSAdmin.Exceptions;

namespace MITD.PMSAdmin.Domain.Model.JobIndices
{
    public class AbstractJobIndex : IEntity<AbstractJobIndex>
    {

        #region Fields

        protected readonly byte[] rowVersion;

        #endregion

        #region Properties

        private readonly AbstractJobIndexId id;
        public virtual AbstractJobIndexId Id
        {
            get { return id; }
        }

        
        protected string name;
        public virtual string Name { get { return name; } }

        protected string dictionaryName;
        public virtual string DictionaryName { get { return dictionaryName; } }

        private Guid transferId;
        public virtual Guid TransferId { get { return transferId; } set { transferId = value; }}  

        #endregion

        #region Constructors
        public AbstractJobIndex()
        {
            //For OR mapper
        }

        public AbstractJobIndex(AbstractJobIndexId abstractJobIndexId, string name, string dictionaryName)
        {
            if (abstractJobIndexId == null)
                throw new ArgumentNullException("AbstractJobIndexId");
            this.id = abstractJobIndexId;
            if (string.IsNullOrWhiteSpace(name))
                throw new JobIndexArgumentException("JobIndex","Name"); 
            this.name = name;
            if (string.IsNullOrWhiteSpace(name))
                throw new JobIndexArgumentException("JobIndex", "DictionaryName");
            this.dictionaryName = dictionaryName;
        }

        #endregion

        #region Public Methods
        //AddCustomField(CustomField);
        //RemoveCustomField(CustomField)

        #endregion

        #region IEntity Member

        public virtual bool SameIdentityAs(AbstractJobIndex other)
        {
            return (other != null) && Id.Equals(other.Id);
        }

        #endregion

        #region Object override

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            var other = (AbstractJobIndex)obj;
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
