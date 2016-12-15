using MITD.Core;
using MITD.PMS.Presentation.Logic;
using MITD.PMS.Presentation.Contracts;
using MITD.Presentation;

namespace MITD.PMS.Presentation.BasicInfoApp
{
    public class BasicInfoController : IBasicInfoController
    {
        private IViewManager viewManager;


        public BasicInfoController(IViewManager viewManager)
        {
            this.viewManager = viewManager;
        }

        #region User
        public void ShowUserList(bool isShiftPressed)
        {
            var view = viewManager.ShowInTabControl<IUserListView>(isShiftPressed);
            ((UserListVM)view.ViewModel).Load();
        }

        public void ShowUserGroupList(bool isShiftPressed)
        {
            var view = viewManager.ShowInTabControl<IUserGroupListView>(isShiftPressed);
            ((UserGroupListVM)view.ViewModel).Load();
        }

        public void ShowUserView(UserDTO user, ActionType action)
        {
            var view = ServiceLocator.Current.GetInstance<IUserView>();
            ((UserVM)view.ViewModel).Load(user, action);
            viewManager.ShowInDialog(view);
        }

        public void ShowUserGroupView(UserGroupDTO userGroup, ActionType action)
        {
            var view = ServiceLocator.Current.GetInstance<IUserGroupView>();
            ((UserGroupVM)view.ViewModel).Load(userGroup, action);
            viewManager.ShowInDialog(view);
        }

        public void ShowCustomActionsManageViews(PartyDTO party, bool isgroup, string groupId)
        {
            var view = ServiceLocator.Current.GetInstance<IPartyCustomActionsView>();
            ((PartyCustomActionsVM)view.ViewModel).Load(party, isgroup, groupId);
            viewManager.ShowInDialog(view);
        }

        public void ShowWorkListUsersManageViews(UserDTO user)
        {
            var view = ServiceLocator.Current.GetInstance<IManageWorkListUsersView>();
            ((ManageWorkListUsersVM)view.ViewModel).Load(user);
            viewManager.ShowInDialog(view);
        }

        public void ShowChangePasswordView(bool isShiftPressed)
        {
            var view = ServiceLocator.Current.GetInstance<IChangePasswordView>();
            viewManager.ShowInDialog(view);
        }

        public void ShowEmailInView(UserStateDTO user, bool isShiftPressed)
        {
            //var view = ServiceLocator.Current.GetInstance<IEmailInView>();
            //viewManager.ShowInDialog(view);
        } 
        #endregion

        #region Job

        public void ShowJobView(JobDTO job, ActionType actionType)
        {
            var view = ServiceLocator.Current.GetInstance<IJobView>();
            ((JobVM)view.ViewModel).Load(job, actionType);
            viewManager.ShowInDialog(view);
        }

        public void ShowJobListView(bool isShiftPressed)
        {
            var view = viewManager.ShowInTabControl<IJobListView>(isShiftPressed);
            ((JobListVM)view.ViewModel).Load();
        } 

        #endregion

        #region JobIndex

        public void ShowJobIndexView(JobIndexDTO jobIndex, ActionType actionType)
        {
            var view = ServiceLocator.Current.GetInstance<IJobIndexView>();
            ((JobIndexVM)view.ViewModel).Load(jobIndex, actionType);
            viewManager.ShowInDialog(view);
        }

        public void ShowJobIndexTreeView(bool isShiftPressed)
        {
            var view = viewManager.ShowInTabControl<IJobIndexTreeView>(isShiftPressed);
            ((JobIndexTreeVM)view.ViewModel).Load();
        }

        public void ShowJobIndexCategoryView(JobIndexCategoryDTO jobIndexCategory, ActionType action)
        {
            var view = ServiceLocator.Current.GetInstance<IJobIndexCategoryView>();
            ((JobIndexCategoryVM)view.ViewModel).Load(jobIndexCategory, action);
            viewManager.ShowInDialog(view);
        }

        public void ShowJobIndexCustomFieldManageView(JobIndexDTO jobIndex, ActionType action)
        {
            var view = ServiceLocator.Current.GetInstance<IJobIndexCustomFieldManageView>();
            ((JobIndexCustomFieldManageVM)view.ViewModel).Load(jobIndex, action);
            viewManager.ShowInDialog(view);
        }
        #endregion

        #region UnitIndex

        public void ShowUnitIndexView(UnitIndexDTO unitIndex, ActionType actionType)
        {
            var view = ServiceLocator.Current.GetInstance<IUnitIndexView>();
            ((UnitIndexVM)view.ViewModel).Load(unitIndex, actionType);
            viewManager.ShowInDialog(view);
        }

        public void ShowUnitIndexTreeView(bool isShiftPressed)
        {
            var view = viewManager.ShowInTabControl<IUnitIndexTreeView>(isShiftPressed);
            ((UnitIndexTreeVM)view.ViewModel).Load();
        }

        public void ShowUnitIndexCategoryView(UnitIndexCategoryDTO unitIndexCategory, ActionType action)
        {
            var view = ServiceLocator.Current.GetInstance<IUnitIndexCategoryView>();
            ((UnitIndexCategoryVM)view.ViewModel).Load(unitIndexCategory, action);
            viewManager.ShowInDialog(view);
        }

