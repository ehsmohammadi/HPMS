using System.Collections.Generic;
using MITD.Core;
using MITD.PMSAdmin.Domain.Model.CustomFieldTypes;

namespace MITD.PMSAdmin.Application.Contracts
{
    public interface ICustomFieldService:IService
    { 
        CustomFieldType AddCustomFieldType(string name, string dictionaryName,
                             long minValue, long maxValue, int entityId, string typeId);
        CustomFieldType UpdateCustomFieldType(CustomFieldTypeId id, string name, string dictionaryName,
                             long minValue, long maxValue, int entityId, string typeId);
        List<CustomFieldType> GetBy(List<CustomFieldTypeId> customFieldIdList);

        void DeleteCustomField(CustomFieldType customFieldType);
    }
}
