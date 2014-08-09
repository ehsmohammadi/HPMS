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
    public class CalculationsExceptionsController : ApiController
    {
        private readonly ICalculationServiceFacade calculationService;

        public CalculationsExceptionsController(ICalculationServiceFacade calculationService)
        {
            this.calculationService = calculationService;
        }

        public CalculationExceptionDTO GetCalculationException(long periodId, long calculationId,long id)
        {
            return calculationService.GetCalculationException(id);
        }

        public PageResultDTO<CalculationExceptionBriefDTOWithAction> GetAllCalculationExceptions(long periodId, long calculationId, int pageSize, int pageIndex)
        {
            return calculationService.GetAllCalculationExceptions(calculationId, pageSize, pageIndex);
        }

    }
}