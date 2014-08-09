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
    public class UnitListVM : BasicInfoWorkSpaceViewModel, IEventHandler<UpdateUnitListArgs>
    {
        #region Fields

        private readonly IPMSController appController;
        private readonly IUnitServiceWrapper unitService;

        #endregion

        #region Properties & Back fields

        private PagedSortableCollectionView<UnitDTOWithActions> units;
        public PagedSortableCollectionView<UnitDTOWithActions> Units
        {
            get { return units; }
            set { this.SetField(p => p.Units, ref units, value); }
        }

        private UnitDTOWithActions selectedUnit;
        public UnitDTOWithActions SelectedUnit
        {
            get { return selectedUnit; }
            set
            {
                this.SetField(p => p.SelectedUnit, ref selectedUnit, value);
                if (selectedUnit == null) return;
                UnitCommands = createCommands();
                if (View != null)
                    ((IUnitListView)View).CreateContextMenu(new ReadOnlyCollection<DataGridCommandViewModel>(UnitCommands));
            }
        }

        private List<DataGridCommandViewModel> unitCommands;
        public List<DataGridCommandViewModel> UnitCommands
        {
            get { return unitCommands; }
            private set
            {
                this.SetField(p => p.UnitCommands, ref unitCommands, value);
                if (UnitCommands.Count > 0) SelectedCommand = UnitCommands[0];
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

        public UnitListVM()
        {
            init();
            Units.Add(new UnitDTOWithActions { Id = 4, Name = "Test" });
        }

        public UnitListVM(IPMSController appController,
            IUnitServiceWrapper unitService, IBasicInfoAppLocalizedResources basicInfoAppLocalizedResources)
        {
            this.appController = appController;
            this.unitService = unitService;
            BasicInfoAppLocalizedResources = basicInfoAppLocalizedResources;
            DisplayName = BasicInfoAppLocalizedResources.UnitListViewTitle;
            init();

        }

        #endregion

        #region Methods

        void init()
        {
            Units = new PagedSortableCollectionView<UnitDTOWithActions>();
            Units.OnRefresh += (s, args) => Load();
            UnitCommands = new List<DataGridCommandViewModel>
            {
                   CommandHelper.GetControlCommands(this, appController, new List<int>{ (int) ActionType.AddUnit}).FirstOrDefault()
            };
        }

        private List<DataGridCommandViewModel> createCommands()
        {
            return CommandHelper.GetControlCommands(this, appController, SelectedUnit.ActionCodes);
        }

        public void Load()
        {
            ShowBusyIndicator("در حال دریافت اطلاعات...");
            unitService.GetAllUnits(
                (res, exp) => appController.BeginInvokeOnDispatcher(() =>
                {
                    HideBusyIndicator();
                    if (exp == null)
                    {
                        Units.SourceCollection = res.Result;
                        Units.TotalItemCount = res.TotalCount;
                        Units.PageIndex = Math.Max(0, res.CurrentPage - 1);
                        HideBusyIndicator();
                    }
                    else
                    {
                        HideBusyIndicator();
                        appController.HandleException(exp);
                    } 
                        
                }), units.PageSize, units.PageIndex + 1);
        }


        protected override void OnRequestClose()
        {
            base.OnRequestClose();
            appController.Close(this);
        }

        
        public void Handle(UpdateUnitListArgs eventData)
        {
            Load();
        }

        #endregion


    }
}
