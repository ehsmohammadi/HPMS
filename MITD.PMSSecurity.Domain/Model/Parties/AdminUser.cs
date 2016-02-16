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
                    //ActionType.ManageJobIndexCustomFields,
                    //ActionType.AddJobIndexCustomFields,
                    //ActionType.AddJobIndexCategory,
                    //ActionType.DeleteJobIndexCategory,
                    //ActionType.ModifyJobIndexCategory,

                    #endregion

                    #region Job
       
                    ActionType.ManageJobs,
                    //ActionType.CreateJob,
                    //ActionType.ModifyJob,
                    //ActionType.DeleteJob,
                    //ActionType.ManageJobCustomFields,
                    //ActionType.AssignJobCustomFields,

                    #endregion

                    #region UnitIndex
      
                    ActionType.ManageUnitIndices,
                    //ActionType.AddUnitIndex,
                    //ActionType.ModifyUnitIndex,
                    //ActionType.DeleteUnitIndex,
                    //ActionType.ManageUnitIndexCustomFields,
                    //ActionType.AddUnitIndexCustomFields,
                    //ActionType.AddUnitIndexCategory,
                    //ActionType.DeleteUnitIndexCategory,
                    //ActionType.ModifyUnitIndexCategory,

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