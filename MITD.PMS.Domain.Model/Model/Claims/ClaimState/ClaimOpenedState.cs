using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MITD.PMS.Domain.Model.Claims
{
    public class ClaimOpenedState : ClaimState
    {
        public ClaimOpenedState()
            : base("1", "Opened")
        {
        }

        internal override void AcceptClaim(Claim claim)
        {
            claim.State = new ClaimAcceptedState();
        }

        internal override void RejectClaim(Claim claim)
        {
            claim.State = new ClaimRejectedState();
        }

        internal override void CancelClaim(Claim claim)
        {
            claim.State = new ClaimCanceledState();
        }
    }
}
