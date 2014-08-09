using MITD.Core;
using MITD.PMS.Presentation.Contracts;
using MITD.Presentation;
using MITD.PMS.Presentation.BasicInfoApp.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MITD.PMS.Presentation.Logic
{
    public class GridRuleListVM : BasicInfoWorkSpaceViewModel, IEventHandler<UpdateRuleListArgs>
    {
        #region Fields

        private readonly IPMSController appController;
        private readonly IRuleServiceWrapper ruleService;

        #endregion

        #region Properties & Fields

        private PolicyDTO policy;
        public PolicyDTO Policy
        {
            get { return policy; }
            set
            {
                this.SetField(p => p.Policy, ref policy, value);
            }
        }

        private PolicyRules policyRules;
        public PolicyRules PolicyRules
        {
            get { return policyRules; }
            set
            {
                this.SetField(p => p.PolicyRules, ref policyRules, value);
            }
        }

        private RuleDTOWithAction selectedRule;
        public RuleDTOWithAction SelectedRule
        {
            get { return selectedRule; }
            set
            {
                this.SetField(p => p.SelectedRule, ref selectedRule, value);
                if (selectedRule == null) return;
                RuleCommands = createCommands();
                if (View != null)
                    ((IGridRuleListView)View).CreateContextMenu(new ReadOnlyCollection<DataGridCommandViewModel>(RuleCommands));
            }
        }

        private List<DataGridCommandViewModel> ruleCommands;
        public List<DataGridCommandViewModel> RuleCommands
        {
            get { return ruleCommands; }
            private set
            {
                this.SetField(p => p.RuleCommands, ref ruleCommands, value);
                if (RuleCommands.Count > 0) SelectedCommand = RuleCommands[0];
            }

        }

        private DataGridCommandViewModel selectedCommand;
        public DataGridCommandViewModel SelectedCommand
        {
            get { return selectedCommand; }
            set { this.SetField(p => p.SelectedCommand, ref selectedCommand, value); }
        }

        #endregion

        #region Constructors

        public GridRuleListVM()
        {
            init();
        }

        public GridRuleListVM(IPMSController appController,
                              IRuleServiceWrapper ruleService,
                              IBasicInfoAppLocalizedResources basicInfoAppLocalizedResources)
        {
            this.appController = appController;
            this.ruleService = ruleService;
            BasicInfoAppLocalizedResources = basicInfoAppLocalizedResources;
            init();
        }

        #endregion

        #region Methods

        void init()
        {
            PolicyRules = new PolicyRules();
            RuleCommands = new List<DataGridCommandViewModel>
            {
                   CommandHelper.GetControlCommands(this, appController, new List<int>{ (int) ActionType.AddRule}).FirstOrDefault()
            };
        }

        public void Load(PolicyDTO policyParam)
        {
            Policy = policyParam;
            DisplayName = BasicInfoAppLocalizedResources.GridRuleListViewTitle + " " + Policy.Name;
            ShowBusyIndicator("در حال دریافت اطلاعات...");
            if (policy.Id == 0)
                return;
            ruleService.GetPolicyRules(
                (res, exp) => appController.BeginInvokeOnDispatcher(() =>
                {
                    HideBusyIndicator();
                    if (exp == null)
                    {
                        PolicyRules = res;
                    }
                    else appController.HandleException(exp);
                }), Policy.Id);
        }

        private List<DataGridCommandViewModel> createCommands()
        {
            return CommandHelper.GetControlCommands(this, appController, SelectedRule.ActionCodes);
        }

        protected override void OnRequestClose()
        {
            base.OnRequestClose();
            appController.Close(this);
        }

        public void Handle(UpdateRuleListArgs eventData)
        {
            Load(Policy);
        }

        #endregion


    }
}