        public void ShowUnitIndexCustomFieldManageView(UnitIndexDTO unitIndex, ActionType action)
        {
            var view = ServiceLocator.Current.GetInstance<IUnitIndexCustomFieldManageView>();
            ((UnitIndexCustomFieldManageVM)view.ViewModel).Load(unitIndex, action);
            viewManager.ShowInDialog(view);
        }
        #endregion

        public void ShowJobPositionView(JobPositionDTO jobPosition, ActionType actionType)
        {
            var view = ServiceLocator.Current.GetInstance<IJobPositionView>();
            ((JobPositionVM)view.ViewModel).Load(jobPosition, actionType);
            viewManager.ShowInDialog(view);
        }

        public void ShowJobPositionList(bool isShiftPressed)
        {
            var view = viewManager.ShowInTabControl<IJobPositionListView>(isShiftPressed);
            ((JobPositionListVM)view.ViewModel).Load();
        }

        public void ShowRuleView(RuleDTO rule, ActionType actionType)
        {
            var view = ServiceLocator.Current.GetInstance<IRuleView>();
            ((RuleVM)view.ViewModel).Load(rule, actionType);
            viewManager.ShowInDialog(view);
        }


        public void ShowRuleList(long policyId)
        {
            var view = viewManager.ShowInTabControl<IRuleListView>();
            ((RuleListVM)view.ViewModel).Load(policyId);
        }

        public void ShowRuleTrailsView(RuleDTO res)
        {
            var view = ServiceLocator.Current.GetInstance<IRuleTrailListView>();
            ((RuleTrailListVM)view.ViewModel).Load(res);
            viewManager.ShowInDialog(view);
        }

        public void ShowRuleTrailView(RuleTrailDTO res)
        {
            var view = ServiceLocator.Current.GetInstance<IRuleTrailView>();
            ((RuleTrailVM)view.ViewModel).Load(res);
            viewManager.ShowInDialog(view);
        }



        #region CustomField

        public void ShowCustomFieldView(CustomFieldDTO customFieldDto, ActionType action)
        {
            var view = ServiceLocator.Current.GetInstance<ICustomFieldView>();
            ((CustomFieldVM)view.ViewModel).Load(customFieldDto, action);
            viewManager.ShowInDialog(view);
        }

        public void ShowCustomFieldListView(bool isShiftPressed)
        {
            var view = viewManager.ShowInTabControl<ICustomFieldListView>(isShiftPressed);
            ((CustomFieldListVM)view.ViewModel).Load();
        }

        public void ShowJobCustomFieldManageView(JobDTO job, ActionType action)
        {
           var view = ServiceLocator.Current.GetInstance<IJobCustomFieldManageView>();
           ((JobCustomFieldManageVM)view.ViewModel).Load(job, action);
            viewManager.ShowInDialog(view);
        }



        #endregion


        #region Unit

        public void ShowUnitView(UnitDTO unit, ActionType actionType)
        {
            var view = ServiceLocator.Current.GetInstance<IUnitView>();
            ((UnitVM)view.ViewModel).Load(unit, actionType);
            viewManager.ShowInDialog(view);
        }

        public void ShowUnitList(bool isShiftPressed)
        {
            var view = viewManager.ShowInTabControl<IUnitListView>(isShiftPressed);
            ((UnitListVM)view.ViewModel).Load();
        }

        public void ShowUnitCustomFieldManageView(UnitDTO unitDto, ActionType action)
        {
            var view = ServiceLocator.Current.GetInstance<IUnitCustomFieldManageView>();
            ((UnitCustomFieldManageVM)view.ViewModel).Load(unitDto, action);
            viewManager.ShowInDialog(view);
        }

        public void ShowPolicyView(PolicyDTO policy, ActionType actionType)
        {
            var view = ServiceLocator.Current.GetInstance<IPolicyView>();
            ((PolicyVM)view.ViewModel).Load(policy, actionType);
            viewManager.ShowInDialog(view);
        }

        public void ShowPolicyListView(bool isShiftPressed)
        {
            var view = viewManager.ShowInTabControl<IPolicyListView>(isShiftPressed);
            ((PolicyListVM)view.ViewModel).Load();
        }

        //public void ShowRuleVersionView(RuleDTO rule, ActionType action)
        //{
        //    var view = ServiceLocator.Current.GetInstance<IRuleVersionView>();
        //    ((RuleVersionVM)view.ViewModel).Load(rule, action);
        //    viewManager.ShowInDialog(view);
        //}

        public void ShowFunctionListView(long policyId)
        {
            var view = viewManager.ShowInTabControl<IFunctionListView>();
            ((FunctionListVM)view.ViewModel).Load(policyId);
        }

      
        public void ShowFunctionView(FunctionDTO function, ActionType action)
        {
            var view = ServiceLocator.Current.GetInstance<IFunctionView>();
            ((FunctionVM)view.ViewModel).Load(function, action);
            viewManager.ShowInDialog(view);
        }



        public void ShowLogList(bool isShiftPressed)
        {
            var view = viewManager.ShowInTabControl<ILogListView>(isShiftPressed);
            ((LogListVM)view.ViewModel).Load();
        }

        public void ShowLogView(LogDTO log)
        {
            var view = ServiceLocator.Current.GetInstance<ILogView>();
            ((LogVM)view.ViewModel).Load(log);
            viewManager.ShowInDialog(view);
        }



        #endregion



    }


}
