using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.Core;

namespace MITD.PMS.Presentation.Contracts.Fasade
{
    public interface IJobFacadeService : IFacadeService
    { 
        PageResultDTO<JobDTOWithActions> GetAllJobs(int pageSize, int pageIndex,
                                                     QueryStringConditions queryStringConditions);
        IList<JobDTO> GetAllJobs();

        JobDTO AddJob(JobDTO job);
        JobDTO UpdateJob(JobDTO job);
        string DeleteJob(long jobId);
        JobDTO GetJobById(long id);

    }
}
