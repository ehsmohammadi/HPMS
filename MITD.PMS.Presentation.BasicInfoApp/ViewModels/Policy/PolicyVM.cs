using MITD.PMS.Presentation.Contracts;
using MITD.Presentation;
using MITD.Core;


namespace MITD.PMS.Presentation.Logic
{
    public class PolicyVM : BasicInfoWorkSpaceViewModel, IEventHandler<UpdatePolicyRuleListArgs>
    {
        #region Fields

        private readonly IPMSController appController;
        private readonly IPolicyServiceWrapper policyService;
        private ActionType actionType;

        #endregion

        #region Properties

        private PolicyDTO policy;
        public PolicyDTO Policy
        {
            get { return policy; }
            set
            {
                this.SetField(vm => vm.Policy, ref policy, value);
            }
        }

        private bool gridRuleListViewVisibility;
        public bool GridRuleListViewVisibility
        {
            get { return gridRuleListViewVisibility; }
            set { this.SetField(vm => vm.GridRuleListViewVisibility, ref gridRuleListViewVisibility, value); }
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
                    cancelCommand = new CommandViewModel("انصراف",new DelegateCommand(OnRequestClose));
                }
                return cancelCommand;
            }
        } 

        #endregion

        #region Constructors

        public PolicyVM()
        {
            init();
            Policy = new PolicyDTO { Name = "Policy1", DictionaryName = "Policy1" };
        }

        public PolicyVM(IPMSController appController, 
                        IPolicyServiceWrapper policyService,
                        IBasicInfoAppLocalizedResources basicInfoAppLocalizedResources)
        {
            init();
            this.appController = appController;
            this.policyService = policyService;
            BasicInfoAppLocalizedResources = basicInfoAppLocalizedResources;
            DisplayName = BasicInfoAppLocalizedResources.PolicyViewTitle;
            
            
        }


        #endregion

        #region Methods

        private void init()
        {
            Policy = new PolicyDTO();
            GridRuleListViewVisibility=new bool();         
        }

        public void Load(PolicyDTO policyParam,ActionType actionTypeParam)
        {
            actionType = actionTypeParam;
            Policy = policyParam;
            GridRuleListViewVisibility = false;
            if (actionType == ActionType.ModifyPolicy)
            {
                GridRuleListViewVisibility = true;
            }
            
        }

        private void save()
        {
            if (!policy.Validate()) return;

            ShowBusyIndicator();
            if (actionType==ActionType.AddPolicy)
            {
                policyService.AddPolicy((res, exp) => appController.BeginInvokeOnDispatcher(() =>
                    {
                        HideBusyIndicator();
                        if (exp != null)
                            appController.HandleException(exp);
                        else
                            finalizeAction();
                    }), policy);
            }
            else if (actionType == ActionType.ModifyPolicy)
            {
                policyService.UpdatePolicy((res, exp) => appController.BeginInvokeOnDispatcher(() =>
                    {
                        HideBusyIndicator();
                        if (exp != null)
                            appController.HandleException(exp);
                        else
                            finalizeAction();
                    }), policy);
            }
        }

        private void finalizeAction()
        {
            appController.Publish(new UpdatePolicyListArgs());
            OnRequestClose();
        }

        protected override void OnRequestClose()
        {
            base.OnRequestClose();
            appController.Close(this);
        }

        public void Handle(UpdatePolicyRuleListArgs eventData)
        {
            
        }

        #endregion

    }
}

