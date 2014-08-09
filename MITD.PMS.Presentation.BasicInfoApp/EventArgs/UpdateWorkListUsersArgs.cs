using System.Collections.Generic;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Presentation.BasicInfoApp
{
    public class UpdateWorkListUsersArgs
    {
        public UpdateWorkListUsersArgs(List<UserDescriptionDTO> usernameList, string partyName)
        {
            WorkListUsers = usernameList;
            PartyName = partyName;
        }

        public List<UserDescriptionDTO> WorkListUsers
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
