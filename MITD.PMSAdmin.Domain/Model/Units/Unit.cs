using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.Domain.Model;
using MITD.PMSAdmin.Domain.Model.CustomFieldTypes;
using MITD.PMSAdmin.Domain.Model.Jobs;
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

        private Guid transferId;
        public virtual Guid TransferId { get { return transferId; } set { transferId = value; }}  

        private IList<CustomFieldTypeId> customFieldTypeIdList = new List<CustomFieldTypeId>();
        public virtual IReadOnlyList<CustomFieldTypeId> CustomFieldTypeIdList
        {
            get { return customFieldTypeIdList.ToList().AsReadOnly(); }
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

        public virtual void Update(string name, string dictionaryName, List<CustomFieldType> customFieldTypes)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new UnitArgumentException("Unit", "Name");
            this.name = name;
            if (string.IsNullOrWhiteSpace(name))
                throw new UnitArgumentException("Unit", "DictionaryName");
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
                throw new UnitArgumentException("Unit", "CustomFieldType");
            if (customFieldType.EntityId != EntityTypeEnum.Unit)
                throw new UnitArgumentException("Unit", "CustomFieldType");

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
