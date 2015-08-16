using System;
using System.Collections.Generic;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Integration.PMS.API
{
    public interface IUnitServiceWrapper
    {

        void GetUnit(Action<UnitDTO, Exception> action, long id);
        void AddUnit(Action<UnitDTO, Exception> action, UnitDTO unit);
        void UpdateUnit(Action<UnitDTO, Exception> action, UnitDTO unit);
        void GetAllUnits(Action<PageResultDTO<UnitDTOWithActions>, Exception> action, int pageSize, int pagePost);
        void DeleteUnit(Action<string, Exception> action, long id);
        void GetAllUnits(Action<List<UnitDTO>, Exception> action);

    }
}
