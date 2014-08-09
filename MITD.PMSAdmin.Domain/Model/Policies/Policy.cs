using MITD.Domain.Model;
using MITD.PMSAdmin.Exceptions;
using System;

namespace MITD.PMSAdmin.Domain.Model.Policies
{
    public abstract class Policy : IEntity<Policy>
    {
        #region Fields

        protected readonly byte[] rowVersion;

        #endregion

        #region Properties

        private readonly PolicyId id;
        private string name;
        private string dictionaryName;

        public virtual PolicyId Id
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

        #endregion

        #region Constructors
        protected Policy()
        {
            //For OR mapper
        }

        public Policy(PolicyId policyId, string name, string dictionaryName)
        {
            if (policyId == null)
                throw new ArgumentNullException("policyId");

            this.id = policyId;
            if (string.IsNullOrWhiteSpace(name))
                throw new PolicyArgumentException("Policy", "Name");
            this.name = name;
            if (string.IsNullOrWhiteSpace(name))
                throw new PolicyArgumentException("Policy", "DictionaryName");
            this.dictionaryName = dictionaryName;
            this.id = policyId;
        }

        #endregion

        #region Public Methods

        public virtual void Update(string name, string dictionaryName)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new PolicyArgumentException("Policy", "Name");
            this.name = name;
            if (string.IsNullOrWhiteSpace(name))
                throw new PolicyArgumentException("Policy", "DictionaryName");
            this.dictionaryName = dictionaryName;

        }

        #endregion

        #region IEntity Member

        public virtual bool SameIdentityAs(Policy other)
        {
            return (other != null) && Id.Equals(other.Id);
        }

        #endregion

        #region Object override

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            var other = (Policy)obj;
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
