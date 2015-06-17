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
    public class JobIndexCategoryListVM : BasicInfoWorkSpaceViewModel, IEventHandler<UpdateJobIndexCategoryListArgs>
    {
        #region Fields

        private readonly IBasicInfoController basicInfoController;
        private readonly IPMSController appController;
        private readonly IJobIndexCategoryServiceWrapper jobIndexCategoryService;

        



        #endregion

        #region Properties

        private IBasicInfoAppLocalizedResources basicInfoAppLocalizedResources;

        public IBasicInfoAppLocalizedResources BasicInfoAppLocalizedResources
        {
            get { return basicInfoAppLocalizedResources; }
            set { this.SetField(p => p.BasicInfoAppLocalizedResources, ref basicInfoAppLocalizedResources, value); }
        }

        private PagedSortableCollectionView<JobIndexCategoryDTOWithActions> jobIndexCategories;
        public PagedSortableCollectionView<JobIndexCategoryDTOWithActions> JobIndexCategories
        {
            get { return jobIndexCategories; }
            set { this.SetField(p => p.JobIndexCategories, ref jobIndexCategories, value); }
        }

        private JobIndexCategoryDTOWithActions selectedJobIndexCategory;
        public JobIndexCategoryDTOWithActions SelectedJobIndexCategory
        {
            get { return selectedJobIndexCategory; }
            set { this.SetField(p => p.SelectedJobIndexCategory, ref selectedJobIndexCategory, value); }
        }


        private ReadOnlyCollection<DataGridCommandViewModel> jobIndexCategoryCommands;
        public ReadOnlyCollection<DataGridCommandViewModel> JobIndexCategoryCommands
        {
            get
            {
                if (jobIndexCategoryCommands == null)
                {
                    var cmds = createCommands();
                    jobIndexCategoryCommands = new ReadOnlyCollection<DataGridCommandViewModel>(cmds);
                }
                return jobIndexCategoryCommands;
            }
            private set
            {
                if (jobIndexCategoryCommands == value) return;
                jobIndexCategoryCommands = value;
                OnPropertyChanged("JobIndexCategoryCommands");
            }

        }

        #endregion

        #region Constructors

        public JobIndexCategoryListVM()
        {
            init();
            JobIndexCategories.Add(new JobIndexCategoryDTOWithActions { Id = 4, Name = "Test" });
        }

        public JobIndexCategoryListVM(IBasicInfoController basicInfoController, IPMSController appController,
            IJobIndexCategoryServiceWrapper jobIndexCategoryService, IBasicInfoAppLocalizedResources basicInfoAppLocalizedResources)
        {
            init();
            this.appController = appController;
            this.jobIndexCategoryService = jobIndexCategoryService;
            this.BasicInfoAppLocalizedResources = basicInfoAppLocalizedResources;
            this.basicInfoController = basicInfoController;
            DisplayName = basicInfoAppLocalizedResources.JobIndexCategoryListViewTitle;

        }

        #endregion

        #region Methods

        void init()
        {
            jobIndexCategories = new PagedSortableCollectionView<JobIndexCategoryDTOWithActions>();
            jobIndexCategories.OnRefresh += (s, args) => Load();
        }

        private List<DataGridCommandViewModel> createCommands()
        {
            var filterCommand = new List<DataGridCommandViewModel>();
            appController.PMSActions.Where(a => SelectedJobIndexCategory.ActionCodes.Contains((int)a.ActionCode)).ForEach(
                action => filterCommand.Add(new DataGridCommandViewModel
                {

                    CommandViewModel = new CommandViewModel(action.ActionName,
                                                            new DelegateCommand(
                                                                () => action.DoAction(SelectedJobIndexCategory),
                                                                () => true)),
                    Icon = action.ActionIcon
                }));

            return filterCommand;
        }

        public void Load()
        {
            jobIndexCategoryService.GetAllJobIndexCategorys(
                 (res, exp) =>
                 {
                     HideBusyIndicator();
                     if (exp == null)
                     {
                         //jobIndexCategories.SourceCollection = res.Result;
                         jobIndexCategories.TotalItemCount = res.TotalCount;
                         jobIndexCategories.PageIndex = Math.Max(0, res.CurrentPage - 1);
                     }
                     else appController.HandleException(exp);
                 }, jobIndexCategories.PageSize, jobIndexCategories.PageIndex + 1);
        }

        protected override void OnPropertyChanged(string propertyName)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == "SelectedJobIndexCategory" && JobIndexCategories.Count > 0)
            {
                JobIndexCategoryCommands = new ReadOnlyCollection<DataGridCommandViewModel>(createCommands());
                if (View != null)
                    ((JobIndexCategoryListView)View).CreateContextMenu(JobIndexCategoryCommands);
            }
        }

        protected override void OnRequestClose()
        {
            base.OnRequestClose();
            appController.Close(this);
        }


        public void Handle(UpdateJobIndexCategoryListArgs eventData)
        {
            Load();
        }

        #endregion

    }
}
