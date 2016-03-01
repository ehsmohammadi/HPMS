using System;
using System.Collections.Generic;
using System.Web.Http;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Service.Host.Controllers
{
    [Authorize]
    public class JobIndexPointsController : ApiController
    {
        private readonly IJobIndexPointFacadeService jobIndexPointService;

        public JobIndexPointsController(IJobIndexPointFacadeService jobIndexPointService)
        {
            this.jobIndexPointService = jobIndexPointService;
        }

        public PageResultDTO<JobIndexPointSummaryDTOWithAction> GetAllJobIndexPoints(long periodId, long calculationId, int pageSize, int pageIndex)
        {
            return jobIndexPointService.GetAllJobIndexPoints(periodId,calculationId,pageSize,pageIndex);
        }

        public JobIndexPointSummaryDTOWithAction GetEmployeeSummaryCalculationResult(long periodId, long calculationId, string employeeNo, string resultType)
        {
            if (resultType.Trim().ToLower() != "summary")
                throw new Exception("ResultType is not valid , resultType can be 'summary' ");
            return jobIndexPointService.GetEmployeeSummaryCalculationResult(periodId, calculationId, employeeNo);
        }

        public List<JobPositionValueDTO> GetEmployeeJobPositionsCalculationResult(long periodId, long calculationId, string employeeNo)
        {
            return jobIndexPointService.GetEmployeeJobPositionsCalculationResult(periodId, calculationId, employeeNo);
        }


    }
}