using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MITD.Core;
using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.PeriodManagementApp.Views;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public class UnitInPeriodTreeVM : PeriodMgtWorkSpaceViewModel, IEventHandler<UpdateUnitInPeriodTreeArgs>
    {

        #region Fields

        private readonly IPMSController appController;       
        private readonly IUnitInPeriodServiceWrapper unitInPeriodService;
        private readonly IPeriodServiceWrapper periodService;

        #endregion

        #region Properties & Back Field

        private PeriodDTO period;
        public PeriodDTO Period
        {
            get { return period; }
            set { this.SetField(p => p.Period, ref period, value); }
        }

        private ObservableCollection<TreeElementViewModel<UnitInPeriodDTOWithActions>> unitInPeriodTree;
        public ObservableCollection<TreeElementViewModel<UnitInPeriodDTOWithActions>> UnitInPeriodTree
        {
            get { return unitInPeriodTree; }
            set { this.SetField(p => p.UnitInPeriodTree, ref unitInPeriodTree, value); }
        }

        private TreeElementViewModel<UnitInPeriodDTOWithActions> selectedUnitInPeriod;
        public TreeElementViewModel<UnitInPeriodDTOWithActions> SelectedUnitInPeriod
        {
            get { return selectedUnitInPeriod; }
            set
            {
                this.SetField(p => p.SelectedUnitInPeriod, ref selectedUnitInPeriod, value);
                if (selectedUnitInPeriod == null) return;
                UnitInPeriodCommands = createCommands();
                if (View != null)
                    ((IUnitInPeriodTreeView)View).CreateContextMenu(new ReadOnlyCollection<DataGridCommandViewModel>(UnitInPeriodCommands));
            }
        }

        private List<DataGridCommandViewModel> unitInPeriodCommands;
        public List<DataGridCommandViewModel> UnitInPeriodCommands
        {
            get { return unitInPeriodCommands; }
            private set
            {
                this.SetField(p => p.UnitInPeriodCommands, ref unitInPeriodCommands, value);
                if (UnitInPeriodCommands.Count > 0) SelectedCommand = UnitInPeriodCommands[0];
            }

        }

        private DataGridCommandViewModel selectedCommand;
        public DataGridCommandViewModel SelectedCommand
        {
            get { return selectedCommand; }
            set { this.SetField(p => p.SelectedCommand, ref selectedCommand, value); }
        }

        private CommandViewModel addRootCommand;
        public CommandViewModel AddRootCommand
        {
            get
            {
                if (addRootCommand == null)
                {
                    addRootCommand = new CommandViewModel("ایجاد ریشه اصلی", new DelegateCommand(() =>
                    {
                        var action = appController.PMSActions.Single(a => a.ActionCode == ActionType.AddUnitInPeriod);
                        SelectedUnitInPeriod = null;
                        action.DoAction(this);
                    }));
                }
                return addRootCommand;
            }
        }

        #endregion

        #region Constructors

        public UnitInPeriodTreeVM()
        {
            init();
        }

        public UnitInPeriodTreeVM(IPMSController appController,
                                  IPeriodMgtAppLocalizedResources periodMgtAppLocalizedResources,
                                  IUnitInPeriodServiceWrapper unitInPeriodService,
                                  IPeriodServiceWrapper periodService)
        {

            this.appController = appController;
            this.unitInPeriodService = unitInPeriodService;
            this.periodService = periodService;
            PeriodMgtAppLocalizedResources = periodMgtAppLocalizedResources;
            init();
        }

        #endregion

        #region Methods

        void init()
        {
            DisplayName = PeriodMgtAppLocalizedResources.UnitInPeriodTreeViewTitle;
            UnitInPeriodTree = new ObservableCollection<TreeElementViewModel<UnitInPeriodDTOWithActions>>();
            Period = new PeriodDTO();
            UnitInPeriodCommands = new List<DataGridCommandViewModel> {
                new  DataGridCommandViewModel{ CommandViewModel = AddRootCommand}
            };
        }

        private List<DataGridCommandViewModel> createCommands()
        {
            var commands = new List<DataGridCommandViewModel>();
            commands.Add(new DataGridCommandViewModel { CommandViewModel = AddRootCommand });
            if (SelectedUnitInPeriod != null)
                commands.AddRange(CommandHelper.GetControlCommands(this, appController, SelectedUnitInPeriod.Data.ActionCodes));
            return commands;
        }

        public void Load(PeriodDTO period)
        {
            ShowBusyIndicator("در حال دریافت اطلاعات...");
            Period = period;
            DisplayName = PeriodMgtAppLocalizedResources.UnitInPeriodTreeViewTitle+" "+Period.Name;
            unitInPeriodService.GetUnitsWithActions(
                (res, exp) =>appController.BeginInvokeOnDispatcher(()=>
                {

                    if (exp == null)
                    {
                        UnitInPeriodTree = SilverLightTreeViewHelper<UnitInPeriodDTOWithActions>.prepareListForTreeView(res);
                        HideBusyIndicator();
                    }
                    else
                    {
                        HideBusyIndicator();
                        appController.HandleException(exp);
                    }
                }), period.Id);

        }

        protected override void OnRequestClose()
        {
            base.OnRequestClose();
            appController.Close(this);
        }

        public void Handle(UpdateUnitInPeriodTreeArgs eventData)
        {
            Load(Period);
        }

        #endregion


    }

}
