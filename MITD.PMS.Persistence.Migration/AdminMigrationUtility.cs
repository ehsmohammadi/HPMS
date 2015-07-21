using System.Collections.Generic;
using System.Linq;
using MITD.PMSAdmin.Domain.Model.CustomFieldTypes;
using MITD.PMSAdmin.Domain.Model.UnitIndices;
using MITD.PMSAdmin.Domain.Model.Units;

namespace MITD.PMS.Persistence
{
    public static class AdminMigrationUtility
    {
        public static List<CustomFieldType> DefinedCustomFields =new List<CustomFieldType>();
        public static UnitIndexCategory unitIndexCategory;

        public static void DefineCustomFieldType(ICustomFieldRepository customFieldRepository, string name,
            string dictionaryName, int min, int max, EntityTypeEnum entityType)
        {
            var customfieldType = new CustomFieldType(customFieldRepository.GetNextId(),
                name, dictionaryName, min, max, entityType, "string");
            customFieldRepository.Add(customfieldType);
            DefinedCustomFields.Add(customfieldType);
        }


        public static void CreateUnit(IUnitRepository unitRepository, string name, string dictionaryName)
        {
            var unit = new Unit(unitRepository.GetNextId(),
                        name, dictionaryName);
            unit.AssignCustomFields(DefinedCustomFields.Where(dc => dc.EntityId == EntityTypeEnum.Unit).ToList());
            unitRepository.Add(unit);
            //unitList.Add(unit);
        }

        public static void CreateUnitIndex(IUnitIndexRepository unitIndexRepository, string name, string dictionaryName)
        {
            if (unitIndexCategory == null)
            {
                unitIndexCategory = new UnitIndexCategory(unitIndexRepository.GetNextId(), null, "شاخص های سازمانی",
                    "UnitIndices");
                unitIndexRepository.Add(unitIndexCategory);
            }

            var unitIndex = new UnitIndex(unitIndexRepository.GetNextId(), unitIndexCategory,
                    name, dictionaryName);
            unitIndex.AssignCustomFields(DefinedCustomFields.Where(dc => dc.EntityId == EntityTypeEnum.UnitIndex).ToList());
            unitIndexRepository.Add(unitIndex);
            // GenralUnitIndexList.Add(new UnitindexDes { UnitIndex = jobIndex1, Importance = "7", IsInquirable = true });
        }


    }
}
