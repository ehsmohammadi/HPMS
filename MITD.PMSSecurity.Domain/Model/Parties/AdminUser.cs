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
                    #region PMSAdmin

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

                    #region Unit 

                    ActionType.ManageUnits,
                    //ActionType.AddUnit,
                    //ActionType.ModifyUnit,
                    //ActionType.DeleteUnit,
                    //ActionType.ManageUnitCustomFields,
                    //ActionType.AddUnitCustomFields,

                    #endregion

                    #region JobPosition 

                    ActionType.ManageJobPositions,
                    //ActionType.AddJobPosition,
                    //ActionType.ModifyJobPosition,
                    //ActionType.DeleteJobPosition,

                    #endregion

                    #region Policy 
                    
                    ActionType.ManagePolicies,//for calculation -> manage calculation use it
                    //ActionType.AddPolicy,
                    //ActionType.ModifyPolicy,
                    //ActionType.DeletePolicy,

                    #endregion

                    #region Users

                    //ActionType.AddPermittedUserToMyTasks,
                    //ActionType.RemovePermittedUserFromMyTasks,
                    //ActionType.SettingPermittedUserToMyTasks,
                    //ActionType.AddUser,
                    //ActionType.ModifyUser,
                    //ActionType.DeleteUser,
                    //ActionType.ManageUserCustomActions,
                    //ActionType.ManageUserWorkListUsers,
                    //ActionType.ShowUser,
                    //ActionType.AddUserGroup,
                    //ActionType.ModifyUserGroup,
                    //ActionType.DeleteUserGroup,
                    //ActionType.ManageGroupCustomActions,
                    //ActionType.ShowUserGroup,

                    #endregion

                    #region Log

                    ActionType.ShowLog,
                    ActionType.DeleteLog,

                    #endregion 

                    #endregion

                    #region Employee 7 
                    
                    ActionType.ManageEmployees,//for calculation -> manage calculation use it 
                    //ActionType.AddEmployee,
                    //ActionType.ModifyEmployee,
                    //ActionType.DeleteEmployee,
                    //ActionType.AddEmployeeJobCustomFields,
                    //ActionType.ModifyEmployeeJobCustomFields,
                    ActionType.ManageEmployeeJobPositions,
                    //ActionType.ShowEmployeeInquiry,

                    #endregion

                    #region Calculation 8
                    
                    ActionType.ManageCalculations,
                    ActionType.AddCalculation,
                    ActionType.ModifyCalculation,
                    ActionType.DeleteCalculation,
                    ActionType.RunCalculation,
                    ActionType.StopCalculation,
                    ActionType.SetDeterministicCalculation,
                    ActionType.UnsetDeterministicCalculation,
                    ActionType.ShowCalculationState,
                    ActionType.ShowCalculationResult,
                    ActionType.ShowCalculationException,
                    ActionType.ShowAllCalculationException,

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