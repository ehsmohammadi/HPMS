using MITD.PMS.Presentation.Contracts;
using System.Collections.Generic;
using System.Web.Http;

namespace MITD.PMS.Service.Host.Controllers
{
    [Authorize]
    public class PeriodClaimsController : ApiController
    {
        private readonly IPeriodClaimServiceFacade periodClaimService;

        public PeriodClaimsController(IPeriodClaimServiceFacade periodClaimService)
        {
            this.periodClaimService = periodClaimService;
        }

        

        public ClaimDTO PostClaim(long periodId, ClaimDTO claim)
        {
            return periodClaimService.AddClaim(periodId, claim);
        }

       

        public ClaimDTO PutClaimState(long periodId, long id, string message, ClaimStateDTO claimStateDTO)
        {
            return periodClaimService.ChangeClaimState(periodId, id, message, claimStateDTO);
        }

        public string DeleteClaim(long periodId, long id)
        {
            return periodClaimService.DeleteClaim(periodId, id);
        }

        public ClaimDTO GetClaim(long periodId, long id)
        {
            return periodClaimService.GetClaim(periodId, id);
        }

        public PageResultDTO<ClaimDTOWithAction> GetAllClaims(long periodId, string employeeNo, int pageSize, int pageIndex,
                                                      string filter = "", string sortBy = "")
        {
            var queryStringCondition = new QueryStringConditions {SortBy = sortBy, Filter = filter};
            return periodClaimService.GetClaimsWithActions(periodId, employeeNo,pageSize,pageIndex,queryStringCondition);
        }

        public PageResultDTO<ClaimDTOWithAction> GetAllClaims(long periodId, int pageSize, int pageIndex,
                                                      string filter = "", string sortBy = "")
        {
            var queryStringCondition = new QueryStringConditions { SortBy = sortBy, Filter = filter };
            return periodClaimService.GetAllClaimsForAdminWithActions(periodId, pageSize, pageIndex, queryStringCondition);
        }
    }
}