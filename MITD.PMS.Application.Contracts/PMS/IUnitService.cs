
using System.Collections.Generic;
using MITD.Core;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Domain.Model.Units;

namespace MITD.PMS.Application.Contracts
{
    public interface IUnitService : IService
    {
        List<UnitInquiryConfigurationItem> GetInquirySubjectWithInquirer(UnitId unitId);

        Unit UpdateUnit(UnitId unitId, List<SharedUnitCustomFieldId> customFieldIdList, IList<UnitIndexForUnit> unitIndexList);
   //     Unit AssignUnit(UnitId unitId, List<SharedUnitCustomFieldId> customFieldIdList, IList<UnitIndexForUnit> unitIndexList);

        Unit AssignUnit(UnitId parentId, UnitId unitId, List<SharedUnitCustomFieldId> customFieldIdList,
            IList<UnitIndexForUnit> unitIndexList);
        Unit AssignUnit(PeriodId periodId, SharedUnitId sharedUnitId, SharedUnitId parentSharedUnitId);
        void RemoveUnit(PeriodId periodId, SharedUnitId sharedUnitId);
        List<Unit> GetAllParentUnits(Period period);
        List<Unit> GetAllUnitByParentId(UnitId id);
        UnitId GetUnitIdBy(Period period, SharedUnitId sharedUnitId);
        Unit GetUnitBy(UnitId unitId);
    }
}
