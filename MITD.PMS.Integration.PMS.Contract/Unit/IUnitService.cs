using System;
using System.Collections.Generic;
using MITD.PMS.Presentation.Contracts;
using MITD.Presentation;

namespace MITD.PMS.Integration.PMS.Contract
{
    public interface IUnitServiceWrapper : IServiceWrapper
    {

        UnitDTO GetUnit(long id);
        UnitDTO AddUnit(UnitDTO unit);

        //void UpdateUnit(Action<UnitDTO, Exception> action, UnitDTO unit);
        //void GetAllUnits(Action<PageResultDTO<UnitDTOWithActions>, Exception> action, int pageSize, int pagePost);
        //void DeleteUnit(Action<string, Exception> action, long id);
        //void GetAllUnits(Action<List<UnitDTO>, Exception> action);

    }
}
