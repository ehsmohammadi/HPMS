using MITD.PMS.Presentation.Contracts;
using System.Collections.Generic;
using System.Web.Http;
using MITD.PMS.Presentation.Contracts.Fasade;

namespace MITD.PMS.Service.Host.Controllers
{
    [Authorize]
    public class PeriodsController : ApiController
    {
        private readonly IPeriodServiceFacade periodService;

        public PeriodsController(IPeriodServiceFacade periodService)
        {
            this.periodService = periodService;
        }

        public PageResultDTO<PeriodDTOWithAction> GetAllPeriods(int pageSize, int pageIndex)
        {
            return periodService.GetAllPeriods(pageSize, pageIndex);
        }

        public List<PeriodDescriptionDTO> GetAllPeriods()
        {
            return periodService.GetAllPeriods();
        }

        public List<PeriodDescriptionDTO> GetPeriodsWithConfirmedResult(int hasConfirmedResult)
        {
            return periodService.GetPeriodsWithConfirmedResult();
        }

        public EmployeeResultDTO GetEmployeeResultInPeriod(long periodId,string employeeNo)
        {
            return periodService.GetEmployeeResultInPeriod(periodId, employeeNo);
        }

        public string DeletePeriod(long id)
        {
            return periodService.DeletePeriod(id);
        }

        public PeriodDTO GetPeriod(long id)
        {
            return periodService.GetPeriod(id);
        }

        public PeriodDTO PostPeriod(PeriodDTO period)
        {
            return periodService.AddPeriod(period);
        }

        public PeriodDTO PutPeriod(PeriodDTO period)
        {
            if (!string.IsNullOrEmpty(period.PutActionName) && period.PutActionName.ToLower() == "changeactivestatus")
            {
                periodService.ChangePeriodActiveStatus(period.Id, period.ActiveStatus);
                return period;
            }
            else
                return periodService.UpdatePeriod(period);
        }



        public PeriodDTO GetCurrentPeriod(int active)
        {
            return periodService.GetCurrentPeriod();
        }
    }
}