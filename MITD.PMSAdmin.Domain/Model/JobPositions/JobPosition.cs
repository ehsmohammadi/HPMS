using System;
using MITD.Domain.Model;
using MITD.PMSAdmin.Exceptions;

namespace MITD.PMSAdmin.Domain.Model.JobPositions
{
    public class JobPosition : IEntity<JobPosition>
    { 
        #region Fields

        private readonly byte[] rowVersion;

        #endregion

        #region Properties

        private readonly JobPositionId id;
        public virtual JobPositionId Id
        {
            get { return id; }
        }

        private string name;
        public virtual string Name
        {
            get { return name; }
           
        }

        private string dictionaryName;
        public virtual string DictionaryName
        {
            get { return dictionaryName; }
            
        }

        private Guid transferId;
        public virtual Guid TransferId { get { return transferId; }
            set { transferId = value; }
        }

        #endregion

        #region Constructors
        public JobPosition()
        {
           //For OR mapper
        }

        public JobPosition(JobPositionId jobPositionId, string name, string dictionaryName)
        {
            this.id = jobPositionId;
            if (string.IsNullOrWhiteSpace(name))
                throw new JobPositionArgumentException("JobPosition", "Name");
            this.name = name;
            if (string.IsNullOrWhiteSpace(name))
                throw new JobPositionArgumentException("JobPosition", "DictionaryName");
            this.dictionaryName = dictionaryName;
            this.id = jobPositionId;
        }

        #endregion

        #region Public Methods

        public virtual void Update(string name, string dictionaryName)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new JobPositionArgumentException("JobPosition", "Name");
            this.name = name;
            if (string.IsNullOrWhiteSpace(name))
                throw new JobPositionArgumentException("JobPosition", "DictionaryName");
            this.dictionaryName = dictionaryName;
        }

        #endregion

        #region IEntity Member

        public virtual bool SameIdentityAs(JobPosition other)
        {
            return (other != null) && Id.Equals(other.Id);
        }

        #endregion

        #region Object override

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            var other = (JobPosition)obj;
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
