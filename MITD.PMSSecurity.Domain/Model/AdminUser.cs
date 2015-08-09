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
                    // BasicInfo mgmt Actions || PMSAdmin Domain Action 

                    #region Policy
		            ActionType.AddPolicy,
                    ActionType.DeletePolicy,
                    ActionType.ModifyPolicy,

                    ActionType.ManageRules,
                    ActionType.ManageFunctions,

                    ActionType.AddFunction,
                    ActionType.DeleteFunction,
                    ActionType.ModifyFunction,

                    ActionType.AddRule,
                    ActionType.DeleteRule,
                    ActionType.ModifyRule,
                    ActionType.ShowRuleTrail,
                    ActionType.ShowAllRuleTrails,
	                #endregion

                    #region CustomField
		            ActionType.AddCustomField,
                    ActionType.DeleteCustomField,
                    ActionType.ModifyCustomField, 
	                #endregion

                    #region Job
		            ActionType.AddJob,
                    ActionType.DeleteJob,
                    ActionType.ModifyJob,
                    ActionType.ManageJobCustomFields, 
	                #endregion

                    #region JobIndex
		            ActionType.AddJobIndex,
                    ActionType.ModifyJobIndex,
                    ActionType.DeleteJobIndex,
                    ActionType.ManageJobIndexCustomFields,


                    ActionType.AddJobIndexCategory,
                    ActionType.DeleteJobIndexCategory,
                    ActionType.ModifyJobIndexCategory, 
	                #endregion

                    #region Unit
		            ActionType.AddUnit,
                    ActionType.DeleteUnit,
                    ActionType.ModifyUnit,
                    ActionType.ManageUnitCustomFields, 
	                #endregion

                    ActionType.ManageUnitCustomFields,
                    //ActionType.AddUnitCustomFields,
                    //ActionType.AddUnitIndex,
                    //ActionType.AddUnitIndexCategory,
                    //ActionType.AddUnitIndexCustomFields,
                    //ActionType.ModifyUnitIndex,
                    //ActionType.ModifyUnitIndexCategory,

                    #region JobPosition
		            ActionType.AddJobPosition,
                    ActionType.DeleteJobPosition,
                    ActionType.ModifyJobPosition, 
	                #endregion

                    #region User
                    ActionType.AddPermittedUserToMyTasks,
                    ActionType.RemovePermittedUserFromMyTasks,
                    ActionType.SettingPermittedUserToMyTasks,

                    ActionType.AddUser,
                    ActionType.DeleteUser,
                    ActionType.ModifyUser,
                    ActionType.ManageUserCustomActions,
                    ActionType.ManageUserWorkListUsers,

                    ActionType.AddUserGroup,
                    ActionType.DeleteUserGroup,
                    ActionType.ModifyUserGroup,
                    ActionType.ManageGroupCustomActions, 
	                #endregion

                    #region Log
		            ActionType.ShowLog,
                    ActionType.DeleteLog, 
                    #endregion

                    // PMS mgmt Actions || PMS Domain Action

                    #region Period
		            ActionType.AddPeriod,
                    ActionType.ModifyPeriod,
                    ActionType.DeletePeriod,
                    ActionType.ManageUnits,
                    ActionType.ManageUnitIndices,
                    ActionType.ManageJobPositions,
                    ActionType.ManageJobIndices,
                    ActionType.ManageJobs,
                    ActionType.ManageCalculations,
                    ActionType.ManageEmployees,
                    ActionType.ActivatePeriod,
                    ActionType.InitializePeriodForInquiry,
                    ActionType.StartInquiry,
                    ActionType.CompleteInquiry,
                    ActionType.StartCliaming,
                    ActionType.FinishCliaming,
                    ActionType.ClosePeriod,
                    ActionType.CopyPeriodBasicData,
                    ActionType.GetPeriodInitializingInquiryStatus,
                    ActionType.RollBackPeriodState, 
	                #endregion
                    
                    #region JobInPeriod
		            ActionType.AddJobInPeriod,
                    ActionType.ModifyJobInPeriod,
                    ActionType.DeleteJobInPeriod, 
	                #endregion

                    #region JobIndexInPeriod
		            ActionType.AddJobIndexInPeriod,
                    ActionType.ModifyJobIndexInPeriod,
                    ActionType.DeleteJobIndexInPeriod,
                    ActionType.AddJobIndexGroupInPeriod,
                    ActionType.ModifyJobIndexGroupInPeriod,
                    ActionType.DeleteJobIndexGroupInPeriod, 
	                #endregion

                    #region UnitInPeriod
		            ActionType.AddUnitInPeriod,
                    ActionType.ModifyUnitInPeriod,
                    ActionType.DeleteUnitInPeriod, 
                    ActionType.ManageUnitInPeriodInquiry,
	                #endregion

                    #region UnitIndexInPeriod
		            ActionType.AddUnitIndexInPeriod,
                    ActionType.ModifyUnitIndexInPeriod,
                    ActionType.DeleteUnitIndexInPeriod,
                    ActionType.AddUnitIndexGroupInPeriod,
                    ActionType.ModifyUnitIndexGroupInPeriod,
                    ActionType.DeleteUnitIndexGroupInPeriod, 
	                #endregion

                    #region JobPositionInPeriod
		            ActionType.AddJobPositionInPeriod,
                    ActionType.DeleteJobPositionInPeriod,
                    ActionType.ManageJobPositionInPeriodInquiry, 
	                #endregion



                    #region Employee
		            ActionType.AddEmployee,
                    ActionType.ModifyEmployee,
                    ActionType.DeleteEmployee,
                    ActionType.ManageEmployeeJobPositions,
                    ActionType.AddEmployeeJobCustomFields,
                    ActionType.ModifyEmployeeJobCustomFields, 
	                #endregion

                    //---------------------------------------------

                    #region Calculation
		            ActionType.AddCalculation,
                    ActionType.DeleteCalculation,
                    ActionType.ModifyCalculation,

                    ActionType.ShowCalculationState,
                    ActionType.SetDeterministicCalculation,
                    ActionType.UnsetDeterministicCalculation,
                    ActionType.ShowCalculationResult,

                    ActionType.ShowAllCalculationException,
                    ActionType.ShowCalculationException,
                    //ActionType.ShowCalculationResultDetail,

                    ActionType.RunCalculation,
                    ActionType.StopCalculation, 
	                #endregion

                    #region Inquiry
		            //bayad bardashte shee
                    ActionType.FillInquiryForm,
                    ActionType.DeleteCustomInquirer, 
	                #endregion

                    #region Claim
		            ActionType.ShowClaim,
                    ActionType.ReplyToClaim,
                    ActionType.ShowAdminClaimList, 
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