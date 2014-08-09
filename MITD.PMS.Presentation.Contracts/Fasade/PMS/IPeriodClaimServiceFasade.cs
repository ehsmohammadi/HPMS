using System.Collections.Generic;
using MITD.Core;

namespace MITD.PMS.Presentation.Contracts
{
    public interface IPeriodClaimServiceFacade : IFacadeService
    {
        ClaimDTO AddClaim(long periodId, ClaimDTO claim);
        string DeleteClaim(long periodId, long claimId);
        ClaimDTO GetClaim(long periodId, long claimId);
        PageResultDTO<ClaimDTOWithAction> GetClaimsWithActions(long periodId, string employeeNo,
            int pageSize, int pageIndex, QueryStringConditions queryStringConditions);

        List<ClaimStateDTO> GetAllClaimStates(long periodId);
        ClaimDTO ChangeClaimState(long periodId, long id, string message, ClaimStateDTO claimStateDto);
        List<ClaimTypeDTO> GetAllClaimTypes(int periodId);
        PageResultDTO<ClaimDTOWithAction> GetAllClaimsForAdminWithActions(long periodId, int pageSize, int pageIndex, QueryStringConditions queryStringCondition);
    }
}
