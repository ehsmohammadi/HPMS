
using System.Collections.Generic;
using MITD.Core;
using MITD.PMS.Domain.Model.JobIndices;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Domain.Model.Jobs;
using MITD.PMS.Domain.Model.Units;

namespace MITD.PMS.Application.Contracts
{
    public interface IJobService : IService
    {
        Job AssignJob(JobId jobId, List<SharedJobCustomFieldId> customFieldIdList, IList<AbstractJobIndexId> jobIndexIdList);
        void RemoveJob(JobId jobId);
        Job UpdateJob(JobId jobId, List<SharedJobCustomFieldId> customFieldIdList, IList<AbstractJobIndexId> jobIndexIdList);
        Job GetJobById(JobId jobId);
        List<JobId> GetAllJobIdList(PeriodId periodId);
        JobId GetJobIdBy(Period currentPeriod, SharedJobId sharedJobId);
    }
}
