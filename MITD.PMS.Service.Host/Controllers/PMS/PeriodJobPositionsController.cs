using MITD.PMS.Presentation.Contracts;
using System.Collections.Generic;
using System.Web.Http;

namespace MITD.PMS.Service.Host.Controllers
{
    [Authorize]
    public class PeriodJobPositionsController : ApiController
    {
        private readonly IPeriodJobPositionServiceFacade periodJobPositionService;

        public PeriodJobPositionsController(IPeriodJobPositionServiceFacade periodJobPositionService)
        {
            this.periodJobPositionService = periodJobPositionService;
        }

        public JobPositionInPeriodAssignmentDTO PostJobPosition(long periodId, JobPositionInPeriodAssignmentDTO jobPositionInPeriod)
        {
            return periodJobPositionService.AssignJobPosition(periodId, jobPositionInPeriod);
        }

        public JobPositionInPeriodAssignmentDTO PutJobPosition(long periodId, JobPositionInPeriodAssignmentDTO jobPositionInPeriod)
        {
            return periodJobPositionService.UpdateJobPosition(periodId, jobPositionInPeriod);
        }

        public string DeleteJobPosition(long periodId, long jobPositionId)
        {
            return periodJobPositionService.RemoveJobPosition(periodId, jobPositionId);
        }

        public JobPositionInPeriodAssignmentDTO GetJobPosition(long periodId, long jobPositionId)
        {
            return periodJobPositionService.GetJobPosition(periodId, jobPositionId);
        }

        public IEnumerable<JobPositionInPeriodDTO> GetAllJobPositionInPeriods(long periodId, string type)
        {
            if (string.Equals(type, "JobPositionInPeriodDTOWithActions"))
            {
                return periodJobPositionService.GetJobPositionsWithActions(periodId);
            }
            return periodJobPositionService.GetJobPositions(periodId);
        }
    }
}