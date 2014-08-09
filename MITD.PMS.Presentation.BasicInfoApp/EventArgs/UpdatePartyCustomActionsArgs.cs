using System.Collections.Generic;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Presentation.BasicInfoApp
{
    public class UpdatePartyCustomActionsArgs
    {
        public UpdatePartyCustomActionsArgs(Dictionary<int, bool> customActions, string partyName)
        {
            CustomActions = customActions;
            PartyName = partyName;
        }

        public Dictionary<int, bool> CustomActions
        {
            get;
            private set;
        }

        public string PartyName
        {
            get;
            private set;
        }
    }

    
}
