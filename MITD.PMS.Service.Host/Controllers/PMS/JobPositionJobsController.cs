using MITD.PMS.Presentation.Contracts;
using System.Collections.Generic;
using System.Web.Http;
using MITD.PMS.Presentation.Contracts.Fasade;

namespace MITD.PMS.Service.Host.Controllers
{
    [Authorize]
    public class JobPositionJobsController : ApiController
    {
        private readonly IPeriodJobServiceFacade jobService;


        public JobPositionJobsController(IPeriodJobServiceFacade jobService)
        {
            this.jobService = jobService;
        }

        public JobInPeriodDTO GetJobInPeriod(long periodId, long jobPositionId)
        {
            return jobService.GetJobByJobPositionId(periodId, jobPositionId);
        }
    }
}