
using System.Collections.Generic;
using MITD.Core;


namespace MITD.PMSSecurity.Domain.Model
{
    public sealed class SuperAdminUser:User
    {
        public override List<ActionType> Actions { get; set; }

        public SuperAdminUser(PartyId userId, string fName, string lName, string email)
            : base(userId, fName, lName, email)
        {
            foreach (var action in Enumeration.GetAll<ActionType>())
            {
                Actions.Add(action);
            }
        }
    }
}
