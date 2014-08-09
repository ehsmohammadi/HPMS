using MITD.PMS.Exceptions;
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
    public class PeriodsStateController : ApiController
    {
        private readonly IPeriodServiceFacade periodService;

        public PeriodsStateController(IPeriodServiceFacade periodService)
        {
            this.periodService = periodService;
        }

        public PeriodStateWithIntializeInquirySummaryDTO GetPeriodState(long id)
        {
            return periodService.GetPeriodState(id);
        }

        public PeriodStateWithCopyingSummaryDTO GetPeriodState(long id, string sumaryType)
        {
             return periodService.GetPeriodBasicDataCopyState(id);
        }

        public void PutPeriodState(long id, PeriodStateDTO stateDto)
        {
            periodService.ChangePeriodState(id, stateDto);
        }

        public void PutPeriodState(long id, string dummy)
        {
            periodService.RollBackPeriodState(id);
        }

    }
}