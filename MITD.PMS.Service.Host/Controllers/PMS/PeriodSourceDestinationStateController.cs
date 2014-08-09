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
    public class PeriodSourceDestinationStateController : ApiController
    {
        private readonly IPeriodServiceFacade periodService;

        public PeriodSourceDestinationStateController(IPeriodServiceFacade periodService)
        {
            this.periodService = periodService;
        }

        //public PeriodStateWithRunSummaryDTO GetPeriodState(long periodId, long Id)
        //{
        //    return periodService.GetPeriodState(Id);
        //}

        public void PutPeriodState(long sourcePeriodId, long destinationPeriodId, PeriodStateDTO stateDto)
        {
            periodService.CopyBasicData(sourcePeriodId, destinationPeriodId, stateDto);
        }

    }
}