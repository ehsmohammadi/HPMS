using System;
using System.Collections.Generic;
using MITD.Domain.Repository;
using MITD.PMS.Domain.Model.Periods;

namespace MITD.PMS.Domain.Model.Units
{
    public interface IUnitRepository : IRepository
    {
        List<Unit> GetUnits(PeriodId periodId);
        void Add(Unit unit);
        void DeleteUnit(Unit unit);
        Unit GetBy(UnitId unitId);
        List<Unit> GetAllParentUnits(Period period);
        List<Unit> GetAllUnitByParentId(UnitId id);
        UnitId GetUnitIdBy(Period period, SharedUnitId sharedUnitId);

        Exception ConvertException(Exception exp);
        Exception TryConvertException(Exception exp);
    }
}
