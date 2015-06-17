using System;
using System.Collections.Generic;
using MITD.Domain.Repository;

namespace MITD.PMSAdmin.Domain.Model.UnitIndices
{
    public interface IUnitIndexRepository : IRepository
    {
        IList<UnitIndex> GetAllUnitIndex(ListFetchStrategy<UnitIndex> fs);
        IList<UnitIndexCategory> GetAllUnitIndexCategory(ListFetchStrategy<UnitIndexCategory> fs);
        AbstractUnitIndexId GetNextId();
        void Add(AbstractUnitIndex unitIndex);
        void Update(AbstractUnitIndex unitIndexy);
        IList<AbstractUnitIndex> GetAll();
        AbstractUnitIndex GetById(AbstractUnitIndexId unitIndexId);
        void Delete(AbstractUnitIndexId unitIndexId);
        UnitIndexCategory GetUnitIndexCategory(AbstractUnitIndexId parentId);
        UnitIndex GetUnitIndex(AbstractUnitIndexId unitIndexId);

        IList<UnitIndex> GetAllUnitIndex();
        IList<UnitIndexCategory> GetAllUnitIndexCategory();

        UnitIndexException ConvertException(Exception exp);
        UnitIndexException TryConvertException(Exception exp);
    }
}
