using System;
using System.Collections.Generic;
using MITD.Domain.Repository;
using MITD.PMS.Domain.Model.Periods;

namespace MITD.PMS.Domain.Model.Jobs
{
    public interface IJobRepository : IRepository
    {
        void GetAllJob(ListFetchStrategy<Job> fs);
        List<Job> GetAllJob(PeriodId periodId);
        Job GetById(JobId jobId);

        void Add(Job job);
        void DeleteJob(Job job);

        List<JobId> GetAllJobIdList(PeriodId periodId);
        JobId GetJobIdBy(Period currentPeriod, SharedJobId sharedJobId);

        Exception ConvertException(Exception exp);
        Exception TryConvertException(Exception exp);
    }
}
