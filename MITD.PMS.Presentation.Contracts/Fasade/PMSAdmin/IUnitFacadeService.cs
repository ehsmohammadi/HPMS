using System;
using System.Collections.Generic;
using MITD.Core;

namespace MITD.PMS.Presentation.Contracts
{
    public interface IUnitFacadeService:IFacadeService
    {
         
        PageResultDTO<UnitDTOWithActions> GetAllUnits(int pageSize, int pageIndex, QueryStringConditions queryStringConditions);
        UnitDTO AddUnit(UnitDTO customField);
        UnitDTO UpdateUnit(UnitDTO customField);
        UnitDTO GetUnitById(long unitId);
        UnitDTO GetUnitByTransferId(Guid transferId);
        string DeleteUnit(long unitId);
        List<UnitDTO> GetAllUnits();
        
    }
}
