using System.Linq;
using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.PeriodManagementApp;
using MITD.Presentation;
using System.Collections.ObjectModel;

namespace MITD.PMS.Presentation.Logic
{
    public class UnitInPeriodVM : PeriodMgtWorkSpaceViewModel
    {
        #region Fields

        private readonly IPMSController appController;
        private readonly IUnitServiceWrapper unitService;
        private readonly IUnitInPeriodServiceWrapper unitInPeriodService;
        private ActionType actionType;

        #endregion

        #region Properties & BackField


        private UnitInPeriodAssignmentDTO unitInPeriod;
        public UnitInPeriodAssignmentDTO UnitInPeriod
        {
            get { return unitInPeriod; }
            set { this.SetField(vm => vm.UnitInPeriod, ref unitInPeriod, value); }
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

        private ObservableCollection<UnitDTO> units;
        public ObservableCollection<UnitDTO> Units
        {
            get { return units; }
            set { this.SetField(vm => vm.Units, ref units, value); }
        }

        #endregion

        #region Constructors

        public UnitInPeriodVM()
        {
            PeriodMgtAppLocalizedResources = new PeriodMgtAppLocalizedResources();
            init();
        }

        public UnitInPeriodVM( IUnitInPeriodServiceWrapper unitInPeriodService, 
                           IPMSController appController,
            IUnitServiceWrapper unitService,
                           IPeriodMgtAppLocalizedResources periodMgtAppLocalizedResources)
        {
           
            this.unitInPeriodService = unitInPeriodService;
            this.appController = appController;
            this.unitService = unitService;
            PeriodMgtAppLocalizedResources = periodMgtAppLocalizedResources;
            
            init();
        } 

        private void init()
        {
            UnitInPeriod = new UnitInPeriodAssignmentDTO { };
            DisplayName = PeriodMgtAppLocalizedResources.UnitInPeriodViewTitle;
        }

        #endregion

        #region Methods

        public void Load(UnitInPeriodAssignmentDTO unitInPeriodParam,ActionType actionTypeParam)
        {
            preLoad();
            actionType = actionTypeParam;
            UnitInPeriod = unitInPeriodParam;
        }

        private void preLoad()
        {
           unitService.GetAllUnits((units, exp) =>appController.BeginInvokeOnDispatcher(()=>
           {
               if (exp == null)
               {
                   unitInPeriodService.GetAllUnits((unitsInperiod, exp1) =>appController.BeginInvokeOnDispatcher(()=>
                   {
                       if (exp1 == null)
                       {
                           Units =
                               new ObservableCollection<UnitDTO>(
                                   units.Where(u => !unitsInperiod.Select(up => up.UnitId).Contains(u.Id)));
                       }
                       else
                           appController.HandleException(exp1);

                   }), UnitInPeriod.PeriodId);
               }
               else
                   appController.HandleException(exp);
           }));
           
        }

        private void save()
        {
            if (!unitInPeriod.Validate()) return;

            ShowBusyIndicator();
            if (actionType==ActionType.AddUnitInPeriod)
            {
                unitInPeriodService.AddUnitInPeriod((res, exp) => appController.BeginInvokeOnDispatcher(() =>
                    {
                        HideBusyIndicator();
                        if (exp != null)
                            appController.HandleException(exp);
                        else
                            FinalizeAction();
                    }), unitInPeriod);
            }
        }

        private void FinalizeAction()
        {
            appController.Publish(new UpdateUnitInPeriodTreeArgs());
            OnRequestClose();
        }

        protected override void OnRequestClose()
        {
            base.OnRequestClose();
            appController.Close(this);
        } 

        #endregion
    }
}

