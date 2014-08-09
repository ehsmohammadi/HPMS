using System;
using System.Transactions;
using MITD.Core;
using MITD.PMS.Application.Contracts;
using MITD.PMS.Common;
using MITD.PMS.Domain.Model.Claims;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Domain.Service;
using MITD.PMS.Exceptions;
using MITD.PMSSecurity.Application.Contracts;

namespace MITD.PMS.Application
{
    public class ClaimService : IClaimService
    {

        private readonly IClaimRepository claimRep;
        private readonly IPeriodRepository periodRep;
        private readonly IPMSSecurityService securityService;

        public ClaimService(IClaimRepository claimRep, IPeriodRepository periodRep, IPMSSecurityService securityService)
        {
            this.claimRep = claimRep;
            this.periodRep = periodRep;
            this.securityService = securityService;
        }

        public Claim AddClaim(PeriodId periodId, string employeeNo, string title, DateTime requestDate, ClaimTypeEnum claimTypeId, string request)
        {
            try
            {
                using (var tr = new TransactionScope())
                {
                    var id = claimRep.GetNextId();
                    var period = periodRep.GetById(periodId);
                    var claim = new Claim(id, period, employeeNo, title, requestDate, claimTypeId, request);
                    claimRep.Add(claim);
                    tr.Complete();
                    return claim;
                }
            }
            catch (Exception exp)
            {
                var res = claimRep.TryConvertException(exp);
                if (res == null)
                    throw;
                throw res;
            }

        }



        public Claim AcceptClaim(ClaimId claimId, DateTime responseDate, string response)
        {
            using (var tr = new TransactionScope())
            {
                var claim = claimRep.GetById(claimId);
                var period = periodRep.GetById(claim.PeriodId);
                claim.Accept(responseDate, response, period);
                tr.Complete();
                return claim;
            }

        }

        public Claim RejectClaim(ClaimId claimId, DateTime responseDate, string response)
        {
            using (var tr = new TransactionScope())
            {
                var claim = claimRep.GetById(claimId);
                var period = periodRep.GetById(claim.PeriodId);
                claim.Reject(responseDate, response, period);
                tr.Complete();
                return claim;
            }

        }

        public Claim CancelClaim(ClaimId claimId)
        {
            using (var tr = new TransactionScope())
            {
                var claim = claimRep.GetById(claimId);
                var period = periodRep.GetById(claim.PeriodId);
                claim.Cancel(period);
                tr.Complete();
                return claim;
            }

        }

        public void DeleteClaim(ClaimId claimId)
        {
            try
            {
                using (var tr = new TransactionScope())
                {
                    var claim = claimRep.GetById(claimId);
                    var period = periodRep.GetById(claim.PeriodId);
                    var employee = securityService.GetCurrentLoginEmployee(period);
                    ClaimControlService.CheckDeleteClaim(claim, employee);
                    claimRep.Delete(claim);
                    tr.Complete();
                }
            }
            catch (Exception exp)
            {
                var res = claimRep.TryConvertException(exp);
                if (res == null)
                    throw;
                throw res;
            }
        }

        public Claim ChangeClaimState(PeriodId periodId, ClaimId claimId, string message, ClaimState claimState)
        {
            if (claimState == ClaimState.Accepted)
                return AcceptClaim(claimId, DateTime.Now, message);

            if (claimState == ClaimState.Rejected)
                return RejectClaim(claimId, DateTime.Now, message);

            if (claimState == ClaimState.Canceled)
                return CancelClaim(claimId);

            throw new ClaimException((int)ApiExceptionCode.InvalidClaimState, ApiExceptionCode.InvalidClaimState.DisplayName);
        }
    }
}
