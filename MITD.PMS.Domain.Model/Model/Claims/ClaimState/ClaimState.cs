using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.Core;
using MITD.Domain.Model;
using MITD.PMS.Exceptions;


namespace MITD.PMS.Domain.Model.Claims
{
    public class ClaimState : Enumeration, IValueObject<ClaimState>, IClaimState
    {
        public static readonly ClaimState Opened = new ClaimOpenedState();
        public static readonly ClaimState Accepted = new ClaimAcceptedState();
        public static readonly ClaimState Rejected = new ClaimRejectedState();
        public static readonly ClaimState Canceled = new ClaimCanceledState();

        protected ClaimState(string value, string name)
            : base(value, name)
        {
        }

        public bool SameValueAs(ClaimState other)
        {
            return Equals(other);
        }
        public static bool operator ==(ClaimState left, ClaimState right)
        {
            return object.Equals(left, right);
        }
        public static bool operator !=(ClaimState left, ClaimState right)
        {
            return !(left == right);
        }

        internal virtual void OpenClaim(Claim claim)
        {
            throw new ClaimInvalidStateOperationException("Claim", DisplayName, "OpenClaim");
        }

        internal virtual void RejectClaim(Claim claim)
        {
            throw new ClaimInvalidStateOperationException("Claim", DisplayName, "RejectClaim");
        }

        internal virtual void AcceptClaim(Claim claim)
        {
            throw new ClaimInvalidStateOperationException("Claim", DisplayName, "AcceptClaim");
        }

        internal virtual void CancelClaim(Claim claim)
        {
            throw new ClaimInvalidStateOperationException("Claim", DisplayName, "CancelClaim");
        }
    }
}
