using MITD.PMS.Presentation.Contracts;
using System.Collections.Generic;
using System.Web.Http;

namespace MITD.PMS.Service.Host.Controllers
{
    public class PeriodClaimStatesController : ApiController
    {
        private readonly IPeriodClaimServiceFacade periodClaimService;

        public PeriodClaimStatesController(IPeriodClaimServiceFacade periodClaimService)
        {
            this.periodClaimService = periodClaimService;
        }
        

        public List<ClaimStateDTO> GetAllClaimState(int periodId)
        {
            return periodClaimService.GetAllClaimStates(periodId);
        }

       
    }
}