using MITD.PMS.Presentation.Contracts;
using System.Collections.Generic;
using System.Web.Http;

namespace MITD.PMS.Service.Host.Controllers
{
    [Authorize]
    public class PeriodUnitsController : ApiController
    {
        private const string unitInPeriodWithActionsClassType = "UnitInPeriodDTOWithActions";
        private const string unitInPeriodClassType = "UnitInPeriodDTO";

        private IPeriodUnitServiceFacade periodUnitService;

        public PeriodUnitsController(IPeriodUnitServiceFacade periodUnitService)
        {
            this.periodUnitService = periodUnitService;
        }

       
        public PageResultDTO<UnitInPeriodDTOWithActions> GetAllUnites(long periodId, int pageSize, int pageIndex,
                                                             string filter = "", string sortBy = "", string selectedColumns ="")
        {
            var queryStringCondition = new QueryStringConditions { SortBy = sortBy, Filter = filter };
            return periodUnitService.GetAllUnits(periodId,pageSize, pageIndex, queryStringCondition, selectedColumns);
        }

        public UnitInPeriodDTO PutUnit(long periodId, UnitInPeriodDTO unitInPeriod)
        {
            //update customFields
            return periodUnitService.UpdateUnit(periodId, unitInPeriod);
        }

        public UnitInPeriodDTO PostUnit(long periodId, UnitInPeriodDTO unitInPeriod)
        {
            return periodUnitService.AssignUnit(periodId, unitInPeriod);
        }

        public void DeleteUnit(long periodId, long unitId)
        {
            periodUnitService.RemoveUnit(periodId, unitId);
        }

        public UnitInPeriodDTO GetUnit(long periodId, long unitId, string Type)
        {
            if (Type.ToLower() == unitInPeriodWithActionsClassType.ToLower())
                return periodUnitService.GetUnit(periodId, unitId, "");
            return periodUnitService.GetUnit(periodId, unitId, "");
        }

        public IEnumerable<UnitInPeriodDTO> GetAllUnitInPeriods(long periodId, string Type)
        {
            if (Type.ToLower() == unitInPeriodWithActionsClassType.ToLower())
                return periodUnitService.GetUnitsWithActions(periodId);
            return periodUnitService.GetUnits(periodId);
        }

       

    }
}