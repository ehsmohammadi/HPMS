using System.Collections.Generic;
using System.Linq;
using MITD.PMS.Presentation.Contracts;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public class RuleTrailVM : BasicInfoWorkSpaceViewModel
    {
        #region Fields

        private readonly IPMSController appController;
        private readonly IRuleServiceWrapper ruleService;

        #endregion

        #region Properties & Backfields

       

        private RuleTrailDTO rule;
        public RuleTrailDTO Rule
        {
            get { return rule; }
            set { this.SetField(vm => vm.Rule, ref rule, value); }
        }

        private string ruleExcuteTimeStr;
        public string RuleExcuteTimeStr
        {
            get { return ruleExcuteTimeStr; }
            set { this.SetField(vm => vm.RuleExcuteTimeStr, ref ruleExcuteTimeStr, value); }
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

        
        #endregion

        #region Constructors

        public RuleTrailVM()
        {

            init();
            Rule = new RuleTrailDTO { Name = "قانون یک"};
        }
        public RuleTrailVM(IPMSController appController, IRuleServiceWrapper ruleService, IBasicInfoAppLocalizedResources basicInfoAppLocalizedResources)
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
            Rule = new RuleTrailDTO();

        }

        private void preLoad()
        {
            ShowBusyIndicator("در حال دریافت اطلاعات");
            ruleService.GetRuleExcuteTimes((res, exp) => appController.BeginInvokeOnDispatcher(() =>
            {
                HideBusyIndicator();
                if (exp == null)
                    RuleExcuteTimeStr = res.Single(r=>r.Id == Rule.ExcuteTime).Name;
                else
                    appController.HandleException(exp);
            }));
        }
        public void Load(RuleTrailDTO ruleParam)
        {
            Rule = ruleParam;
            preLoad();
        }

        
        private void finalizeAction()
        {
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

