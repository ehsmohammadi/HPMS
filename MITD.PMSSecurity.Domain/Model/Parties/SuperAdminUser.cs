
using System;
using System.Collections.Generic;

namespace MITD.PMSSecurity.Domain.Model
{
    public sealed class SuperAdminUser:User
    {
        public override List<ActionType> Actions { get; set; }

        public SuperAdminUser(PartyId userId, string fName, string lName, string email)
            : base(userId, fName, lName, email)
        {
            foreach (ActionType action in Enum.GetValues(typeof(ActionType)))
            {
                Actions.Add(action);
            }
        }
    }
}
