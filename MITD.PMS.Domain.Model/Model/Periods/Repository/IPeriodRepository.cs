using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MITD.Domain.Repository;
using MITD.PMS.Domain.Model.Periods;

namespace MITD.PMS.Domain.Model.Periods
{
    public interface IPeriodRepository : IRepository
    {
        long GetNextId();
        void Add(Period period);
        Period GetById(PeriodId periodId);
        void GetAll(ListFetchStrategy<Period> fs);
        List<Period> GetAll();
        void Delete(Period period);
        Period GetBy(Expression<Func<Period, bool>> precidate);
        void DeleteBasicData(PeriodId id);
        List<Period> GetPeriodsWithDeterministicCalculation();

        Exception ConvertException(Exception exp);
        Exception TryConvertException(Exception exp);
    }
}
