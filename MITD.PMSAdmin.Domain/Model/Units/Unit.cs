using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.Domain.Model;
using MITD.PMSAdmin.Exceptions;
using MS.Internal.Xml.XPath;

namespace MITD.PMSAdmin.Domain.Model.Units
{
    public class Unit : IEntity<Unit>
    { 
        #region Fields

        private readonly byte[] rowVersion;
       
        #endregion

        #region Properties

        private readonly UnitId id;
        private  string name;
        private  string dictionaryName;

        public virtual UnitId Id
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
        public Unit()
        {
           //For OR mapper
        }

        public Unit(UnitId unitId, string name, string dictionaryName)
        {
            if (unitId == null)
                throw new ArgumentNullException("unitId");
            this.id = unitId;
            if (string.IsNullOrWhiteSpace(name))
                throw new UnitArgumentException("Unit","Name");
            this.name = name;
            if (string.IsNullOrWhiteSpace(name))
                throw new UnitArgumentException("Unit", "DictionaryName");
            this.dictionaryName = dictionaryName;

            this.id = unitId;
        }

        #endregion

        #region Public Methods

        public virtual void Update(string name, string dictionaryName)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new UnitArgumentException("Unit", "Name");
            this.name = name;
            if (string.IsNullOrWhiteSpace(name))
                throw new UnitArgumentException("Unit", "DictionaryName");
            this.dictionaryName = dictionaryName;

        }

        #endregion

        #region IEntity Member

        public virtual bool SameIdentityAs(Unit other)
        {
            return (other != null) && Id.Equals(other.Id);
        }

        #endregion

        #region Object override

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            var other = (Unit)obj;
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
