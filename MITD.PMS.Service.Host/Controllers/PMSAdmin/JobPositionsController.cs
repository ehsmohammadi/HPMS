using System.Collections.Generic;
using MITD.PMS.Presentation.Contracts;
using System.Web.Http;

namespace MITD.PMS.Service.Host.Controllers
{
    [Authorize]
    public class JobPositionsController : ApiController
    {
        private readonly IJobPositionFacadeService jobPositionService;

        public JobPositionsController(IJobPositionFacadeService jobPositionService)
        {
            this.jobPositionService = jobPositionService;
        }
         
        public PageResultDTO<JobPositionDTOWithActions> GetAllJobPositions(int pageSize, int pageIndex,string filter="",string sortBy="")
        {
            var queryStringCondition = new QueryStringConditions { SortBy = sortBy, Filter = filter };
            return jobPositionService.GetAllJobPositions(pageSize, pageIndex, queryStringCondition);
        }

        public List<JobPositionDTO> GetAllJobPositions()
        {
           
            return jobPositionService.GetAllJobPositions();
        }


        public JobPositionDTO PostJobPosition(JobPositionDTO jobPosition)
        {
            return jobPositionService.AddJobPosition(jobPosition);
        }
        public JobPositionDTO PutJobPosition(JobPositionDTO jobPosition)
        {
            return jobPositionService.UpdateJobPosition(jobPosition);
        }

        public JobPositionDTO GetJobPosition(long id)
        {
            return jobPositionService.GetJobPositionById(id);
        }

        public string DeleteJobPostion(long id)
        {
            return jobPositionService.DeleteJob(id);
        }
    }
}