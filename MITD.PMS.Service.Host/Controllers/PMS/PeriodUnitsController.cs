using MITD.PMS.Presentation.Contracts;
using System.Collections.Generic;
using System.Web.Http;

namespace MITD.PMS.Service.Host.Controllers
{
    [Authorize]
    public class PeriodUnitsController : ApiController
    {
        private readonly IPeriodUnitServiceFacade periodUnitService;

        public PeriodUnitsController(IPeriodUnitServiceFacade periodUnitService)
        {
            this.periodUnitService = periodUnitService;
        }

        public UnitInPeriodAssignmentDTO PostUnit(long periodId, UnitInPeriodAssignmentDTO unitInPeriod)
        {
            return periodUnitService.AssignUnit(periodId, unitInPeriod);
        }

        public string DeleteUnit(long periodId, long unitId)
        {
            return periodUnitService.RemoveUnit(periodId, unitId);
        }

        public UnitInPeriodDTO GetUnit(long periodId, long unitId)
        {
            return periodUnitService.GetUnit(periodId, unitId);
        }

        public IEnumerable<UnitInPeriodDTO> GetAllUnitInPeriods(long periodId, string type)
        {
            if (string.Equals(type, "UnitInPeriodDTOWithActions"))
            {
                return periodUnitService.GetUnitsWithActions(periodId);
            }
            return periodUnitService.GetUnits(periodId);
        }
    }
}