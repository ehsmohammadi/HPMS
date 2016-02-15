using System;
using System.Collections.Generic;
using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;
using MITD.Presentation.Config;
using MITD.Presentation;

namespace MITD.PMS.Presentation.BasicInfoApp
{
    public class BootStrapper : IBootStrapper
    {
        public void Execute()
        {
            var resourceDic = App.GetResourceStream(new Uri("MITD.PMS.Presentation.BasicInfoApp;component/Assets/LocalResource.xaml", UriKind.Relative));
            ApplicationConfigHelper.ConfigureModule<IBasicInfoController, BasicInfoController>(resourceDic);
            ModuleConfigHelper.ConfigureActionModule<IActionService>(new Dictionary<int, Type>
                {
                    {(int) ActionType.CreateCustomField,typeof(AddCustomFieldService)},
                    {(int) ActionType.ModifyCustomField,typeof(ModifyCustomFieldService)},
                    {(int) ActionType.DeleteCustomField,typeof(DeleteCustomFieldService)},

                    {(int) ActionType.CreateJob,typeof(AddJobService)},
                    {(int) ActionType.ModifyJob,typeof(ModifyJobService)},
                    {(int) ActionType.DeleteJob,typeof(DeleteJobService)},
                    {(int) ActionType.ManageJobCustomFields,typeof(ManageJobCustomFieldsService)},


                    {(int) ActionType.ModifyJobIndex,typeof(ModifyJobIndexService)},
                    {(int) ActionType.DeleteJobIndex,typeof(DeleteJobIndexService)},
                    {(int) ActionType.AddJobIndex,typeof(AddJobIndexService)},
                    {(int) ActionType.ManageJobIndexCustomFields,typeof(ManageJobIndexCustomFieldsService)},

                    {(int) ActionType.ModifyJobIndexCategory,typeof(ModifyJobIndexCategoryService)},
                    {(int) ActionType.DeleteJobIndexCategory,typeof(DeleteJobIndexCategoryService)},
                    {(int) ActionType.AddJobIndexCategory,typeof(AddJobIndexCategoryService)},

                    {(int) ActionType.ModifyUnitIndex,typeof(ModifyUnitIndexService)},
                    {(int) ActionType.DeleteUnitIndex,typeof(DeleteUnitIndexService)},
                    {(int) ActionType.AddUnitIndex,typeof(AddUnitIndexService)},
                    {(int) ActionType.ManageUnitIndexCustomFields,typeof(ManageUnitIndexCustomFieldsService)},

                    {(int) ActionType.ModifyUnitIndexCategory,typeof(ModifyUnitIndexCategoryService)},
                    {(int) ActionType.DeleteUnitIndexCategory,typeof(DeleteUnitIndexCategoryService)},
                    {(int) ActionType.AddUnitIndexCategory,typeof(AddUnitIndexCategoryService)},

                    {(int) ActionType.ModifyJobPosition,typeof(ModifyJobPositionService)},
                    {(int) ActionType.DeleteJobPosition,typeof(DeleteJobPositionService)},
                    {(int) ActionType.AddJobPosition,typeof(AddJobPositionService)},
                    
                    {(int) ActionType.AddPolicy,typeof(AddPolicyService)},
                    {(int) ActionType.ModifyPolicy,typeof(ModifyPolicyService)},
                    {(int) ActionType.DeletePolicy,typeof(DeletePolicyService)},
                    {(int) ActionType.ManageRules,typeof(ManageRulesService)},
                    {(int) ActionType.ManageFunctions,typeof(ManageFunctionsService)},
                    
                    {(int) ActionType.AddRule,typeof(AddRuleService)},
                    {(int) ActionType.ModifyRule,typeof(ModifyRuleService)},
                    {(int) ActionType.DeleteRule,typeof(DeleteRuleService)},
                    {(int) ActionType.ShowAllRuleTrails,typeof(ShowAllRuleTrailService)},
                    {(int) ActionType.ShowRuleTrail,typeof(ShowRuleTrailService)},

                    //{(int) ActionType.AddRuleVersion,typeof(AddRuleVersionService)},
                    //{(int) ActionType.ModifyLastRuleVersion,typeof(ModifyLastRuleVersionService)},
                    //{(int) ActionType.DeleteLastRuleVersion,typeof(DeleteLastRuleVersionService)},
                    //{(int) ActionType.ViewRuleVersions,typeof(ViewRuleVersionsService)},

                    {(int) ActionType.CreateFunction,typeof(AddFunctionService)},
                    {(int) ActionType.ModifyFunction,typeof(ModifyFunctionService)},
                    {(int) ActionType.DeleteFunction,typeof(DeleteFunctionService)},

                    {(int) ActionType.AddUnit,typeof(AddUnitService)},
                    {(int) ActionType.ModifyUnit,typeof(ModifyUnitService)},
                    {(int) ActionType.DeleteUnit,typeof(DeleteUnitService)},
                    {(int) ActionType.ManageUnitCustomFields,typeof(ManageUnitCustomFieldsService)},
                    //{(int) ActionType.AddUnitCustomFields,typeof(ManageUnitCustomFieldsService)},

                    {(int) ActionType.AddUser,typeof(AddUserService)},
                    {(int) ActionType.ModifyUser,typeof(ModifyUserService)},
                    {(int) ActionType.DeleteUser,typeof(DeleteUserService)},

                    {(int) ActionType.AddUserGroup,typeof(AddUserGroupService)},
                    {(int) ActionType.ModifyUserGroup,typeof(ModifyUserGroupService)},
                    {(int) ActionType.DeleteUserGroup,typeof(DeleteUserGroupService)},

                    {(int) ActionType.ManageUserCustomActions,typeof(ManageUserCustomActionsService)},
                    {(int) ActionType.ManageGroupCustomActions,typeof(ManageGroupCustomActionsService)},
                    {(int) ActionType.ManageUserWorkListUsers,typeof(ManageWorkListUsersService)},

                    

                    {(int) ActionType.ShowLog,typeof(ShowLogService)},
                    {(int) ActionType.DeleteLog,typeof(DeleteLogService)},
                    

                });



        }
    }
}
