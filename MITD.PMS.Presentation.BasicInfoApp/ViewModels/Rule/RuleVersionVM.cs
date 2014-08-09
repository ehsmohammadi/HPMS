using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using MITD.Core;
using MITD.PMS.Presentation.Contracts;
using MITD.Presentation;
using System;

namespace MITD.PMS.Presentation.Logic
{
    public class RuleVersionVM : BasicInfoWorkSpaceViewModel,IEventHandler<UpdateRuleVersionsArgs>
    {
        #region Fields

        private readonly IPMSController appController;
        private readonly IRuleServiceWrapper ruleService;
        private readonly IPeriodServiceWrapper periodService;
        private ActionEnum actionType;
        private Dictionary<ActionEnum, Action> actions;

        #endregion

        #region Properties & Backfields

        private RuleDTO rule;
        public RuleDTO Rule
        {
            get { return rule; }
            set { this.SetField(vm => vm.Rule, ref rule, value); }
        }

        private ObservableCollection<PeriodDescriptionDTO> periods;
        public ObservableCollection<PeriodDescriptionDTO> Periods
        {
            get { return periods; }
            set { this.SetField(vm => vm.Periods, ref periods, value); }
        }

        private ObservableCollection<RuleVersionDTO> versions;
        public ObservableCollection<RuleVersionDTO> Versions
        {
            get { return versions; }
            set { this.SetField(vm => vm.Versions, ref versions, value); }
        }

        private RuleVersionDTO selectedRuleVersion;
        public RuleVersionDTO SelectedRuleVersion
        {
            get { return selectedRuleVersion; }
            set { this.SetField(vm => vm.SelectedRuleVersion, ref selectedRuleVersion, value); }
        }

        private bool showVersion;
        public bool ShowVersion
        {
            get { return showVersion; }
            set { this.SetField(vm => vm.ShowVersion, ref showVersion, value); }
        }

        private bool showVersions;
        public bool ShowVersions
        {
            get { return showVersions; }
            set { this.SetField(vm => vm.ShowVersions, ref showVersions, value); }
        }

        private bool showCompileCommand;
        public bool ShowCompileCommand
        {
            get { return showCompileCommand; }
            set { this.SetField(vm => vm.ShowCompileCommand, ref showCompileCommand, value); }
        }

        private bool showSaveCommand;
        public bool ShowSaveCommand
        {
            get { return showSaveCommand; }
            set { this.SetField(vm => vm.ShowSaveCommand, ref showSaveCommand, value); }
        }

