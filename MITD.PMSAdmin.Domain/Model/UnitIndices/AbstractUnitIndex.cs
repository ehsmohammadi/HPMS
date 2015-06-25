using System;
using MITD.Domain.Model;
using MITD.PMSAdmin.Exceptions;

namespace MITD.PMSAdmin.Domain.Model.UnitIndices
{
    public class AbstractUnitIndex : IEntity<AbstractUnitIndex>
    {

        #region Fields

        protected readonly byte[] rowVersion;

        #endregion

        #region Properties

        private readonly AbstractUnitIndexId id;
        public virtual AbstractUnitIndexId Id
        {
            get { return id; }
        }

        
        protected string name;
        public virtual string Name { get { return name; } }

        protected string dictionaryName;
        public virtual string DictionaryName { get { return dictionaryName; } }



        #endregion

        #region Constructors
        public AbstractUnitIndex()
        {
            //For OR mapper
        }

        public AbstractUnitIndex(AbstractUnitIndexId abstractUnitIndexId, string name, string dictionaryName)
        {
            if (abstractUnitIndexId == null)
                throw new ArgumentNullException("AbstractUnitIndexId");
            this.id = abstractUnitIndexId;
            if (string.IsNullOrWhiteSpace(name))
                throw new UnitIndexArgumentException("UnitIndex","Name"); 
            this.name = name;
            if (string.IsNullOrWhiteSpace(name))
                throw new UnitIndexArgumentException("UnitIndex", "DictionaryName");
            this.dictionaryName = dictionaryName;
        }

        #endregion

        #region Public Methods
        //AddCustomField(CustomField);
        //RemoveCustomField(CustomField)

        #endregion

        #region IEntity Member

        public virtual bool SameIdentityAs(AbstractUnitIndex other)
        {
            return (other != null) && Id.Equals(other.Id);
        }

        #endregion

        #region Object override

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            var other = (AbstractUnitIndex)obj;
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
