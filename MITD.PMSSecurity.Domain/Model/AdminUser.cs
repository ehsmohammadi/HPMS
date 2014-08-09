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
                    ActionType.AddPeriod,
                    ActionType.ModifyPeriod,
                    ActionType.DeletePeriod,
                    ActionType.ManageUnits,
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
                    
                    ActionType.AddJobInPeriod,
                    ActionType.ModifyJobInPeriod,
                    ActionType.DeleteJobInPeriod,
                   // ActionType.ManageJobInPeriodCustomFields,

                    ActionType.AddUnitInPeriod,
                    ActionType.DeleteUnitInPeriod,

                    ActionType.AddJobPositionInPeriod,
                    ActionType.DeleteJobPositionInPeriod,
                    ActionType.ManageJobPositionInPeriodInquiry,

                    ActionType.AddJobIndexInPeriod,
                    ActionType.ModifyJobIndexInPeriod,
                    ActionType.DeleteJobIndexInPeriod,
                    ActionType.AddJobIndexGroupInPeriod,
                    ActionType.ModifyJobIndexGroupInPeriod,
                    ActionType.DeleteJobIndexGroupInPeriod,

                    ActionType.AddEmployee,
                    ActionType.ModifyEmployee,
                    ActionType.DeleteEmployee,
                    //ActionType AddEmployeeJobPositions = new ActionType("1", "AddPeriod");
                    ActionType.ManageEmployeeJobPositions,
                    //ActionType DeleteEmployeeJobPositions = new ActionType("3", "Employee"); 
                    ActionType.AddEmployeeJobCustomFields,
                    ActionType.ModifyEmployeeJobCustomFields,
                    //ActionType.GetEmployeeJobPositions,
                    //ActionType DeleteEmployeeJobCustomFields = new ActionType("3", "Employee");

                    ActionType.AddJobIndex,
                    ActionType.ModifyJobIndex,
                    ActionType.DeleteJobIndex,
                    ActionType.ManageJobIndexCustomFields,
                    //ActionType AddJobIndexCustomFields = new ActionType("3", "Employee");
                    //ActionType ModifyJobIndexCustomFields = new ActionType("3", "Employee");

                    ActionType.AddJobIndexCategory,
                    ActionType.DeleteJobIndexCategory,
                    ActionType.ModifyJobIndexCategory,

                    ActionType.AddJobPosition,
                    ActionType.DeleteJobPosition,
                    ActionType.ModifyJobPosition,

                    ActionType.AddFunction,
                    ActionType.DeleteFunction,
                    ActionType.ModifyFunction,

                    ActionType.AddCustomField,
                    ActionType.DeleteCustomField,
                    ActionType.ModifyCustomField,

                    ActionType.AddJob,
                    ActionType.DeleteJob,
                    ActionType.ModifyJob,
                    ActionType.ManageJobCustomFields,

                    ActionType.AddUnit,
                    ActionType.DeleteUnit,
                    ActionType.ModifyUnit,

                    ActionType.AddPolicy,
                    ActionType.DeletePolicy,
                    ActionType.ModifyPolicy,

                    ActionType.ManageRules,
                    ActionType.ManageFunctions,

                    ActionType.AddRule,
                    ActionType.DeleteRule,
                    ActionType.ModifyRule,
                    ActionType.ShowRuleTrail,
                    ActionType.ShowAllRuleTrails,

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
                    //bayad bardashte shee
                    ActionType.FillInquiryForm,
                    ActionType.DeleteCustomInquirer,

                    
                    ActionType.ShowClaim,
                    ActionType.ReplyToClaim,
                    ActionType.ShowAdminClaimList,
                    

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

                    ActionType.ShowLog,
                    ActionType.DeleteLog,
                };
            }
        }

        public AdminUser(PartyId userId, string fName, string lName, string email)
            : base(userId, fName, lName, email)
        {
        }

    }
}