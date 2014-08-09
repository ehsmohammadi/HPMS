using System.Collections.Generic;
using MITD.Core;

namespace MITD.PMS.Presentation.Contracts
{
    public interface IPeriodJobServiceFacade : IFacadeService
    {
        PageResultDTO<JobInPeriodDTOWithActions> GetAllJobs(long periodId,int pageSize, int pageIndex,
                                                             QueryStringConditions queryStringConditions, string selectedColumns);
        JobInPeriodDTO AssignJob(long periodId, JobInPeriodDTO jobInPeriod);
        void RemoveJob(long periodId, long jobId);
        JobInPeriodDTOWithActions GetJobWithActions(long periodId, long jobId, string selectedColumns);
        JobInPeriodDTO GetJob(long periodId, long jobId, string selectedColumns);
        List<JobInPeriodDTOWithActions> GetAllJobWithActions(long periodId, string selectedColumns);
        List<JobInPeriodDTO> GetAllJob(long periodId, string selectedColumns);
        JobInPeriodDTO UpdateJob(long periodId, JobInPeriodDTO jobInPeriod);
        JobInPeriodDTO GetJobByJobPositionId(long periodId, long jobPositionId);
    }
}
