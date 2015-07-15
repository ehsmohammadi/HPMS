using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Castle.Core.Internal;
using MITD.Core;
using MITD.PMS.Presentation.BasicInfoApp;
using MITD.PMS.Presentation.BasicInfoApp.Views;
using MITD.PMS.Presentation.Contracts;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public class JobIndexTreeVM : BasicInfoWorkSpaceViewModel, IEventHandler<UpdateJobIndexTreeArgs>
    {

        #region Fields

        private readonly IPMSController appController;
        private readonly IJobIndexServiceWrapper jobIndexService;

        #endregion

        #region Properties & Back Field

        private ObservableCollection<TreeElementViewModel<AbstractJobIndexDTOWithActions>> jobIndexTree;
        public ObservableCollection<TreeElementViewModel<AbstractJobIndexDTOWithActions>> JobIndexTree
        {
            get { return jobIndexTree; }
            set { this.SetField(p => p.JobIndexTree, ref jobIndexTree, value); }
        }

        private TreeElementViewModel<AbstractJobIndexDTOWithActions> selectedjobIndex;
        public TreeElementViewModel<AbstractJobIndexDTOWithActions> SelectedJobIndex
        {
            get { return selectedjobIndex; }
            set
            {
                this.SetField(p => p.SelectedJobIndex, ref selectedjobIndex, value);
                if (selectedjobIndex == null) return;
                JobIndexCommands = createCommands();
                if (View != null)
                    ((IJobIndexTreeView)View).CreateContextMenu(new ReadOnlyCollection<DataGridCommandViewModel>(JobIndexCommands));
            }
        }

        private List<DataGridCommandViewModel> jobIndexCommands;
        public List<DataGridCommandViewModel> JobIndexCommands
        {
            get { return jobIndexCommands; }
            private set
            {
                this.SetField(p => p.JobIndexCommands, ref jobIndexCommands, value);
                if (JobIndexCommands.Count > 0) SelectedCommand = JobIndexCommands[0];
            }

        }

        private DataGridCommandViewModel selectedCommand;
        public DataGridCommandViewModel SelectedCommand
        {
            get { return selectedCommand; }
            set { this.SetField(p => p.SelectedCommand, ref selectedCommand, value); }
        }

        private CommandViewModel addRootCategory;
        public CommandViewModel AddRootCategory
        {
            get
            {
                if (addRootCategory == null)
                {
                    addRootCategory = new CommandViewModel("ایجاد دسته شاخص اصلی", new DelegateCommand(() =>
                        {
                            var action = appController.PMSActions.Single(a => a.ActionCode == ActionType.AddJobIndexCategory);
                            SelectedJobIndex = null;
                            action.DoAction(this);
                        }));
                }
                return addRootCategory;
            }
        }

        #endregion

        #region Constructors

        public JobIndexTreeVM()
        {
            init();
        }

        public JobIndexTreeVM(
                               IPMSController appController,
                               IBasicInfoAppLocalizedResources basicInfoAppLocalizedResources,
                               IJobIndexServiceWrapper jobIndexService)
        {
            this.appController = appController;
            this.jobIndexService = jobIndexService;
            BasicInfoAppLocalizedResources = basicInfoAppLocalizedResources;
            init();
        }

        #endregion

        #region Methods

        void init()
        {
            DisplayName = "مدیریت شاخص های شغل ";
            JobIndexTree = new ObservableCollection<TreeElementViewModel<AbstractJobIndexDTOWithActions>>();
            JobIndexCommands = new List<DataGridCommandViewModel> {
                new  DataGridCommandViewModel{ CommandViewModel = AddRootCategory}
            };
        }

        private List<DataGridCommandViewModel> createCommands()
        {

            var filterCommand = new List<DataGridCommandViewModel>();
            filterCommand.Add(new DataGridCommandViewModel { CommandViewModel = AddRootCategory });
            if (SelectedJobIndex != null)
            {
                appController.PMSActions.Where(
                    a => SelectedJobIndex.Data.ActionCodes.Contains((int)a.ActionCode)).ForEach(
                        action => filterCommand.Add(new DataGridCommandViewModel
                            {

                                CommandViewModel = new CommandViewModel(action.ActionName,
                                                                        new DelegateCommand(
                                                                            () =>
                                                                            action.DoAction(this),
                                                                            () => true)),
                                Icon = action.ActionIcon
                            }));
            }
            return filterCommand;

        }

        public void Load()
        {
            ShowBusyIndicator("در حال دریافت اطلاعات...");
            jobIndexService.GetAllAbstractJobIndex(
                (res, exp) => appController.BeginInvokeOnDispatcher(() =>
                {

                    if (exp == null)
                    {
                        JobIndexTree = SilverLightTreeViewHelper<AbstractJobIndexDTOWithActions>.prepareListForTreeView(res);
                        HideBusyIndicator();
                    }
                    else
                    {
                        HideBusyIndicator();
                        appController.HandleException(exp);
                    }
                }));
        }

        protected override void OnRequestClose()
        {
            base.OnRequestClose();
            appController.Close(this);
        }

        public void Handle(UpdateJobIndexTreeArgs eventData)
        {
            Load();
        }

        #endregion


    }

}
