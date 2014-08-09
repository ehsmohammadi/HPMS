using System.Collections.Generic;
using MITD.Core;

namespace MITD.PMS.Presentation.Contracts
{
    public interface IPeriodUnitServiceFacade : IFacadeService
    {
        UnitInPeriodAssignmentDTO AssignUnit(long periodId, UnitInPeriodAssignmentDTO unitInPeriod);
        string RemoveUnit(long periodId, long unitId);
        IEnumerable<UnitInPeriodDTOWithActions> GetUnitsWithActions(long periodId);
        IEnumerable<UnitInPeriodDTO> GetUnits(long periodId);

        UnitInPeriodDTO GetUnit(long periodId, long unitId);
    }
}
