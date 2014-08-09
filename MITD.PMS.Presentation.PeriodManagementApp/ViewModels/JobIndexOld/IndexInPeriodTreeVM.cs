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
    public class IndexInPeriodTreeVM : PeriodMgtWorkSpaceViewModel, IEventHandler<UpdateIndexTreeArgs>
    {

        #region Fields

        private readonly IPMSController appController;
        private readonly IPeriodController periodController;
        //private readonly IIndexInPeriodServiceWrapper indexInPeriodService;
        //private ObservableCollection<TreeElement> indexInPeriodTree;
        //private TreeElement selectedindexInPeriod;

        #endregion

        #region Properties

        //public ObservableCollection<TreeElement> IndexInPeriodTree
        //{
        //    get { return indexInPeriodTree; }
        //    set { this.SetField(p => p.IndexInPeriodTree, ref indexInPeriodTree, value); }
        //}

        //public TreeElement SelectedIndexInPeriod
        //{
        //    get { return selectedindexInPeriod; }
        //    set { this.SetField(p => p.SelectedIndexInPeriod, ref selectedindexInPeriod, value); }
        //}

        #endregion

        #region Constructors

        public IndexInPeriodTreeVM()
        {
            init();
        }

        public IndexInPeriodTreeVM(IPeriodController periodController,
                               IPMSController appController,
                               IMainAppLocalizedResources localizedResources
                               //IIndexInPeriodServiceWrapper indexInPeriodService
            )
        {
            init();
            this.appController = appController;
            //this.indexInPeriodService = indexInPeriodService;
            this.periodController = periodController;
        }

        #endregion

        #region Methods

        void init()
        {
            DisplayName = "مدیریت شاخص ها ";
            //IndexInPeriodTree = new ObservableCollection<TreeElement>();
        }

        private List<DataGridCommandViewModel> createCommands()
        {

            var filterCommand = new List<DataGridCommandViewModel>();
            //if (SelectedIndexInPeriod != null)
            //{
            //    //appController.PMSActions.Where(
            //    //    a => SelectedIndexInPeriod.ActionCodes.Contains((int)a.ActionCode)).ForEach(
            //    //        action => filterCommand.Add(new DataGridCommandViewModel
            //    //            {

            //    //                CommandViewModel = new CommandViewModel(action.ActionName,
            //    //                                                        new DelegateCommand(
            //    //                                                            () =>
            //    //                                                            action.DoAction(
            //    //                                                                SelectedIndexInPeriod),
            //    //                                                            () => true)),
            //    //                Icon = action.ActionIcon
            //    //            }));
            //}
            return filterCommand;

        }

        public void Load()
        {
            ShowBusyIndicator("در حال دریافت اطلاعات...");
            //indexInPeriodService.GetIndexInPeriods(
            //    (res, exp) =>
            //    {

            //        if (exp == null)
            //        {
            //            //IndexInPeriodTree = SilverLightTreeViewHelper.prepareListForTreeView(res );
            //            HideBusyIndicator();
            //        }
            //        else
            //        {
            //            HideBusyIndicator();
            //            appController.HandleException(exp);
            //        }
            //    });
        }

        protected override void OnPropertyChanged(string propertyName)
        {
            base.OnPropertyChanged(propertyName);
            //if (propertyName == "SelectedIndexInPeriod" && IndexInPeriodTree != null)
            //{
            //    if (View != null)
            //        ((IndexInPeriodTreeView)View).CreateContextMenu(
            //            new ReadOnlyCollection<DataGridCommandViewModel>(createCommands())
            //            );
            //}
        }

        protected override void OnRequestClose()
        {
            base.OnRequestClose();
            appController.Close(this);
        }

        public void Handle(UpdateIndexTreeArgs eventData)
        {
            Load();
        }

        #endregion
    }

}
