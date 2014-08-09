using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MITD.Core;
using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.PeriodManagementApp.Views;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public class JobPositionInPeriodTreeVM : PeriodMgtWorkSpaceViewModel, IEventHandler<UpdateJobPositionInPeriodTreeArgs>
    {

        #region Fields

        private readonly IPMSController appController;
        private readonly IJobPositionInPeriodServiceWrapper jobPositionInPeriodService;
        private readonly IPeriodServiceWrapper periodService;

        #endregion

        #region Properties & Back Field

        private PeriodDTO period;
        public PeriodDTO Period
        {
            get { return period; }
            set { this.SetField(p => p.Period, ref period, value); }
        }

        private ObservableCollection<TreeElementViewModel<JobPositionInPeriodDTOWithActions>> jobPositionInPeriodTree;
        public ObservableCollection<TreeElementViewModel<JobPositionInPeriodDTOWithActions>> JobPositionInPeriodTree
        {
            get { return jobPositionInPeriodTree; }
            set { this.SetField(p => p.JobPositionInPeriodTree, ref jobPositionInPeriodTree, value); }
        }

        private TreeElementViewModel<JobPositionInPeriodDTOWithActions> selectedJobPositionInPeriod;
        public TreeElementViewModel<JobPositionInPeriodDTOWithActions> SelectedJobPositionInPeriod
        {
            get { return selectedJobPositionInPeriod; }
            set
            {
                this.SetField(p => p.SelectedJobPositionInPeriod, ref selectedJobPositionInPeriod, value);
                if (selectedJobPositionInPeriod == null) return;
                JobPositionInPeriodCommands = createCommands();
                if (View != null)
                    ((IJobPositionInPeriodTreeView)View).CreateContextMenu(new ReadOnlyCollection<DataGridCommandViewModel>(JobPositionInPeriodCommands));
            }
        }

        private List<DataGridCommandViewModel> jobPositionInPeriodCommands;
        public List<DataGridCommandViewModel> JobPositionInPeriodCommands
        {
            get { return jobPositionInPeriodCommands; }
            private set
            {
                this.SetField(p => p.JobPositionInPeriodCommands, ref jobPositionInPeriodCommands, value);
                if (JobPositionInPeriodCommands.Count > 0) SelectedCommand = JobPositionInPeriodCommands[0];
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
                        var action = appController.PMSActions.Single(a => a.ActionCode == ActionType.AddJobPositionInPeriod);
                        SelectedJobPositionInPeriod = null;
                        action.DoAction(this);
                    }));
                }
                return addRootCommand;
            }
        }

        #endregion

        #region Constructors

        public JobPositionInPeriodTreeVM()
        {
            init();
        }

        public JobPositionInPeriodTreeVM(IPMSController appController,
                                  IPeriodMgtAppLocalizedResources periodMgtAppLocalizedResources,
                                  IJobPositionInPeriodServiceWrapper jobPositionInPeriodService,
                                  IPeriodServiceWrapper periodService)
        {

            this.appController = appController;
            this.jobPositionInPeriodService = jobPositionInPeriodService;
            this.periodService = periodService;
            PeriodMgtAppLocalizedResources = periodMgtAppLocalizedResources;
            init();
        }

        #endregion

        #region Methods

        void init()
        {
            DisplayName = PeriodMgtAppLocalizedResources.JobPositionInPeriodTreeViewTitle;
            JobPositionInPeriodTree = new ObservableCollection<TreeElementViewModel<JobPositionInPeriodDTOWithActions>>();
            Period = new PeriodDTO();
            JobPositionInPeriodCommands = new List<DataGridCommandViewModel> {
                new  DataGridCommandViewModel{ CommandViewModel = AddRootCommand}
            };
        }

        private List<DataGridCommandViewModel> createCommands()
        {
            var commands = new List<DataGridCommandViewModel>();
            commands.Add(new DataGridCommandViewModel { CommandViewModel = AddRootCommand });
            if (selectedJobPositionInPeriod != null)
                commands.AddRange(CommandHelper.GetControlCommands(this, appController, selectedJobPositionInPeriod.Data.ActionCodes));
            return commands;
        }

        public void Load(PeriodDTO period)
        {
            ShowBusyIndicator("در حال دریافت اطلاعات...");
            Period = period;
            DisplayName = PeriodMgtAppLocalizedResources.JobPositionInPeriodTreeViewTitle+" "+Period.Name;
            jobPositionInPeriodService.GetJobPositionsWithActions(
                (res, exp) =>  appController.BeginInvokeOnDispatcher(() =>
                {

                    if (exp == null)
                    {
                        JobPositionInPeriodTree = SilverLightTreeViewHelper<JobPositionInPeriodDTOWithActions>.prepareListForTreeView(res);
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

        public void Handle(UpdateJobPositionInPeriodTreeArgs eventData)
        {
            Load(Period);
        }

        #endregion


    }

}
