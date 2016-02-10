using System.Collections.Generic;
using MITD.Core;

namespace MITD.PMS.Presentation.Contracts
{
    public interface IJobFacadeService : IFacadeService
    { 
        PageResultDTO<JobDTOWithActions> GetAllJobs(int pageSize, int pageIndex,QueryStringConditions queryStringConditions);
        IList<JobDTO> GetAllJobs();

        JobDTO AddJob(JobDTO job);
        JobDTO UpdateJob(JobDTO job);
        string DeleteJob(long jobId);
        JobDTO GetJobById(long id);

    }
}
