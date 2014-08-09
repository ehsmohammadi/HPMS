using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MITD.Domain.Repository;
using MITD.PMS.Domain.Model.Periods;

namespace MITD.PMS.Domain.Model.JobIndices
{
    public interface IJobIndexRepository : IRepository
    {
        List<JobIndex> GetAllJobIndex(PeriodId periodId);
        List<JobIndexGroup> GetAllJobIndexGroup(PeriodId periodId);
        AbstractJobIndex GetById(AbstractJobIndexId jobIndexId);

        void Add(AbstractJobIndex jobIndex);
        void Delete(AbstractJobIndexId jobIndexId);

        JobIndex GetJobIndexById(AbstractJobIndexId id);
        JobIndexGroup GetJobIndexGroupById(AbstractJobIndexId id);


        IEnumerable<AbstractJobIndex> GetAll(PeriodId periodId);
        AbstractJobIndexId GetNextId();
        void Update(AbstractJobIndex jobIndex);

        IEnumerable<JobIndex> FindJobIndices(Expression<Func<JobIndex, bool>> where);
        List<AbstractJobIndex> GetAllAbstractJobIndexByParentId(AbstractJobIndexId id);
        List<JobIndexGroup> GetAllParentJobIndexGroup(Period period);
        List<SharedJobIndexId> GetSharedJobIndexIdBy(List<AbstractJobIndexId> abstractJobIndexIds);
        List<AbstractJobIndexId> GetJobIndexIdsBy(Period jobIndexIds, List<SharedJobIndexId> sharedJobIndexIds);

        Exception ConvertException(Exception exp);
        Exception TryConvertException(Exception exp);
    }
}
