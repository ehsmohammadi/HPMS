using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MITD.PMS.Domain.Model.Claims
{
    public class ClaimRejectedState : ClaimState
    {
        public ClaimRejectedState()
            : base("4", "Rejected")
        {
        }
    }
}
