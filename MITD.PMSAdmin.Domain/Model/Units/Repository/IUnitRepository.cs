using System;
using System.Collections.Generic;
using MITD.Domain.Repository;

namespace MITD.PMSAdmin.Domain.Model.Units
{
    public interface  IUnitRepository:IRepository
    { 
        void FindBy(ListFetchStrategy<Unit> fs);
        void Add(Unit unit);
        void UpdateUnit(Unit unit);
        Unit GetById(UnitId unitId);
        Unit GetByTransferId(Guid transferId);
        UnitId GetNextId();
        void DeleteUnit(Unit unit);
        List<Unit> GetAll();

        UnitException ConvertException(Exception exp);
        UnitException TryConvertException(Exception exp);
        
    }
}
