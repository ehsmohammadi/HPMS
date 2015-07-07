using System.Collections.Generic;
using MITD.Core;

namespace MITD.PMS.Presentation.Contracts
{
    public interface IPeriodUnitIndexServiceFasade : IFacadeService
    {
        IEnumerable<AbstractUnitIndexInPeriodDTOWithActions> GetAllAbstractUnitIndices(long periodId);
        AbstractUnitIndexInPeriodDTO GetAbstarctUnitIndexById(long id);
        AbstractUnitIndexInPeriodDTO AddUnitIndex(UnitIndexInPeriodDTO abstractIndex);
        AbstractUnitIndexInPeriodDTO AddUnitIndexGroup(UnitIndexGroupInPeriodDTO abstractIndex);
        AbstractUnitIndexInPeriodDTO UpdateUnitIndex(UnitIndexInPeriodDTO abstractIndex);
        AbstractUnitIndexInPeriodDTO UpdateUnitIndexGroup(UnitIndexGroupInPeriodDTO abstractIndex);
        string DeleteAbstractUnitIndex(long id);
        IEnumerable<AbstractUnitIndexInPeriodDTO> GetAllUnitIndices(long periodId);
        IEnumerable<AbstractUnitIndexInPeriodDTO> GetAllUnitIndexGrroups(long periodId);
    }
}
