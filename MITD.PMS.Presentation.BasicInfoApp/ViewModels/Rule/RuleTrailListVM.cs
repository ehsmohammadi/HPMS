using MITD.Core;
using MITD.PMS.Presentation.Contracts;
using MITD.Presentation;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MITD.PMS.Presentation.Logic
{
    public class RuleTrailListVM : BasicInfoWorkSpaceViewModel, IEventHandler<UpdateRuleTrailListArgs>
    {
        #region Fields

        private readonly IPMSController appController;
        private readonly IRuleServiceWrapper ruleService;

        #endregion

        #region Properties & Back Fields

        private PagedSortableCollectionView<RuleTrailDTOWithAction> ruleTrails;
        public PagedSortableCollectionView<RuleTrailDTOWithAction> RuleTrails
        {
            get { return ruleTrails; }
            set { this.SetField(p => p.RuleTrails, ref ruleTrails, value); }
        }

        private RuleTrailDTOWithAction selectedRuleTrail;
        public RuleTrailDTOWithAction SelectedRuleTrail
        {
            get { return selectedRuleTrail; }
            set
            {
                this.SetField(p => p.SelectedRuleTrail, ref selectedRuleTrail, value);
                if (selectedRuleTrail == null) return;
                RuleTrailCommands = createCommands();
                if (View != null)
                    ((IRuleTrailListView)View).CreateContextMenu(new ReadOnlyCollection<DataGridCommandViewModel>(RuleTrailCommands));
            }
        }

        private List<DataGridCommandViewModel> ruleTrailCommands;
        public List<DataGridCommandViewModel> RuleTrailCommands
        {
            get { return ruleTrailCommands; }
            private set
            {
                this.SetField(p => p.RuleTrailCommands, ref ruleTrailCommands, value);
                if (RuleTrailCommands.Count > 0) SelectedCommand = RuleTrailCommands[0];
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

        public RuleTrailListVM()
        {
            init();
            RuleTrails.Add(new RuleTrailDTOWithAction { Id = 4, Name = "Test" });
        }

        public RuleTrailListVM(IPMSController appController,
            IRuleServiceWrapper ruleService, IBasicInfoAppLocalizedResources basicInfoAppLocalizedResources)
        {
            this.appController = appController;
            this.ruleService = ruleService;
            this.BasicInfoAppLocalizedResources = basicInfoAppLocalizedResources;
            DisplayName = basicInfoAppLocalizedResources.RuleTrailListViewTitle;
            init();
        }

        #endregion

        #region Methods

        void init()
        {
            RuleTrails = new PagedSortableCollectionView<RuleTrailDTOWithAction>();
            //RuleTrails.OnRefresh += (s, args) => Load();
            RuleTrailCommands = new List<DataGridCommandViewModel>();
        }

        private List<DataGridCommandViewModel> createCommands()
        {
            return CommandHelper.GetControlCommands(this, appController, SelectedRuleTrail.ActionCodes);
        }

        public void Load(RuleDTO rule)
        {
            ShowBusyIndicator("در حال دریافت اطلاعات...");
            ruleService.GetAllRuleTrails(
                 (res, exp) => appController.BeginInvokeOnDispatcher(() =>
                 {
                     HideBusyIndicator();
                     if (exp == null)
                     {
                         RuleTrails.SourceCollection = res.Result;
                         RuleTrails.TotalItemCount = res.TotalCount;
                         RuleTrails.PageIndex = Math.Max(0, res.CurrentPage - 1);
                     }
                     else appController.HandleException(exp);
                 }),rule.PolicyId,rule.Id, RuleTrails.PageSize, RuleTrails.PageIndex + 1);
        }

        protected override void OnRequestClose()
        {
            base.OnRequestClose();
            appController.Close(this);
        }


        public void Handle(UpdateRuleTrailListArgs eventData)
        {
            
            Load(eventData.RuleDto);
        }

        #endregion

    }
}
