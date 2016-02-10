
using System;
using System.Collections.Generic;
using MITD.Core;


namespace MITD.PMSSecurity.Domain
{
    public sealed class SuperAdminUser:User
    {
        public override List<ActionType> Actions { get; set; }

        public SuperAdminUser(PartyId userId, string fName, string lName, string email)
            : base(userId, fName, lName, email)
        {
            Actions = new List<ActionType>();
            foreach (ActionType action in Enum.GetValues(typeof(ActionType)))
            {
                Actions.Add(action);
            }
        }
    }
}
