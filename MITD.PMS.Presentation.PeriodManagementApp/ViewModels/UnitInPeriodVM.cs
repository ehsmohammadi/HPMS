using System;
using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.PeriodManagementApp;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public sealed class UnitInPeriodVM : WorkspaceViewModel
    {
        #region Fields

        private PeriodDto period;
        private IPMSController appController;
        private CommandViewModel saveCommand;
        private IPeriodServiceWrapper periodService;
        private ActionEnum actionType;
        private CommandViewModel cancelCommand;

        #endregion

        #region Properties

        //public Period UnitInPeriod
        //{
        //    get { return period; }
        //    set { this.SetField(vm => vm.Period, ref period, value); }
        //}

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

        public UnitInPeriodVM()
        {
            //Period = new PeriodDto { Name = "دوره اول", StartDate = DateTime.Now, EndDate = DateTime.Now };
        }
        public UnitInPeriodVM(IPMSController appController, IPeriodServiceWrapper periodService)
        {
            this.appController = appController;
            this.periodService = periodService;
            //Period = new PeriodDto();
            DisplayName = "دوره ";
        } 

        #endregion

        #region Methods

        public void Load(PeriodDto periodParam,ActionEnum actionTypeParam)
        {
            actionType = actionTypeParam;
            if (actionType == ActionEnum.ModifyPeriod)
            {
                ShowBusyIndicator();
                periodService.GetPeriod((res, exp) =>
                    {
                        //HideBusyIndicator();
                        //if (exp == null)
                        //    Period = res;
                        //else
                        //    appController.HandleException(exp);
                    },
                                        periodParam.Id);
            }
        }

       
        private void save()
        {
            if (!period.Validate()) return;

            ShowBusyIndicator();
            if (actionType==ActionEnum.AddPeriod)
            {
                periodService.AddPeriod((res, exp) => appController.BeginInvokeOnDispatcher(() =>
                    {
                        HideBusyIndicator();
                        if (exp != null)
                            appController.HandleException(exp);
                        else
                            FinalizeAction();
                    }), period);
            }
            else if (actionType == ActionEnum.ModifyPeriod)
            {
                periodService.UpdatePeriod((res, exp) => appController.BeginInvokeOnDispatcher(() =>
                    {
                        HideBusyIndicator();
                        if (exp != null)
                            appController.HandleException(exp);
                        else
                            FinalizeAction();
                    }), period);
            }
        }
        
        private void FinalizeAction()
        {
            appController.Publish(new UpdatePeriodListArgs());
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
