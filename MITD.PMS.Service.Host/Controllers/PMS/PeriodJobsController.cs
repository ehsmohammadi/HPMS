using MITD.PMS.Presentation.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MITD.PMS.Presentation.Contracts.Fasade;

namespace MITD.PMS.Service.Host.Controllers
{
    [Authorize]
    public class PeriodJobsController : ApiController
    {
        private const string jobInPeriodWithActionsClassType = "JobInPeriodWithActions";
        private const string jobInPeriodClassType = "JobInPeriod";

        private IPeriodJobServiceFacade periodJobService;

        public PeriodJobsController(IPeriodJobServiceFacade periodJobService)
        {
            this.periodJobService = periodJobService;
        }

       
        public PageResultDTO<JobInPeriodDTOWithActions> GetAllJobes(long periodId, int pageSize, int pageIndex,
                                                             string filter = "", string sortBy = "", string selectedColumns ="")
        {
            var queryStringCondition = new QueryStringConditions { SortBy = sortBy, Filter = filter };
            return periodJobService.GetAllJobs(periodId,pageSize, pageIndex, queryStringCondition, selectedColumns);
        }

        public JobInPeriodDTO PutJob(long periodId, JobInPeriodDTO jobInPeriod)
        {
            //update customFields
            return periodJobService.UpdateJob(periodId, jobInPeriod);
        }

        public JobInPeriodDTO PostJob(long periodId, JobInPeriodDTO jobInPeriod)
        {
            return periodJobService.AssignJob(periodId, jobInPeriod);
        }

        public void DeleteJob(long periodId, long jobId)
        {
            periodJobService.RemoveJob(periodId, jobId);
        }

        public JobInPeriodDTO GetJob(long periodId, long jobId, string classType = jobInPeriodClassType, string selectedColumns = "")
        {
            if (classType.ToLower() == jobInPeriodWithActionsClassType.ToLower())
                return periodJobService.GetJobWithActions(periodId, jobId, selectedColumns);
            return periodJobService.GetJob(periodId, jobId, selectedColumns);
        }

        public IEnumerable<JobInPeriodDTO> GetAllJobInPeriods(long periodId, string classType = jobInPeriodClassType, string selectedColumns ="")
        {
            if (classType.ToLower() == jobInPeriodWithActionsClassType.ToLower())
                return periodJobService.GetAllJobWithActions(periodId, selectedColumns);
            return periodJobService.GetAllJob(periodId, selectedColumns);
        }

       

       
    }
}