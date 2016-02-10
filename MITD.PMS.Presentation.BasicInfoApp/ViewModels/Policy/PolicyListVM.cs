using MITD.Core;
using MITD.PMS.Presentation.Contracts;
using MITD.Presentation;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MITD.PMS.Presentation.Logic
{
    public class PolicyListVM : BasicInfoWorkSpaceViewModel, IEventHandler<UpdatePolicyListArgs>
    {
        #region Fields

        private readonly IPMSController appController;
        private readonly IPolicyServiceWrapper policyService;

        #endregion

        #region Properties & Back Fields

        private PagedSortableCollectionView<PolicyDTOWithActions> policys;
        public PagedSortableCollectionView<PolicyDTOWithActions> Policys
        {
            get { return policys; }
            set { this.SetField(p => p.Policys, ref policys, value); }
        }

        private PolicyDTOWithActions selectedPolicy;
        public PolicyDTOWithActions SelectedPolicy
        {
            get { return selectedPolicy; }
            set
            {
                this.SetField(p => p.SelectedPolicy, ref selectedPolicy, value);
                if (selectedPolicy == null) return;
                PolicyCommands = createCommands();
                if (View != null)
                    ((IPolicyListView)View).CreateContextMenu(new ReadOnlyCollection<DataGridCommandViewModel>(PolicyCommands));
            }
        }

        private List<DataGridCommandViewModel> policyCommands;
        public List<DataGridCommandViewModel> PolicyCommands
        {
            get { return policyCommands; }
            private set
            {
                this.SetField(p => p.PolicyCommands, ref policyCommands, value);
                if (PolicyCommands.Count > 0) SelectedCommand = PolicyCommands[0];
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

        public PolicyListVM()
        {
            init();
            Policys.Add(new PolicyDTOWithActions { Id = 4, Name = "Test" });
        }

        public PolicyListVM(IPMSController appController,
            IPolicyServiceWrapper policyService, IBasicInfoAppLocalizedResources basicInfoAppLocalizedResources)
        {
            this.appController = appController;
            this.policyService = policyService;
            this.BasicInfoAppLocalizedResources = basicInfoAppLocalizedResources;
            DisplayName = basicInfoAppLocalizedResources.PolicyListViewTitle;
            init();
        }

        #endregion

        #region Methods

        void init()
        {
            Policys = new PagedSortableCollectionView<PolicyDTOWithActions> { PageSize = 20 };
            Policys.OnRefresh += (s, args) => Load();
            PolicyCommands = new List<DataGridCommandViewModel>
            {
                   CommandHelper.GetControlCommands(this, appController, new List<int>{ (int) ActionType.AddPolicy }).FirstOrDefault()
            };
        }

        private List<DataGridCommandViewModel> createCommands()
        {
            return CommandHelper.GetControlCommands(this, appController, SelectedPolicy.ActionCodes);
        }

        public void Load()
        {
            ShowBusyIndicator("در حال دریافت اطلاعات...");
            policyService.GetAllPolicys(
                 (res, exp) => appController.BeginInvokeOnDispatcher(() =>
                 {
                     HideBusyIndicator();
                     if (exp == null)
                     {
                         Policys.SourceCollection = res.Result;
                         Policys.TotalItemCount = res.TotalCount;
                         Policys.PageIndex = Math.Max(0, res.CurrentPage - 1);
                     }
                     else appController.HandleException(exp);
                 }), Policys.PageSize, Policys.PageIndex + 1);
        }

        protected override void OnRequestClose()
        {
            base.OnRequestClose();
            appController.Close(this);
        }


        public void Handle(UpdatePolicyListArgs eventData)
        {
            Load();
        }

        #endregion

    }
}
