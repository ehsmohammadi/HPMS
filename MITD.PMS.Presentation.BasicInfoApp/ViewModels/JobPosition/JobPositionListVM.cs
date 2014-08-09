using MITD.Core;
using MITD.PMS.Presentation.Contracts;
using MITD.Presentation;
using MITD.PMS.Presentation.BasicInfoApp;
using MITD.PMS.Presentation.BasicInfoApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Castle.Core.Internal;

namespace MITD.PMS.Presentation.Logic
{
    public class JobPositionListVM : BasicInfoWorkSpaceViewModel, IEventHandler<UpdateJobPositionListArgs>
    {
        #region Fields

        private readonly IBasicInfoController basicInfoController;
        private readonly IPMSController appController;
        private readonly IJobPositionServiceWrapper jobPositionService;

        #endregion

        #region Properties &  Back Fields

        private PagedSortableCollectionView<JobPositionDTOWithActions> jobPositions;
        public PagedSortableCollectionView<JobPositionDTOWithActions> JobPositions
        {
            get { return jobPositions; }
            set { this.SetField(p => p.JobPositions, ref jobPositions, value); }
        }


        private JobPositionDTOWithActions selectedJobPosition;
        public JobPositionDTOWithActions SelectedJobPosition
        {
            get { return selectedJobPosition; }
            set
            {
                this.SetField(p => p.SelectedJobPosition, ref selectedJobPosition, value);
                if (selectedJobPosition == null) return;
                JobPositionCommands = createCommands();
                if (View != null)
                    ((IJobPositionListView)View).CreateContextMenu(new ReadOnlyCollection<DataGridCommandViewModel>(JobPositionCommands));
            }
        }

        private List<DataGridCommandViewModel> jobPositionCommands;
        public List<DataGridCommandViewModel> JobPositionCommands
        {
            get { return jobPositionCommands; }
            private set
            {
                this.SetField(p => p.JobPositionCommands, ref jobPositionCommands, value);
                if (JobPositionCommands.Count > 0) SelectedCommand = JobPositionCommands[0];
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

        public JobPositionListVM()
        {
            init();
            JobPositions.Add(new JobPositionDTOWithActions { Id = 4, Name = "Test" });
        }

        public JobPositionListVM(IBasicInfoController basicInfoController, IPMSController appController,
            IJobPositionServiceWrapper jobPositionService, IBasicInfoAppLocalizedResources basicInfoAppLocalizedResources)
        {
            this.appController = appController;
            this.jobPositionService = jobPositionService;
            this.basicInfoController = basicInfoController;
            BasicInfoAppLocalizedResources = basicInfoAppLocalizedResources;
            DisplayName = BasicInfoAppLocalizedResources.JobPositionListViewTitle;
            init();

        }

        #endregion

        #region Methods

        void init()
        {
            JobPositions = new PagedSortableCollectionView<JobPositionDTOWithActions>();
            JobPositions.OnRefresh += (s, args) => Load();
            JobPositionCommands = new List<DataGridCommandViewModel>
            {
                   CommandHelper.GetControlCommands(this, appController, new List<int>{ (int) ActionType.AddJobPosition}).FirstOrDefault()
            };
        }

        private List<DataGridCommandViewModel> createCommands()
        {
            var filterCommand = new List<DataGridCommandViewModel>();
            appController.PMSActions.Where(a => SelectedJobPosition.ActionCodes.Contains((int)a.ActionCode)).ForEach(
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

        public void Load()
        {
            ShowBusyIndicator("در حال دریافت اطلاعات...");
            jobPositionService.GetAllJobPositions(
                (res, exp) => appController.BeginInvokeOnDispatcher(() =>
                {
                    HideBusyIndicator();
                    if (exp == null)
                    {
                        JobPositions.SourceCollection = res.Result;
                        JobPositions.TotalItemCount = res.TotalCount;
                        JobPositions.PageIndex = Math.Max(0, res.CurrentPage - 1);
                        HideBusyIndicator();
                    }
                    else
                    {
                        HideBusyIndicator();
                        appController.HandleException(exp);
                    } 
                        
                }), jobPositions.PageSize, jobPositions.PageIndex + 1);
        }


        protected override void OnRequestClose()
        {
            base.OnRequestClose();
            appController.Close(this);
        }

        
        public void Handle(UpdateJobPositionListArgs eventData)
        {
            Load();
        }

        #endregion


    }
}
