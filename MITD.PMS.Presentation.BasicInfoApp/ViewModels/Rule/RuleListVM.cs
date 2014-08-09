using MITD.PMS.Presentation.Contracts;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public class RuleListVM : BasicInfoWorkSpaceViewModel
    {
        #region Fields

        private readonly IPMSController appController;
        private readonly IPolicyServiceWrapper policyService;

        #endregion

        #region Properties

        private PolicyDTO policy;
        public PolicyDTO Policy
        {
            get { return policy; }
            set { this.SetField(p => p.Policy, ref policy, value); }
        }

        #endregion

        #region Constructors

        public RuleListVM()
        {
            init();

        }

        public RuleListVM(IPMSController appController,
                          IPolicyServiceWrapper policyService,
                          IBasicInfoAppLocalizedResources basicInfoAppLocalizedResources)
        {

            this.appController = appController;
            this.policyService = policyService;
            BasicInfoAppLocalizedResources = basicInfoAppLocalizedResources;
            init();

        }

        #endregion

        #region Methods

        void init()
        {
            Policy = new PolicyDTO();
            DisplayName = BasicInfoAppLocalizedResources.RuleListViewTitle;
        }



        public void Load(long policyId)
        {
            ShowBusyIndicator("در حال دریافت اطلاعات");
            policyService.GetPolicy( (res, exp) => appController.BeginInvokeOnDispatcher(() =>
                {
                    HideBusyIndicator();
                    if (exp == null)
                    {
                        Policy = res;
                        DisplayName = BasicInfoAppLocalizedResources.RuleListViewTitle + " " + Policy.Name;
                    }
                    else
                        appController.HandleException(exp);
                }), policyId);

        }


        protected override void OnRequestClose()
        {
            base.OnRequestClose();
            appController.Close(this);
        }


        #endregion


    }
}
