using MITD.PMS.Presentation.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MITD.PMS.Service.Host.Controllers
{
    [Authorize]
    public class CalculationsStateController : ApiController
    {
        private readonly ICalculationServiceFacade calculationService;

        public CalculationsStateController(ICalculationServiceFacade calculationService)
        {
            this.calculationService = calculationService;
        }

        public CalculationStateWithRunSummaryDTO GetCalculationState(long periodId, long Id)
        {
            return calculationService.GetCalculationState(Id);
        }

        public void PutCalculationState(long periodId, long Id, CalculationStateDTO stateDto)
        {
            calculationService.ChangeCalculationState(periodId, Id, stateDto);
        }

    }
}