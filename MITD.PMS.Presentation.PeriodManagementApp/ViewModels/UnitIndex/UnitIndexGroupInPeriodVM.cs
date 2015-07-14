using System;
using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic.Wrapper.PeriodManagement.UnitIndex;
using MITD.PMS.Presentation.PeriodManagementApp;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public sealed class UnitIndexGroupInPeriodVM : PeriodMgtWorkSpaceViewModel
    {
        #region Fields
    
        private readonly IPMSController appController;
        private readonly IUnitIndexInPeriodServiceWrapper unitIndexService;
        private ActionType actionType;

        #endregion

        #region Properties & BackField

        private UnitIndexGroupInPeriodDTO unitIndexGroupInPeriod;
        public UnitIndexGroupInPeriodDTO UnitIndexGroupInPeriod
        {
            get { return unitIndexGroupInPeriod; }
            set { this.SetField(vm => vm.UnitIndexGroupInPeriod, ref unitIndexGroupInPeriod, value); }
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

        #endregion

        #region Constructors

        public UnitIndexGroupInPeriodVM()
        {
            
            PeriodMgtAppLocalizedResources=new PeriodMgtAppLocalizedResources();
            init();
            UnitIndexGroupInPeriod = new UnitIndexGroupInPeriodDTO {Name = "دوره اول"};

        }

        public UnitIndexGroupInPeriodVM(IPMSController appController,
                        IUnitIndexInPeriodServiceWrapper unitIndexService,
                        IPeriodMgtAppLocalizedResources periodMgtAppLocalizedResources)
        {
            this.appController = appController;
            this.unitIndexService = unitIndexService;
            PeriodMgtAppLocalizedResources = periodMgtAppLocalizedResources;
            init();

        } 

        #endregion

        #region Methods

        private void init()
        {
            UnitIndexGroupInPeriod=new UnitIndexGroupInPeriodDTO();
            DisplayName = PeriodMgtAppLocalizedResources.UnitIndexGroupInPeriodViewTitle;
        }

        public void Load(UnitIndexGroupInPeriodDTO unitIndexGroupInPeriodParam,ActionType actionTypeParam)
        {
            actionType = actionTypeParam;
            UnitIndexGroupInPeriod = unitIndexGroupInPeriodParam;
            //if (actionType == ActionType.ModifyPeriod)
            //{
            //    ShowBusyIndicator();
            //    unitIndexService.GetPeriod((res, exp) =>
            //        {
            //            HideBusyIndicator();
            //            if (exp == null)
            //                UnitIndexGroupInPeriod = res;
            //            else
            //                appController.HandleException(exp);
            //        },
            //                            periodParam.Id);
            //}
        }
      
        private void save()
        {
            if (!UnitIndexGroupInPeriod.Validate()) return;

            ShowBusyIndicator();
            if (actionType == ActionType.AddUnitIndexGroupInPeriod)
            {
                unitIndexService.AddUnitIndexGroupInPeriod((res, exp) => appController.BeginInvokeOnDispatcher(() =>
                    {
                        HideBusyIndicator();
                        if (exp != null)
                            appController.HandleException(exp);
                        else
                            finalizeAction();
                    }), UnitIndexGroupInPeriod);
            }
            else if (actionType == ActionType.ModifyUnitIndexGroupInPeriod)
            {
                unitIndexService.UpdateUnitIndexGroupInPeriod((res, exp) => appController.BeginInvokeOnDispatcher(() =>
                    {
                        HideBusyIndicator();
                        if (exp != null)
                            appController.HandleException(exp);
                        else
                            finalizeAction();
                    }), UnitIndexGroupInPeriod);
            }
        }
        
        private void finalizeAction()
        {
            appController.Publish(new UpdateUnitIndexInPeriodTreeArgs());
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
