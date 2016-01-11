using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MITD.PMSSecurity.Domain
{
    public class PartyCustomAction
    {
        public long Id { get; set; }

        public PartyId PartyId { get; set; }

        public virtual Party Party { get;private set; }

        public int ActionTypeId { get; set; }

        public virtual ActionType ActionType { get; set; }

        public bool IsGranted { get; set; }

        public PartyCustomAction(PartyId partyId, int actionTypeId, bool isGranted)
        {
            this.PartyId = partyId;
            this.ActionTypeId = actionTypeId;
            this.IsGranted = isGranted;
        }
    }
}
