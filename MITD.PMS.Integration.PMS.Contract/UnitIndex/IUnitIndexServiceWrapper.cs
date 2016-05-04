using System;
using System.Collections.Generic;
using MITD.PMS.Presentation.Contracts;
using MITD.Presentation;

namespace MITD.PMS.Integration.PMS.Contract
{
    public partial interface IUnitIndexServiceWrapper : IServiceWrapper
    {
        UnitIndexDTO GetUnitIndex(long id);
        UnitIndexDTO AddUnitIndex( UnitIndexDTO unitIndex);

        UnitIndexDTO GetUnitIndexByTransferId(Guid guid);

        #region Not Use
        //void UpdateUnitIndex(Action<UnitIndexDTO, Exception> action, UnitIndexDTO unitIndex);
        //void DeleteUnitIndex(Action<string, Exception> action, long id);
        //void GetAllAbstractUnitIndex(Action<List<AbstractUnitIndexDTOWithActions>, Exception> action);
        //void GetAllUnitIndex(Action<List<UnitIndexDTO>, Exception> action); 
        #endregion
    }
}