        private CommandViewModel saveCommand;
        public CommandViewModel SaveCommand
        {
            get
            {
                if (saveCommand == null)
                {
                    saveCommand = new CommandViewModel("تایید", new DelegateCommand(save));
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

        private ICommand setSelectedRuleContentCommand;
        public ICommand SetSelectedRuleContentCommand
        {
            get
            {
                if (setSelectedRuleContentCommand == null)
                {
                    setSelectedRuleContentCommand = new DelegateCommand(setSelectedRuleContent);
                }
                return setSelectedRuleContentCommand;
            }
        }


        #endregion

        #region Constructors

        public RuleVersionVM()
        {
            init();
            Rule = new RuleDTO { Name = "قانون یک" };
        }

        public RuleVersionVM(IPMSController appController,
            IRuleServiceWrapper ruleService,
            IPeriodServiceWrapper periodService,
            IBasicInfoAppLocalizedResources basicInfoAppLocalizedResources)
        {
            init();
            initializeActions();
            this.appController = appController;
            this.ruleService = ruleService;
            this.periodService = periodService;
            BasicInfoAppLocalizedResources = basicInfoAppLocalizedResources;
            
            

        }

        #endregion

        #region Methods

        private void init()
        {
            Rule = new RuleDTO();
            SelectedRuleVersion=new RuleVersionDTO();
            Versions=new ObservableCollection<RuleVersionDTO>();
            Periods=new ObservableCollection<PeriodDescriptionDTO>();
           
        }

        public void Load(RuleDTO ruleParam, ActionEnum actionTypeParam)
        {
            preLoad();
            Rule = ruleParam;
            actionType = actionTypeParam;
            actions[actionTypeParam].Invoke();

        }

        private void preLoad()
        {
            periodService.GetAllPeriods((res, exp) =>
                {
                    if (exp == null)
                        Periods = res;
                    else
                        appController.HandleException(exp);
                });
        }

        private void initializeActions()
        {
            actions = new Dictionary<ActionEnum, Action>
                {
                    {ActionEnum.AddRuleVersion, customizeForAddRuleVersion},
                    {ActionEnum.ModifyLastRuleVersion, customizeForModifyLastRuleVersion},
                    {ActionEnum.ViewRuleVersions, customizeForViewRuleVersions}
                };
        }

        private void customizeForViewRuleVersions()
        {
            ShowVersion = false;
            ShowVersions = true;
            ShowSaveCommand = false;
            ShowCompileCommand = false;
            DisplayName = BasicInfoAppLocalizedResources.RuleVersionViewShowRuleVersionsTitle;
            getVersions();

        }

        private void getVersions()
        {
            ShowBusyIndicator("در حال دریافت اطلاعات...");
            ruleService.GetRuleVersionsOrderByVersionIdDesc((res, exp) =>appController.BeginInvokeOnDispatcher(()=>
                {
                    HideBusyIndicator();
                    if (exp == null)
                    {
                        Versions = res;
                        Periods = new ObservableCollection<PeriodDescriptionDTO>(
                            Periods.Where(p => Versions.Select(v => v.PeriodId).Contains(p.Id)).ToList());
            
                        SelectedRuleVersion = shallowCopy(Versions.LastOrDefault());
            
                    }
                    else
                        appController.HandleException(exp);
                }), Rule.Id);
        }

        private void customizeForModifyLastRuleVersion()
        {
            ShowVersion = true;
            ShowVersions = false;
            ShowSaveCommand = true;
            ShowCompileCommand = true;
            DisplayName = BasicInfoAppLocalizedResources.RuleVersionViewModifyRuleVersionTitle;
            ShowBusyIndicator("در حال دریافت اطلاعات...");
            ruleService.GetLastRuleVersion((res, exp) =>appController.BeginInvokeOnDispatcher(()=>
            {
                HideBusyIndicator();
                if (exp == null)
                    SelectedRuleVersion = res;
                else
                    appController.HandleException(exp);
            }), Rule.Id);
        }

        private void customizeForAddRuleVersion()
        {
            ShowVersion = true;
            ShowVersions = false;
            ShowSaveCommand = true;
            ShowCompileCommand = true;
            DisplayName = BasicInfoAppLocalizedResources.RuleVersionViewAddRuleVersionTitle;
            ShowBusyIndicator("در حال دریافت اطلاعات...");
            ruleService.GetNewRuleVersionNo((res, exp) =>appController.BeginInvokeOnDispatcher(()=>
                {
                    HideBusyIndicator();
                    if (exp == null)
                        SelectedRuleVersion.VersionId = res;
                    else
                        appController.HandleException(exp);
                }), Rule.Id);
        }

        private void save()
        {
            if (!rule.Validate()) return;          
            if (actionType == ActionEnum.AddRuleVersion)
            {
                ShowBusyIndicator("در حال دریافت اطلاعات...");
                ruleService.AddRuleVersion((res, exp) => appController.BeginInvokeOnDispatcher(() =>
                    {
                        HideBusyIndicator();
                        if (exp != null)
                            appController.HandleException(exp);
                        else
                            finalizeAction();
                    }), SelectedRuleVersion);
            }
            else if (actionType == ActionEnum.ModifyRule)
            {
                ShowBusyIndicator("در حال دریافت اطلاعات...");
                ruleService.UpdateLastRuleVersion((res, exp) => appController.BeginInvokeOnDispatcher(() =>
                    {
                        HideBusyIndicator();
                        if (exp != null)
                            appController.HandleException(exp);
                        else
                            finalizeAction();
                    }), SelectedRuleVersion);
            }
        }

        private void compile()
        {
            ShowBusyIndicator("در حال دریافت اطلاعات...");
            ruleService.CompileRule((res, exp) =>appController.BeginInvokeOnDispatcher(()=>
                {
                    HideBusyIndicator();
                    if (exp == null)
                        appController.ShowMessage("متن قانون مورد نظر قابل اجرا است");
                    else
                        appController.HandleException(exp);
                }), SelectedRuleVersion.Content);
        }

        private void setSelectedRuleContent()
        {
            SelectedRuleVersion = shallowCopy(Versions.First(v => v.VersionId == SelectedRuleVersion.VersionId));
        }

        private void finalizeAction()
        {
            appController.Publish(new UpdateRuleListArgs());
            OnRequestClose();
        }

        protected override void OnRequestClose()
        {
            base.OnRequestClose();
            appController.Close(this);
        }

        #endregion

        public void Handle(UpdateRuleVersionsArgs eventData)
        {
            getVersions();
        }
        
        private RuleVersionDTO shallowCopy(RuleVersionDTO dto)
        {
            var clone = new RuleVersionDTO
                {
                    Content = dto.Content,
                    Id = dto.Id,
                    PeriodId = dto.PeriodId,
                    VersionId = dto.VersionId
                };
            return clone;      
        }
    }
}

