using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MITD.Core;
using MITD.PMS.Domain.Model.UnitIndices;
using MITD.PMS.Domain.Model.Periods;

namespace MITD.PMS.Application.Contracts
{
    public interface IUnitIndexService : IService
    {
        UnitIndexGroup AddUnitIndexGroup(PeriodId periodId, AbstractUnitIndexId parentId, string name, string dictionaryName);
        UnitIndex AddUnitIndex(PeriodId periodId, AbstractUnitIndexId groupId, SharedUnitIndexId jabIndexId, IDictionary<SharedUnitIndexCustomFieldId, string> customFieldValues, bool isInquireable, int sortOrder, long calculationLevel);

        UnitIndexGroup UpdateUnitIndexGroup(AbstractUnitIndexId UnitIndexGroupId, AbstractUnitIndexId parentId,
                                                string name, string dictionaryName);

        UnitIndex UpdateUnitIndex(AbstractUnitIndexId UnitIndexId, AbstractUnitIndexId groupId, IDictionary<SharedUnitIndexCustomFieldId, string> customFieldValues, bool isInquireable, int calculationOrder, long calculationLevel);

        void DeleteAbstractUnitIndex(AbstractUnitIndexId abstractUnitIndexId);

        IList<SharedUnitIndexCustomField> GetSharedUnitIndexCustomField(SharedUnitIndexId sharedUnitIndexId, List<SharedUnitIndexCustomFieldId> fieldIdList);

        IEnumerable<UnitIndex> FindUnitIndices(IEnumerable<AbstractUnitIndexId> unitIndexIds);
        //bool IsValidCustomFieldIdList(AbstractUnitIndexId UnitIndexId, IList<SharedUnitIndexCustomFieldId> customFieldTypeIds);
        //bool IsValidCustomFieldIdList(SharedUnitIndexId UnitIndexId, IList<SharedUnitIndexCustomFieldId> customFieldTypeIds);
        List<AbstractUnitIndex> GetAllAbstractUnitIndexByParentId(AbstractUnitIndexId id);
        List<UnitIndexGroup> GetAllParentUnitIndexGroup(Period period);
        SharedUnitIndexId GetSharedUnitIndexIdBy(AbstractUnitIndexId abstractUnitIndexId);
        AbstractUnitIndexId GetUnitIndexIdBy(Period UnitIndexIds, SharedUnitIndexId sharedUnitIndexId);
        AbstractUnitIndex GetUnitIndexById(AbstractUnitIndexId UnitIndexId);

    }
}
