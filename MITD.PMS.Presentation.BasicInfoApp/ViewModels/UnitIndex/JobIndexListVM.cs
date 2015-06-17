using System.ComponentModel;
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
    public class JobIndexListVM : BasicInfoWorkSpaceViewModel, IEventHandler<UpdateJobIndexListArgs>
    {
        #region Fields

        private readonly IBasicInfoController basicInfoController;
        private readonly IPMSController appController;
        private readonly IJobIndexServiceWrapper jobIndexService;
        
        #endregion

        #region Properties
         
        private PagedSortableCollectionView<JobIndexDTOWithCategoryAndActions> jobIndexes;
        public PagedSortableCollectionView<JobIndexDTOWithCategoryAndActions> JobIndexes
        {
            get { return jobIndexes; }
            set { this.SetField(p => p.JobIndexes, ref jobIndexes, value); }
        }

        private JobIndexDTOWithCategoryAndActions selectedJobIndex;
        public JobIndexDTOWithCategoryAndActions SelectedJobIndex
        {
            get { return selectedJobIndex; }
            set { this.SetField(p => p.SelectedJobIndex, ref selectedJobIndex, value); }
        }

        private JobIndexCategoryDescription selectedJobIndexCategory;
        public JobIndexCategoryDescription SelectedJobIndexCategory
        {
            get { return selectedJobIndexCategory; }
            set { this.SetField(p => p.SelectedJobIndexCategory, ref selectedJobIndexCategory, value); }
        }

        private ObservableCollection<JobIndexCategoryDescription> jobIndexCategories; 
        public ObservableCollection<JobIndexCategoryDescription> JobIndexCategories
        {
            get { return jobIndexCategories; }
            set { this.SetField(p => p.JobIndexCategories, ref jobIndexCategories, value); }
        }

        private CommandViewModel filterCommand;
        public CommandViewModel FilterCommand
        {
            get
            {
                if (filterCommand == null)
                {
                    filterCommand = new CommandViewModel(BasicInfoAppLocalizedResources.JobIndexListViewFilterCommandTitle,
                                            new DelegateCommand(refresh));
                }
                return filterCommand;
            }
        }

        private ReadOnlyCollection<DataGridCommandViewModel> jobIndexCommands;
        public ReadOnlyCollection<DataGridCommandViewModel> JobIndexCommands
        {
            get
            {
                if (jobIndexCommands == null)
                {
                    var cmds = createCommands();
                    jobIndexCommands = new ReadOnlyCollection<DataGridCommandViewModel>(cmds);
                }
                return jobIndexCommands;
            }
            private set
            {
                if (jobIndexCommands == value) return;
                jobIndexCommands = value;
                OnPropertyChanged("JobIndexCommands");
            }

        }

        private JobIndexCriteria jobIndexCriteria;
        public JobIndexCriteria JobIndexCriteria
        {
            get { return jobIndexCriteria; }
            set { this.SetField(p => p.JobIndexCriteria, ref jobIndexCriteria, value); }
        }


        #endregion

        #region Constructors

        public JobIndexListVM()
        {
            init();
            JobIndexes.Add(new JobIndexDTOWithCategoryAndActions{Name = "ehsan"});
            
            
        }

        public JobIndexListVM(IBasicInfoController basicInfoController, IPMSController appController,
            IJobIndexServiceWrapper jobIndexService, IBasicInfoAppLocalizedResources localizedResources)
        {
            init();
            this.appController = appController;
            this.jobIndexService = jobIndexService;
            this.basicInfoController = basicInfoController;
            BasicInfoAppLocalizedResources = localizedResources;
            DisplayName = BasicInfoAppLocalizedResources.JobIndexListViewTitle;
        }

        #endregion

        #region Methods

        void init()
        {
            JobIndexCriteria=new JobIndexCriteria();
            JobIndexes = new PagedSortableCollectionView<JobIndexDTOWithCategoryAndActions>();
            JobIndexes.OnRefresh += (s, args) => refresh();
        }

       
        private List<DataGridCommandViewModel> createCommands()
        {
            var filterCommands = new List<DataGridCommandViewModel>();
            appController.PMSActions.Where(a => SelectedJobIndex.ActionCodes.Contains((int)a.ActionCode)).ForEach(
                action => filterCommands.Add(new DataGridCommandViewModel
                {
                    CommandViewModel = new CommandViewModel(action.ActionName,
                                                            new DelegateCommand(
                                                                () => action.DoAction(SelectedJobIndex),
                                                                () => true)),
                    Icon = action.ActionIcon
                }));

            return filterCommands;
        }

        public void Load()
        {
            refresh();
        }

        private void refresh()
        {
            var sortBy = JobIndexes.SortDescriptions.ToDictionary(sortDesc => sortDesc.PropertyName, sortDesc =>
                (sortDesc.Direction == ListSortDirection.Ascending ? "ASC" : "DESC"));
            ShowBusyIndicator("در حال دریافت اطلاعات...");

            jobIndexService.GetAllJobIndexes(
                  (res, exp) =>
                  {
                      HideBusyIndicator();
                      if (exp == null)
                      {
                          jobIndexes.SourceCollection = res.Result;
                          jobIndexes.TotalItemCount = res.TotalCount;
                          jobIndexes.PageIndex = Math.Max(0, res.CurrentPage - 1);
                      }
                      else appController.HandleException(exp);
                  }, jobIndexes.PageSize, jobIndexes.PageIndex + 1,sortBy,JobIndexCriteria);
        }

        protected override void OnPropertyChanged(string propertyName)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == "SelectedJobIndex" && JobIndexes.Count > 0)
            {
                JobIndexCommands = new ReadOnlyCollection<DataGridCommandViewModel>(createCommands());
                if (View != null)
                    ((JobIndexListView)View).CreateContextMenu(JobIndexCommands);
            }
        }

        protected override void OnRequestClose()
        {
            base.OnRequestClose();
            appController.Close(this);
        }

        
        public void Handle(UpdateJobIndexListArgs eventData)
        {
            Load();
        }

        #endregion


    }
}
