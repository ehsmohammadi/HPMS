using MITD.PMS.Presentation.Contracts;
using System.Collections.Generic;
using System.Web.Http;

namespace MITD.PMS.Service.Host.Controllers
{
    public class PeriodClaimTypesController : ApiController
    {
        private readonly IPeriodClaimServiceFacade periodClaimService;

        public PeriodClaimTypesController(IPeriodClaimServiceFacade periodClaimService)
        {
            this.periodClaimService = periodClaimService;
        }
        

        public List<ClaimTypeDTO> GetAllClaimTypes(int periodId)
        {
            return periodClaimService.GetAllClaimTypes(periodId);
        }

       
    }
}