using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Castle.Core.Internal;
using MITD.Core;
using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.PeriodManagementApp;
using MITD.PMS.Presentation.PeriodManagementApp.Views;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public class JobIndexInPeriodTreeVM : PeriodMgtWorkSpaceViewModel, IEventHandler<UpdateJobIndexInPeriodTreeArgs>
    {

        #region Fields

        private readonly IPMSController appController;
        private readonly IJobIndexInPeriodServiceWrapper jobIndexInPeriodService;
        private readonly IPeriodServiceWrapper periodService;

        #endregion

        #region Properties & Back Field

        private PeriodDTO period;
        public PeriodDTO Period
        {
            get { return period; }
            set { this.SetField(p => p.Period, ref period, value); }
        }

        private List<AbstractIndexInPeriodDTOWithActions> abstractIndexInPeriods;
        public List<AbstractIndexInPeriodDTOWithActions> AbstractIndexInPeriods
        {
            get { return abstractIndexInPeriods; }
            set { this.SetField(p => p.AbstractIndexInPeriods, ref abstractIndexInPeriods, value); }
        }


        private TreeElementViewModel<AbstractIndexInPeriodDTOWithActions> selectedAbstractIndexInPeriod;
        public TreeElementViewModel<AbstractIndexInPeriodDTOWithActions> SelectedAbstractIndexInPeriod
        {
            get { return selectedAbstractIndexInPeriod; }
            set
            {
                this.SetField(p => p.SelectedAbstractIndexInPeriod, ref selectedAbstractIndexInPeriod, value);
                if (selectedAbstractIndexInPeriod == null) return;
                JobIndexInPeriodCommands = createCommands();
                if (View != null)
                    ((IJobIndexInPeriodTreeView)View).CreateContextMenu(new ReadOnlyCollection<DataGridCommandViewModel>(JobIndexInPeriodCommands));
            }
        }

        private List<DataGridCommandViewModel> jobIndexInPeriodCommands;
        public List<DataGridCommandViewModel> JobIndexInPeriodCommands
        {
            get { return jobIndexInPeriodCommands; }
            private set
            {
                this.SetField(p => p.JobIndexInPeriodCommands, ref jobIndexInPeriodCommands, value);
                if (JobIndexInPeriodCommands.Count > 0) SelectedCommand = JobIndexInPeriodCommands[0];
            }

        }

        private DataGridCommandViewModel selectedCommand;
        public DataGridCommandViewModel SelectedCommand
        {
            get { return selectedCommand; }
            set { this.SetField(p => p.SelectedCommand, ref selectedCommand, value); }
        }

        private ObservableCollection<TreeElementViewModel<AbstractIndexInPeriodDTOWithActions>> abstractIndexInPeriodTree;
        public ObservableCollection<TreeElementViewModel<AbstractIndexInPeriodDTOWithActions>> AbstractIndexInPeriodTree
        {
            get { return abstractIndexInPeriodTree; }
            set { this.SetField(p => p.AbstractIndexInPeriodTree, ref abstractIndexInPeriodTree, value); }
        }

        private CommandViewModel addRootGroupCommand;
        public CommandViewModel AddRootGroupCommand
        {
            get
            {
                if (addRootGroupCommand == null)
                {
                    addRootGroupCommand = new CommandViewModel("ایجاد گروه شاخص اصلی", new DelegateCommand(() =>
                    {
                        var action = appController.PMSActions.Single(a => a.ActionCode == ActionType.AddJobIndexGroupInPeriod);
                        SelectedAbstractIndexInPeriod = null;
                        action.DoAction(this);
                    }));
                }
                return addRootGroupCommand;
            }
        }

        #endregion

        #region Constructors

        public JobIndexInPeriodTreeVM()
        {
            PeriodMgtAppLocalizedResources = new PeriodMgtAppLocalizedResources();
            init();
        }

        public JobIndexInPeriodTreeVM(
                               IPMSController appController,
                               IPeriodMgtAppLocalizedResources periodMgtAppLocalizedResources,
                               IJobIndexInPeriodServiceWrapper jobIndexInPeriodService,
                               IPeriodServiceWrapper periodService)
        {

            this.appController = appController;
            this.jobIndexInPeriodService = jobIndexInPeriodService;
            this.periodService = periodService;
            PeriodMgtAppLocalizedResources = periodMgtAppLocalizedResources;
            init();
        }

        #endregion

        #region Methods

        void init()
        {
            DisplayName = PeriodMgtAppLocalizedResources.JobIndexInPeriodTreeViewTitle;
            AbstractIndexInPeriodTree = new ObservableCollection<TreeElementViewModel<AbstractIndexInPeriodDTOWithActions>>();
            Period = new PeriodDTO();
            abstractIndexInPeriods = new List<AbstractIndexInPeriodDTOWithActions>();
            JobIndexInPeriodCommands = new List<DataGridCommandViewModel> {
                new  DataGridCommandViewModel{ CommandViewModel = AddRootGroupCommand}
            };
        }

        private List<DataGridCommandViewModel> createCommands()
        {
            var commands = new List<DataGridCommandViewModel>();
            commands.Add(new DataGridCommandViewModel { CommandViewModel = AddRootGroupCommand });
            if (SelectedAbstractIndexInPeriod != null)
                commands.AddRange(CommandHelper.GetControlCommands(this, appController, SelectedAbstractIndexInPeriod.Data.ActionCodes));
            return commands;
        }

        public void Load(PeriodDTO periodParam)
        {
            Period = periodParam;
            DisplayName = PeriodMgtAppLocalizedResources.JobIndexInPeriodTreeViewTitle+" "+Period.Name;
            setIndexInPeriodTree(Period.Id);
        }

        private void setIndexInPeriodTree(long periodId)
        {
            ShowBusyIndicator("در حال دریافت اطلاعات...");
            jobIndexInPeriodService.GetPeriodAbstractIndexes(
                (res, exp) => appController.BeginInvokeOnDispatcher(() =>
                    {
                        HideBusyIndicator();
                        if (exp == null)
                        {
                            AbstractIndexInPeriods = res;
                            AbstractIndexInPeriodTree =
                                SilverLightTreeViewHelper<AbstractIndexInPeriodDTOWithActions>.prepareListForTreeView(
                                    AbstractIndexInPeriods);
                            HideBusyIndicator();
                        }
                        else
                        {
                            HideBusyIndicator();
                            appController.HandleException(exp);
                        }
                    }), periodId);
        }

        protected override void OnRequestClose()
        {
            base.OnRequestClose();
            appController.Close(this);
        }

        public void Handle(UpdateJobIndexInPeriodTreeArgs eventData)
        {
            setIndexInPeriodTree(Period.Id);
        }

        #endregion


    }

}
