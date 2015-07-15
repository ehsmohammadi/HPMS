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
    public class UnitIndexTreeVM : BasicInfoWorkSpaceViewModel, IEventHandler<UpdateUnitIndexTreeArgs>
    {

        #region Fields

        private readonly IPMSController appController;
        private readonly IUnitIndexServiceWrapper unitIndexService;

        #endregion

        #region Properties & Back Field

        private ObservableCollection<TreeElementViewModel<AbstractUnitIndexDTOWithActions>> unitIndexTree;
        public ObservableCollection<TreeElementViewModel<AbstractUnitIndexDTOWithActions>> UnitIndexTree
        {
            get { return unitIndexTree; }
            set { this.SetField(p => p.UnitIndexTree, ref unitIndexTree, value); }
        }

        private TreeElementViewModel<AbstractUnitIndexDTOWithActions> selectedunitIndex;
        public TreeElementViewModel<AbstractUnitIndexDTOWithActions> SelectedUnitIndex
        {
            get { return selectedunitIndex; }
            set
            {
                this.SetField(p => p.SelectedUnitIndex, ref selectedunitIndex, value);
                if (selectedunitIndex == null) return;
                UnitIndexCommands = createCommands();
                if (View != null)
                    ((IUnitIndexTreeView)View).CreateContextMenu(new ReadOnlyCollection<DataGridCommandViewModel>(UnitIndexCommands));
            }
        }

        private List<DataGridCommandViewModel> unitIndexCommands;
        public List<DataGridCommandViewModel> UnitIndexCommands
        {
            get { return unitIndexCommands; }
            private set
            {
                this.SetField(p => p.UnitIndexCommands, ref unitIndexCommands, value);
                if (UnitIndexCommands.Count > 0) SelectedCommand = UnitIndexCommands[0];
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
                            var action = appController.PMSActions.Single(a => a.ActionCode == ActionType.AddUnitIndexCategory);
                            SelectedUnitIndex = null;
                            action.DoAction(this);
                        }));
                }
                return addRootCategory;
            }
        }

        #endregion

        #region Constructors

        public UnitIndexTreeVM()
        {
            init();
        }

        public UnitIndexTreeVM(
                               IPMSController appController,
                               IBasicInfoAppLocalizedResources basicInfoAppLocalizedResources,
                               IUnitIndexServiceWrapper unitIndexService)
        {
            this.appController = appController;
            this.unitIndexService = unitIndexService;
            BasicInfoAppLocalizedResources = basicInfoAppLocalizedResources;
            init();
        }

        #endregion

        #region Methods

        void init()
        {
            DisplayName = "مدیریت شاخص های واحد  ";
            UnitIndexTree = new ObservableCollection<TreeElementViewModel<AbstractUnitIndexDTOWithActions>>();
            UnitIndexCommands = new List<DataGridCommandViewModel> {
                new  DataGridCommandViewModel{ CommandViewModel = AddRootCategory}
            };
        }

        private List<DataGridCommandViewModel> createCommands()
        {

            var filterCommand = new List<DataGridCommandViewModel>();
            filterCommand.Add(new DataGridCommandViewModel { CommandViewModel = AddRootCategory });
            if (SelectedUnitIndex != null)
            {
                appController.PMSActions.Where(
                    a => SelectedUnitIndex.Data.ActionCodes.Contains((int)a.ActionCode)).ForEach(
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
            unitIndexService.GetAllAbstractUnitIndex(
                (res, exp) => appController.BeginInvokeOnDispatcher(() =>
                {

                    if (exp == null)
                    {
                        UnitIndexTree = SilverLightTreeViewHelper<AbstractUnitIndexDTOWithActions>.prepareListForTreeView(res);
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

        public void Handle(UpdateUnitIndexTreeArgs eventData)
        {
            Load();
        }

        #endregion


    }

}
