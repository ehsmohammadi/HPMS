using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MITD.Core;
using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.PeriodManagementApp;
using MITD.PMS.Presentation.PeriodManagementApp.Views;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public sealed class UnitInPeriodVM : WorkspaceViewModel, IEventHandler<UpdateUnitInPeriodCustomFieldListArgs>,IEventHandler<UpdateUnitInPeriodUnitIndexListArgs>
    {
        #region Fields

        private readonly IPMSController appController;
        private readonly IPeriodController periodController;     
        private readonly IUnitInPeriodServiceWrapper UnitInPeriodService;
        private readonly IUnitServiceWrapper UnitService;
        private readonly ICustomFieldServiceWrapper customFieldService;
        private readonly IPeriodServiceWrapper periodService;
        private ActionType actionType;
    
        #endregion

        #region Properties & Back Fields

        private List<UnitInPeriodDTO> _units;
        public List<UnitInPeriodDTO> Units
        {
            get { return _units; }
            set { this.SetField(vm => vm.Units, ref _units, value); }
        }

        private PeriodDTO period;
        public PeriodDTO Period
        {
            get { return period; }
            set { this.SetField(vm => vm.Period, ref period, value); }
        }

        private UnitInPeriodDTO selectedUnitInPeriod;
        public UnitInPeriodDTO SelectedUnitInPeriod
        {
            get { return selectedUnitInPeriod; }
            set { this.SetField(vm => vm.SelectedUnitInPeriod, ref selectedUnitInPeriod, value); }
        }


        private CommandViewModel addFields;
        public CommandViewModel AddFields
        {
            get
            {
                if (addFields == null )
                {
                    addFields = new CommandViewModel("مدیریت فیلدها واحد",
                                                     //new DelegateCommand(()=>appController.PMSActions.Single(a=>a.ActionCode==ActionType.AddUnitInPrdField).DoAction(UnitInPeriodAssign)));
                                                     new DelegateCommand(
                                                         () =>
                                                         periodController.ShowUnitInPeriodCustomFieldManageView(
                                                             Period.Id, SelectedUnitInPeriod,
                                                             ActionType.ModifyUnitInPeriod)));
                }
                return addFields;
            }
        }


        private CommandViewModel addUnitIndices;
        public CommandViewModel AddUnitIndices
        {
            get
            {
                if (addUnitIndices == null )
                {
                    addUnitIndices = new CommandViewModel("مدیریت شاخص های واحد",
                                                           new DelegateCommand(
                                                         () =>
                                                         periodController.ShowUnitInPeriodUnitIndicesManageView(
                                                             Period.Id, SelectedUnitInPeriod,
                                                             ActionType.ModifyUnitInPeriod)));
                }
                return addUnitIndices;
            }
        }

        private CommandViewModel saveCommand;
        public CommandViewModel SaveCommand
        {
            get
            {
                if (saveCommand == null)
                {
                    saveCommand = new CommandViewModel("تایید", new DelegateCommand(save));
                }
                return saveCommand;
            }
        }

        private CommandViewModel cancelCommand;
        public CommandViewModel CancelCommand
        {
            get
            {
                if (cancelCommand == null)
                {
                    cancelCommand = new CommandViewModel("انصراف",new DelegateCommand(OnRequestClose));
                }
                return cancelCommand;
            }
        }

        private ReadOnlyCollection<DataGridCommandViewModel> _unitInPrdFieldCommands;
        public ReadOnlyCollection<DataGridCommandViewModel> UnitInPrdFieldCommands
        {
            get
            {
                if (_unitInPrdFieldCommands == null)
                {
                    var cmds = createCommands();
                    _unitInPrdFieldCommands = new ReadOnlyCollection<DataGridCommandViewModel>(cmds);
                }
                return _unitInPrdFieldCommands;
            }
            private set
            {
                this.SetField(p => p.UnitInPrdFieldCommands, ref _unitInPrdFieldCommands, value);
            }

        }

        #endregion

        #region Constructors

        public UnitInPeriodVM()
        {
            
        }
        public UnitInPeriodVM(IPMSController appController,
                             IPeriodController periodController,
                             IUnitInPeriodServiceWrapper UnitInPeriodService,
                             IUnitServiceWrapper UnitService,
                             ICustomFieldServiceWrapper customFieldService,
                             IPeriodServiceWrapper periodService
            )
        {
            this.appController = appController;
            this.periodController = periodController;
            this.UnitInPeriodService = UnitInPeriodService;
            this.UnitService = UnitService;
            this.customFieldService = customFieldService;
            this.periodService = periodService;
            DisplayName = "مدیریت واحد در دوره ";
        } 

        #endregion

        #region Methods

        private List<DataGridCommandViewModel> createCommands()
        {
            var filterCommand = new List<DataGridCommandViewModel>();
         

            return filterCommand;
        }

        public void Load(long periodId,long? UnitId,ActionType actionTypeParam)
        {
            
            actionType = actionTypeParam;
            preLoad(periodId,UnitId);
            
            if (UnitId.HasValue) // modify Unit
            {
                Units = new List<UnitInPeriodDTO>();
                ShowBusyIndicator("در حال دریافت اطلاعات...");
                UnitInPeriodService.GetUnitInPeriod((res, exp) =>  appController.BeginInvokeOnDispatcher(() =>
                    {
                        HideBusyIndicator();
                        if (exp == null)
                        {  
                            Units.Add(res);
                            SelectedUnitInPeriod = res;
                        }
                    }),periodId,UnitId.Value);
            }
            else // add new Unit => action is  ActionType.AddUnitInPrdField
            {
                ShowBusyIndicator();
                UnitInPeriodService.GetAllUnitInPeriod((UnitInPeriodListRes, exp) => appController.BeginInvokeOnDispatcher(() => 
                    {
                        if (exp == null)
                        {
                            UnitService.GetAllUnits((UnitsRes, UnitsExp) => appController.BeginInvokeOnDispatcher(() => 
                                {
                                    HideBusyIndicator();
                                   
                                    if (UnitsExp == null)
                                    {
                                        var jList = UnitsRes.Where(r => !UnitInPeriodListRes.Select(jip => jip.UnitId).Contains(r.Id)).ToList();
                                        Units = jList.Select(
                                            j => new UnitInPeriodDTO() { Name = j.Name, UnitId = j.Id, CustomFields = new List<CustomFieldDTO>() }).ToList();

                                    }else
                                        appController.HandleException(UnitsExp);
                                        
                                }));
                            
                        }
                        else
                            appController.HandleException(exp);
                    }),periodId);
                 
              
            }
        }

        private void preLoad(long periodId,long? UnitId)
        {
            ShowBusyIndicator("در حال دریافت اطلاعات...");
            periodService.GetPeriod((res, exp) => appController.BeginInvokeOnDispatcher(() =>
                {
                    HideBusyIndicator();
                    if (exp == null)
                        Period = res;

                }),periodId);

            
            if (!UnitId.HasValue)
                return;
            
            UnitInPeriodService.GetUnitInPeriod((res, exp) => appController.BeginInvokeOnDispatcher(() =>
                {
                    if (exp == null)
                        SelectedUnitInPeriod = res;
                }),periodId,UnitId.Value);
        }

        private void save()
        {
            if (SelectedUnitInPeriod ==null || !SelectedUnitInPeriod.Validate()) 
                return;

            ShowBusyIndicator();
            if (actionType==ActionType.AddUnitInPeriod)
            {
                UnitInPeriodService.AddUnitInPeriod((res, exp) => appController.BeginInvokeOnDispatcher(() =>
                    {
                        HideBusyIndicator();
                        if (exp != null)
                            appController.HandleException(exp);
                        else
                            finalizeAction();
                    }),Period.Id, SelectedUnitInPeriod);
            }
            else if (actionType == ActionType.ModifyUnitInPeriod)
            {
                UnitInPeriodService.UpdateUnitInPeriod((res, exp) => appController.BeginInvokeOnDispatcher(() =>
                    {
                        HideBusyIndicator();
                        if (exp != null)
                            appController.HandleException(exp);
                        else
                            finalizeAction();
                    }), Period.Id, SelectedUnitInPeriod);
            }

        }
        
        private void finalizeAction()
        {
            appController.Publish(new UpdateUnitInPeriodListArgs());
            OnRequestClose();
        }

     

        protected override void OnRequestClose()
        {
            base.OnRequestClose();
            appController.Close(this);
        }

        public void Handle(UpdateUnitInPeriodCustomFieldListArgs eventData)
        {
            if (Period.Id != eventData.PeriodId || SelectedUnitInPeriod.UnitId != eventData.UnitId)
                return;

            SelectedUnitInPeriod.CustomFields = new List<CustomFieldDTO>();
            var fieldIdList = eventData.UnitInPeriodCustomFieldDescriptionList.Select(f => f.Id).ToList();

            customFieldService.GetAllCustomFields((res, exp) => appController.BeginInvokeOnDispatcher(() =>
            {
                if (exp == null)
                    SelectedUnitInPeriod.CustomFields = res.Where(f => fieldIdList.Contains(f.Id)).ToList();
                else
                    appController.HandleException(exp);
            }), "Unit");

        }

        

        
        #endregion

        public void Handle(UpdateUnitInPeriodUnitIndexListArgs eventData)
        {
            if (Period.Id != eventData.PeriodId || SelectedUnitInPeriod.UnitId != eventData.UnitId)
                return;

            SelectedUnitInPeriod.UnitIndices = eventData.UnitInPeriodUnitIndices;

           
        }
    }
    
}
