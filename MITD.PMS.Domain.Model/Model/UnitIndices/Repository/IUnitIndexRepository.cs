using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MITD.Domain.Repository;
using MITD.PMS.Domain.Model.Periods;

namespace MITD.PMS.Domain.Model.UnitIndices
{
    public interface IUnitIndexRepository : IRepository
    {
        List<UnitIndex> GetAllUnitIndex(PeriodId periodId);
        List<UnitIndexGroup> GetAllUnitIndexGroup(PeriodId periodId);
        AbstractUnitIndex GetById(AbstractUnitIndexId UnitIndexId);

        void Add(AbstractUnitIndex UnitIndex);
        void Delete(AbstractUnitIndexId UnitIndexId);

        UnitIndex GetUnitIndexById(AbstractUnitIndexId id);
        UnitIndexGroup GetUnitIndexGroupById(AbstractUnitIndexId id);


        IEnumerable<AbstractUnitIndex> GetAll(PeriodId periodId);
        AbstractUnitIndexId GetNextId();
        void Update(AbstractUnitIndex UnitIndex);

        IEnumerable<UnitIndex> FindUnitIndices(Expression<Func<UnitIndex, bool>> where);
        List<AbstractUnitIndex> GetAllAbstractUnitIndexByParentId(AbstractUnitIndexId id);
        List<UnitIndexGroup> GetAllParentUnitIndexGroup(Period period);
        SharedUnitIndexId GetSharedUnitIndexIdBy(AbstractUnitIndexId abstractUnitIndexId);
        AbstractUnitIndexId GetUnitIndexIdBy(Period UnitIndexIds, SharedUnitIndexId sharedUnitIndexId);

        Exception ConvertException(Exception exp);
        Exception TryConvertException(Exception exp);
    }
}
