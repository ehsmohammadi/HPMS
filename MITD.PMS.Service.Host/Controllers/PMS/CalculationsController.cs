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
    public class CalculationsController : ApiController
    {
        private readonly ICalculationServiceFacade calculationService;

        public CalculationsController(ICalculationServiceFacade calculationService)
        {
            this.calculationService = calculationService;
        }
        public CalculationDTO PostCalculation(CalculationDTO calculation)
        {
            return calculationService.AddCalculation(calculation);
        }
        public PageResultDTO<CalculationBriefDTOWithAction> GetAllCalculations(long periodId,int pageSize, int pageIndex)
        {
            return calculationService.GetAllCalculations(periodId,pageSize, pageIndex);
        }
        public CalculationDTO GetCalculation(long periodId, long id)
        {
            return calculationService.GetCalculation(id);
        }

        public CalculationDTO GetDeterministicCalculation(long periodId)
        {
            return calculationService.GetDeterministicCalculation(periodId);
        }

        public CalculationDTO PutCalculation(CalculationDTO calculation)
        {
            if (calculation.PutActionName.ToLower() == "changecalculationdeterministicstatus")
                calculationService.ChangeDeterministicStatus(calculation.Id,calculation.IsDeterministic);
            return calculation;

        }

        public void DeleteCalculation(long periodId, long id)
        {
            calculationService.DeleteCalculation(id);
        }
    }
}