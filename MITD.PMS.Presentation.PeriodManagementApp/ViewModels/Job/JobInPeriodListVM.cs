using System;
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
    public class JobInPeriodListVM : WorkspaceViewModel, IEventHandler<UpdateJobInPeriodListArgs>
    {
        #region Fields

        private readonly IPeriodController periodController;
        private readonly IPMSController appController;
        private readonly IJobInPeriodServiceWrapper jobInPeriodService;
        private readonly IPeriodServiceWrapper periodService;

        #endregion

        #region Properties & Back Fields

        private PeriodDTO period;
        public PeriodDTO Period
        {
            get { return period; }
            set
            {
                this.SetField(p => p.Period, ref period, value);
            }
        }

        private PagedSortableCollectionView<JobInPeriodDTOWithActions> jobList;
        public PagedSortableCollectionView<JobInPeriodDTOWithActions> JobList
        {
            get { return jobList; }
            set
            {
                this.SetField(p => p.JobList, ref jobList, value);
            }
        }

        private JobInPeriodDTOWithActions selectedJobInPeriod;
        public JobInPeriodDTOWithActions SelectedJobInPeriod
        {
            get { return selectedJobInPeriod; }
            set
            {
                this.SetField(p => p.SelectedJobInPeriod, ref selectedJobInPeriod, value);
                if (selectedJobInPeriod == null) return;
                JobInPeriodCommands = createCommands();
                if (View != null)
                    ((IJobInPeriodListView)View).CreateContextMenu(new ReadOnlyCollection<DataGridCommandViewModel>(JobInPeriodCommands));
            }
        }

        private List<DataGridCommandViewModel> jobInPeriodCommands;
        public List<DataGridCommandViewModel> JobInPeriodCommands
        {
            get { return jobInPeriodCommands; }
            private set
            {
                this.SetField(p => p.JobInPeriodCommands, ref jobInPeriodCommands, value);
                if (JobInPeriodCommands.Count > 0) SelectedCommand = JobInPeriodCommands[0];
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
        public JobInPeriodListVM()
        {
            JobList.Add(new JobInPeriodDTOWithActions() { Name = "شغل 1", JobId = 1 });
            selectedJobInPeriod = new JobInPeriodDTOWithActions() { JobId = 1, Name = "شغل1", DictionaryName = "jobDic1" };
            init();
        }

        public JobInPeriodListVM(IPeriodController periodController, IPMSController appController,
            IJobInPeriodServiceWrapper jobInPeriodService, IPeriodServiceWrapper periodService)
        {
            this.appController = appController;
            this.jobInPeriodService = jobInPeriodService;
            this.periodService = periodService;
            this.periodController = periodController;
            init();

        }
        #endregion

        #region Methods
        void init()
        {
            DisplayName = "مدیریت شغل در دوره";
            JobList = new PagedSortableCollectionView<JobInPeriodDTOWithActions> { PageSize = 20 };
            JobList.OnRefresh += (s, args) => Load(Period);
            JobInPeriodCommands = new List<DataGridCommandViewModel>
            {
                   CommandHelper.GetControlCommands(this, appController, new List<int>{ (int) ActionType.AddJobInPeriod}).FirstOrDefault()
            };
        }

        private List<DataGridCommandViewModel> createCommands()
        {
            return CommandHelper.GetControlCommands(this, appController, SelectedJobInPeriod.ActionCodes);
        }

        public void Load(PeriodDTO periodParam)
        {
            Period = periodParam;
            DisplayName = "مدیریت شغل در دوره" + " " + Period.Name;
            jobInPeriodService.GetAllJobInPeriodWithPagination(
                (res, exp) => appController.BeginInvokeOnDispatcher(() =>
                    {
                        {
                            HideBusyIndicator();
                            if (exp == null)
                            {
                                //PeriodJobs = res;
                                JobList.SourceCollection = res.Result;
                                JobList.TotalItemCount = res.TotalCount;
                                JobList.PageIndex = Math.Max(0, res.CurrentPage - 1);
                            }
                            else appController.HandleException(exp);
                        }
                    }), Period.Id, JobList.PageSize, JobList.PageIndex + 1);
        }

        protected override void OnRequestClose()
        {
            base.OnRequestClose();
            appController.Close(this);
        }

        public void Handle(UpdateJobInPeriodListArgs eventData)
        {
            Load(Period);
        }
        #endregion


    }

}
