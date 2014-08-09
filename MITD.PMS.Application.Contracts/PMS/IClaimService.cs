using System;
using MITD.Core;
using MITD.PMS.Domain.Model.Claims;
using MITD.PMS.Domain.Model.Periods;

namespace MITD.PMS.Application.Contracts
{
    public interface IClaimService:IService
    { 
        
        Claim AddClaim(PeriodId periodId, string employeeNo, string title, DateTime requestDate, ClaimTypeEnum claimTypeId, string request);
        void DeleteClaim(ClaimId claimId);
        Claim AcceptClaim(ClaimId claimId, DateTime responseDate, string response);
        Claim RejectClaim(ClaimId claimId, DateTime responseDate, string response);
        Claim CancelClaim(ClaimId claimId);
        Claim ChangeClaimState(PeriodId periodId, ClaimId id, string message, ClaimState claimState);
    }
}
