using System.Collections.Generic;
using MITD.Core;
using MITD.PMSAdmin.Domain.Model.CustomFieldTypes;
using MITD.PMSAdmin.Domain.Model.UnitIndices;

namespace MITD.PMSAdmin.Application.Contracts
{
    public interface IUnitIndexService : IService
    {
        UnitIndexCategory AddUnitIndexCategory(AbstractUnitIndexId parentId, string name, string dictionaryName);
        UnitIndex AddUnitIndex(AbstractUnitIndexId categoryId, string name, string dictionaryName
            , IList<CustomFieldTypeId> customFieldTypeIdList);

        UnitIndexCategory UpdateUnitIndexCategory(AbstractUnitIndexId unitIndexCatId, AbstractUnitIndexId parentId,
                                                string name, string dictionaryName);

        UnitIndex UpdateUnitIndex(AbstractUnitIndexId unitIndexId, AbstractUnitIndexId categoryId,
            string name, string dictionaryName, IList<CustomFieldTypeId> customFieldTypeIdList);

        void DeleteAbstractUnitIndex(AbstractUnitIndexId abstractUnitIndexId);
        bool IsValidCustomFieldIdList(AbstractUnitIndexId unitIndexId, IList<CustomFieldTypeId> customFieldTypeIds);
        UnitIndex GetBy(AbstractUnitIndexId unitIndexId);
    }
}
