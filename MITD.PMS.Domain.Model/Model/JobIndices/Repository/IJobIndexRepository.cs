﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MITD.Domain.Repository;
using MITD.PMS.Domain.Model.JobPositions;
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

        IEnumerable<JobIndex> FindJobIndices(IEnumerable<AbstractJobIndexId> jobIndexIds);
        List<AbstractJobIndex> GetAllAbstractJobIndexByParentId(AbstractJobIndexId id);
        List<JobIndexGroup> GetAllParentJobIndexGroup(Period period);
        SharedJobIndexId GetSharedJobIndexIdBy(AbstractJobIndexId abstractJobIndexId);
        AbstractJobIndexId GetJobIndexIdBy(Period jobIndexIds, SharedJobIndexId sharedJobIndexId);

        Exception ConvertException(Exception exp);
        Exception TryConvertException(Exception exp);
    }
}
