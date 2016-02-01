using System;
using System.Collections.Generic;
using System.Linq;
using MITD.Domain.Model;
using MITD.PMSAdmin.Domain.Model.CustomFieldTypes;
using MITD.PMSAdmin.Exceptions;

namespace MITD.PMSAdmin.Domain.Model.UnitIndices
{
    public class UnitIndex:AbstractUnitIndex
    {


        #region Fields

        #endregion
       

        #region Properties

        
        private  UnitIndexCategory category;
        public virtual UnitIndexCategory Category { get { return category; } }

        private  IList<CustomFieldTypeId> customFieldTypeIdList=new List<CustomFieldTypeId>();
        public virtual IReadOnlyList<CustomFieldTypeId> CustomFieldTypeIdList
        {
            get { return customFieldTypeIdList.ToList().AsReadOnly(); }
        }  

        #endregion

        #region Constructors

        public UnitIndex()
        {

        }

        public UnitIndex(AbstractUnitIndexId unitIndexId, UnitIndexCategory category, string name,
                                string dictionaryName)
            : base(unitIndexId, name, dictionaryName)
        {
            if (category == null)
                throw new ArgumentException("category is null");
            this.category= category;
        }

       

        #endregion

        #region Public Methods
        public virtual void Update(string name, string dictionaryName,UnitIndexCategory category, List<CustomFieldType> customFieldTypes)
        {
            if(category == null)
                throw new ArgumentException("category");

            if (string.IsNullOrWhiteSpace(name))
                throw new UnitIndexArgumentException("UnitIndex", "Name");
            base.name = name;
            if (string.IsNullOrWhiteSpace(name))
                throw new UnitIndexArgumentException("UnitIndex", "DictionaryName");
            base.dictionaryName = dictionaryName;
            this.category = category;
            
            foreach (var customFieldType in customFieldTypes)
            {
                if (!customFieldTypeIdList.Contains(customFieldType.Id))
                {
                    AssignCustomField(customFieldType);
                }
            }

            var customFieldTypeIdListClone = new List<CustomFieldTypeId>(customFieldTypeIdList);
            for (int i = 0; i < customFieldTypeIdListClone.Count; i++)
            {
                if (!customFieldTypes.Select(c => c.Id).Contains(customFieldTypeIdListClone[i]))
                    RemoveCustomField(customFieldTypeIdListClone[i]);
            }


        }
        public virtual void AssignCustomField(CustomFieldType customFieldType)
        {
            if (customFieldTypeIdList == null)
                customFieldTypeIdList = new List<CustomFieldTypeId>();

            if (customFieldType == null)
                throw new UnitIndexArgumentException("UnitIndex","CustomFieldType");
            if (customFieldType.EntityId != EntityTypeEnum.UnitIndex)
                throw new UnitIndexArgumentException("UnitIndex", "CustomFieldType");

            customFieldTypeIdList.Add(customFieldType.Id);
        }

        public virtual void RemoveCustomField(CustomFieldTypeId customFieldId)
        {
            if (customFieldTypeIdList == null)
                customFieldTypeIdList = new List<CustomFieldTypeId>();

            if (customFieldId != null)
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

        public virtual void AssignCustomFields(List<CustomFieldType> customFieldTypes)
        {
            if (customFieldTypes == null)
                return;
            foreach (var customFieldType in customFieldTypes)
            {
                if (!customFieldTypeIdList.Contains(customFieldType.Id))
                {
                    AssignCustomField(customFieldType);
                }
            }

            for (int i = 0; i < customFieldTypeIdList.Count; i++)
            {
                if (!customFieldTypes.Select(c => c.Id).Contains(customFieldTypeIdList[i]))
                    RemoveCustomField(customFieldTypeIdList[i]);
            }
        }
        

        #endregion

       
    }
}
