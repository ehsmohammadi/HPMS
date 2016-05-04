using System;
using System.Collections.Generic;
using MITD.PMS.Presentation.Contracts;
using System.Web.Http;

namespace MITD.PMS.Service.Host.Controllers
{
    [Authorize]
    public class UnitsController : ApiController
    {
        private readonly IUnitFacadeService unitService;

        public UnitsController(IUnitFacadeService unitService)
        {
            this.unitService = unitService;
        } 

        public PageResultDTO<UnitDTOWithActions> GetAllUnits(int pageSize, int pageIndex,string filter="",string sortBy="")
        {
            var queryStringCondition = new QueryStringConditions { SortBy = sortBy, Filter = filter };
            return unitService.GetAllUnits(pageSize, pageIndex, queryStringCondition);
        }

        public List<UnitDTO> GetAllUnits()
        {
            return unitService.GetAllUnits();
        }

        public UnitDTO PostUnit(UnitDTO unit)
        {
            return unitService.AddUnit(unit);
        }
        public UnitDTO PutUnit(UnitDTO unit)
        {
            return unitService.UpdateUnit(unit);
        }

        public UnitDTO GetUnit(long id)
        {
            return unitService.GetUnitById(id);
        }
        public UnitDTO GetByTransferId(Guid transferId)
        {
            return unitService.GetUnitByTransferId(transferId);
        }

        public string DeleteUnit(long id)
        {
            return unitService.DeleteUnit(id);
        }
    }
}