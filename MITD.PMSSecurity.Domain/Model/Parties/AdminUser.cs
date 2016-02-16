using System.Collections.Generic;

namespace MITD.PMSSecurity.Domain
{
    public class AdminUser : User
    {
        public override List<ActionType> Actions
        {
            get
            {
                return new List<ActionType>
                {
                    #region CustomFieldType

                    ActionType.ManageCustomFields,
                    //ActionType.CreateCustomField,
                    //ActionType.DeleteCustomField,
                    //ActionType.ModifyCustomField,

                    #endregion

                    #region JobIndex

                    ActionType.ManageJobIndices,
                    //ActionType.AddJobIndex,
                    //ActionType.ModifyJobIndex,
                    //ActionType.DeleteJobIndex,
                    ActionType.ManageJobIndexCustomFields,
                    ActionType.AddJobIndexCustomFields,
                    //ActionType.AddJobIndexCategory,
                    //ActionType.DeleteJobIndexCategory,
                    //ActionType.ModifyJobIndexCategory,

                    #endregion
                };
            }
        }

        public AdminUser(PartyId userId, string fName, string lName, string email)
            : base(userId, fName, lName, email)
        {

        }

    }
}