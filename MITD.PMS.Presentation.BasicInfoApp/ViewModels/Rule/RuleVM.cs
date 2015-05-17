using System.Collections.Generic;
using MITD.PMS.Presentation.Contracts;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public class RuleVM : BasicInfoWorkSpaceViewModel
    {
        #region Fields

        private readonly IPMSController appController;
        private readonly IRuleServiceWrapper ruleService;
        private ActionType actionType;

        #endregion

        #region Properties & Backfields

        private List<RuleExcuteTimeDTO> excuteTimeList;
        public List<RuleExcuteTimeDTO> ExcuteTimeList
        {
            get { return excuteTimeList; }
            set { this.SetField(vm => vm.ExcuteTimeList, ref excuteTimeList, value); }
        }

        private RuleDTO rule;
        public RuleDTO Rule
        {
            get { return rule; }
            set { this.SetField(vm => vm.Rule, ref rule, value); }
        }

        private CommandViewModel saveCommand;
        public CommandViewModel SaveCommand
        {
            get
            {
                if (saveCommand == null)
                {
                    saveCommand = new CommandViewModel("ذخیره", new DelegateCommand(save));
                }
                return saveCommand;
            }
        }

        private CommandViewModel cancelCommand;
        public CommandViewModel CancelCommand
        {
            get
            {
                if (cancelCommand == null)
                {
                    cancelCommand = new CommandViewModel("انصراف", new DelegateCommand(OnRequestClose));
                }
                return cancelCommand;
            }
        }

        private CommandViewModel compileCommand;
        public CommandViewModel CompileCommand
        {
            get
            {
                if (compileCommand == null)
                {
                    compileCommand = new CommandViewModel("کامپایل", new DelegateCommand(compile));
                }
                return compileCommand;
            }
        }


        #endregion

        #region Constructors

        public RuleVM()
        {

            init();
            Rule = new RuleDTO { Name = "قانون یک"};
        }
        public RuleVM(IPMSController appController, IRuleServiceWrapper ruleService, IBasicInfoAppLocalizedResources basicInfoAppLocalizedResources)
        {
            init();
            this.appController = appController;
            this.ruleService = ruleService;
            BasicInfoAppLocalizedResources = basicInfoAppLocalizedResources;
            DisplayName = BasicInfoAppLocalizedResources.RuleViewTitle;


        }

        #endregion

        #region Methods

        private void init()
        {
            Rule = new RuleDTO();

        }


        public void Load(RuleDTO ruleParam, ActionType actionTypeParam)
        {
            preLoad();
            Rule = ruleParam;
            actionType = actionTypeParam;
        }

        private void preLoad()
        {
            ShowBusyIndicator("در حال دریافت اطلاعات");
            ruleService.GetRuleExcuteTimes( (res, exp) => appController.BeginInvokeOnDispatcher(() =>
                {
                    HideBusyIndicator();
                    if (exp == null)
                        ExcuteTimeList = res;
                    else
                        appController.HandleException(exp);
                }));
        }

        private void save()
        {
            if (!rule.Validate()) return;

            ShowBusyIndicator();
            if (actionType == ActionType.AddRule)
            {
                ruleService.AddRule((res, exp) => appController.BeginInvokeOnDispatcher(() =>
                    {
                        HideBusyIndicator();
                        if (exp != null)
                            appController.HandleException(exp);
                        else
                        {
                            finalizeAction(false);
                            appController.ShowMessage("با موفقیت ذخیره شد.");
                        }
                    }), Rule);
            }
            else if (actionType == ActionType.ModifyRule)
            {
                ruleService.UpdateRule((res, exp) => appController.BeginInvokeOnDispatcher(() =>
                    {
                        HideBusyIndicator();
                        if (exp != null)
                            appController.HandleException(exp);
                        else
                        {
                            finalizeAction(false);
                            appController.ShowMessage("با موفقیت ذخیره شد.");
                        }
                    }), Rule);
            }
        }

        private void compile()
        {
            ShowBusyIndicator("در حال دریافت اطلاعات...");
            ruleService.CompileRule((res, exp) => appController.BeginInvokeOnDispatcher(() =>
            {
                HideBusyIndicator();
                if (exp == null)
                    appController.ShowMessage("متن قانون مورد نظر قابل اجرا است");
                else
                    appController.HandleException(exp);
            }), rule.PolicyId, new RuleContentDTO { RuleContent = Rule.RuleText });
        }

        private void finalizeAction(bool doClose=true)
        {
            appController.Publish(new UpdateRuleListArgs());
            if(doClose)
                OnRequestClose();
        }

        protected override void OnRequestClose()
        {
            base.OnRequestClose();
            appController.Close(this);
        }



        #endregion
    }
}

