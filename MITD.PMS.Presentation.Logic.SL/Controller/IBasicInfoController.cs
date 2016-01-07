using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Presentation.Logic
{
    public interface IBasicInfoController
    {
        #region Job
        void ShowJobView(JobDTO job, ActionType actionEnum);
        void ShowJobListView(bool isShiftPressed); 
        #endregion

        #region JobIndex
        void ShowJobIndexView(JobIndexDTO jobIndex, ActionType actionType);
        void ShowJobIndexTreeView(bool isShiftPressed);

        void ShowJobIndexCategoryView(JobIndexCategoryDTO jobIndexCategory, ActionType addJobIndexCategory);

        void ShowJobIndexCustomFieldManageView(JobIndexDTO jobIndex, ActionType action);
        #endregion

        #region UnitIndex
        void ShowUnitIndexView(UnitIndexDTO unitIndex, ActionType actionType);
        void ShowUnitIndexTreeView(bool isShiftPressed);

        void ShowUnitIndexCategoryView(UnitIndexCategoryDTO unitIndexCategory, ActionType addUnitIndexCategory);

        void ShowUnitIndexCustomFieldManageView(UnitIndexDTO unitIndex, ActionType action);
        #endregion

        void ShowJobPositionView(JobPositionDTO jobPosition, ActionType actionType);
        void ShowJobPositionList(bool isShiftPressed);

        void ShowRuleView(RuleDTO jobRule, ActionType actionType);
        void ShowRuleList(long policy);
        void ShowRuleTrailsView(RuleDTO res);
        void ShowRuleTrailView(RuleTrailDTO res);
        //void ShowRuleVersionView(RuleDTO rule, ActionType action);

        

        void ShowCustomFieldView(CustomFieldDTO jobIndex, ActionType addCustomField);
        void ShowCustomFieldListView(bool isShiftPressed);

        [RequiredPermission(ActionType.ManageJobCustomFields)]
        void ShowJobCustomFieldManageView(JobDTO job, ActionType modifyJobFields);
        

        void ShowUnitView(UnitDTO unit, ActionType actionType);
        void ShowUnitList(bool isShiftPressed);
        void ShowUnitCustomFieldManageView(UnitDTO unitDto, ActionType action);

        void ShowPolicyView(PolicyDTO policy, ActionType actionType);
        void ShowPolicyListView(bool isShiftPressed);
        

        void ShowFunctionListView(long policyDto);
        void ShowFunctionView(FunctionDTO function, ActionType action);

        [RequiredPermission(ActionType.ModifyUser)]
        void ShowUserList(bool isShiftPressed);

        void ShowUserGroupList(bool isShiftPressed);
        void ShowUserView(UserDTO user, ActionType action);
        void ShowUserGroupView(UserGroupDTO userGroup, ActionType action);
        void ShowCustomActionsManageViews(PartyDTO party, bool isgroup, string groupId);
        void ShowWorkListUsersManageViews(UserDTO user);

        void ShowLogList(bool isShiftPressed);
        void ShowLogView(LogDTO log);

    }
}
