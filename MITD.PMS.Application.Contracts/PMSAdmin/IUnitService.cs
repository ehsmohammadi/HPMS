using System;
using System.Collections.Generic;
using MITD.Core;
using MITD.PMSAdmin.Domain.Model.CustomFieldTypes;
using MITD.PMSAdmin.Domain.Model.Units;

namespace MITD.PMSAdmin.Application.Contracts
{
    public interface IUnitService:IService
    {

        Unit AddUnit(string name, string dictionaryName, IList<CustomFieldTypeId> customFieldTypeIdList, Guid transferId);

        Unit UppdateUnit(UnitId unitId, string name, string dictionaryName,
            IList<CustomFieldTypeId> customFieldTypeIdList);
        void DeleteUnit(UnitId unitId);
        Unit GetBy(UnitId unitId);
        bool IsValidCustomFieldIdList(UnitId unitId, IList<CustomFieldTypeId> customFieldTypeIds);
    }
}
