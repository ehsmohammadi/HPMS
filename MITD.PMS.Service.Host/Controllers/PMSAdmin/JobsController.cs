using System;
using System.Collections.Generic;
using System.Web.Http;
using MITD.PMS.Presentation.Contracts.Fasade;
using MITD.PMS.Presentation.Contracts;

namespace MITD.Homework.Service.Host.Controllers.PMSAdmin
{
    [Authorize]
    public class JobsController : ApiController
    { 
        private readonly IJobFacadeService jobFasadeService;

        public JobsController(IJobFacadeService jobFasadeService )
        {
            this.jobFasadeService = jobFasadeService;
        }

        public PageResultDTO<JobDTOWithActions> GetAllJobes(int pageSize, int pageIndex, string filter = "", string sortBy = "")
        {
            var queryStringCondition = new QueryStringConditions { SortBy = sortBy, Filter = filter };
            return jobFasadeService.GetAllJobs(pageSize, pageIndex, queryStringCondition);
        }

        public IList<JobDTO> GetAllJobes()
        {
            return jobFasadeService.GetAllJobs();
        }


        public JobDTO GetJob(long id)
        {
            return jobFasadeService.GetJobById(id);
        }

        public JobDTO GetJobByTranferId(Guid transferId)
        {
            return jobFasadeService.GetJobByTransferId(transferId);
        }

        public JobDTO PostJob(JobDTO job)
        {
            return jobFasadeService.AddJob(job);
        }
        public JobDTO PutJob(JobDTO job)
        {
            return jobFasadeService.UpdateJob(job);
        }

        public string DeleteJob(long id)
        {
            return jobFasadeService.DeleteJob(id);
        }
       

    }
}
