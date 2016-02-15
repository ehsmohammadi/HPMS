using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Castle.Core.Internal;
using MITD.Core;
using MITD.PMS.Presentation.BasicInfoApp;
using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.PeriodManagementApp.Views;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public class FunctionListVM : BasicInfoWorkSpaceViewModel, IEventHandler<UpdateFunctionListArgs>
    {
        #region Fields

        private readonly IPMSController appController;
        private readonly IFunctionServiceWrapper functionService;

        #endregion

        #region Properties & Back Field

        private PolicyFunctions policyFunctions;
        public PolicyFunctions PolicyFunctions
        {
            get { return policyFunctions; }
            set { this.SetField(p => p.PolicyFunctions, ref policyFunctions, value); }
        }

        private PagedSortableCollectionView<FunctionDTODescriptionWithActions> functions;
        public PagedSortableCollectionView<FunctionDTODescriptionWithActions> Functions
        {
            get { return functions; }
            set { 
                
                this.SetField(p => p.Functions, ref functions, value); 
            }
        }

        private FunctionDTODescriptionWithActions selectedFunction;
        public FunctionDTODescriptionWithActions SelectedFunction
        {
            get { return selectedFunction; }
            set
            {
                this.SetField(p => p.SelectedFunction, ref selectedFunction, value);
                if (selectedFunction == null) return;
                FunctionCommands = createCommands();
                if (View != null)
                    ((IFunctionListView)View).CreateContextMenu(new ReadOnlyCollection<DataGridCommandViewModel>(FunctionCommands));
            }
        }

        private List<DataGridCommandViewModel> functionCommands;
        public List<DataGridCommandViewModel> FunctionCommands
        {
            get { return functionCommands; }
            private set
            {
                this.SetField(p => p.FunctionCommands, ref functionCommands, value);
                if (FunctionCommands.Count > 0) SelectedCommand = FunctionCommands[0];
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

        public FunctionListVM()
        {
            BasicInfoAppLocalizedResources = new BasicInfoAppLocalizedResources();
            init();
            Functions.Add(new FunctionDTODescriptionWithActions {  });
        }

        public FunctionListVM(IPMSController appController,
                              IFunctionServiceWrapper functionService,
                              IBasicInfoAppLocalizedResources basicInfoAppLocalizedResources)
        {
            
            this.appController = appController;
            this.functionService = functionService;
            BasicInfoAppLocalizedResources = basicInfoAppLocalizedResources;
            init();

        }

        #endregion

        #region Methods

        void init()
        {
            DisplayName = "مدیریت توابع";
            Functions = new PagedSortableCollectionView<FunctionDTODescriptionWithActions>();
            PolicyFunctions = new PolicyFunctions();
            Functions.OnRefresh += (s, args) => Load(PolicyFunctions.PolicyId);
            FunctionCommands = new List<DataGridCommandViewModel>
            {
                   CommandHelper.GetControlCommands(this, appController, new List<int>{ (int) ActionType.CreateFunction}).FirstOrDefault()
            };
        }
      
        private List<DataGridCommandViewModel> createCommands()
        {
            var filterCommand = new List<DataGridCommandViewModel>();
            appController.PMSActions.Where(a => SelectedFunction.ActionCodes.Contains((int)a.ActionCode)).ForEach(
                action => filterCommand.Add(new DataGridCommandViewModel
                {

                    CommandViewModel = new CommandViewModel(action.ActionName,
                                                            new DelegateCommand(
                                                                () => action.DoAction(this),
                                                                () => true)),
                    Icon = action.ActionIcon
                }));

            return filterCommand;
        }

        public void Load(long policyId)
        {
            functionService.GetPolicyFunctions(
                 (res, exp) => appController.BeginInvokeOnDispatcher(() =>
                {
                    HideBusyIndicator();
                    if (exp == null)
                    {
                        PolicyFunctions = res;
                        functions.SourceCollection = PolicyFunctions.Functions;
                        DisplayName = BasicInfoAppLocalizedResources.FunctionListViewTitle+PolicyFunctions.PolicyName;
                    }
                    else appController.HandleException(exp);
                }), policyId);

        }

        protected override void OnRequestClose()
        {
            base.OnRequestClose();
            appController.Close(this);
        }
      
        public void Handle(UpdateFunctionListArgs eventData)
        {
            Load(PolicyFunctions.PolicyId);
        }

        #endregion
    }

}
