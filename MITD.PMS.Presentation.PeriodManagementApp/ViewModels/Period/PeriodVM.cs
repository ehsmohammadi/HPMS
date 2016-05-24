using System;
using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.PeriodManagementApp;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public sealed class PeriodVM : PeriodMgtWorkSpaceViewModel
    {
        #region Fields
    
        private readonly IPMSController appController;
        private readonly IPeriodServiceWrapper periodService;
        private ActionType actionType;

        #endregion

        #region Properties & BackField

        private PeriodDTO period;
        public PeriodDTO Period
        {
            get { return period; }
            set { this.SetField(vm => vm.Period, ref period, value); }
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

        public PeriodVM()
        {
            
            PeriodMgtAppLocalizedResources=new PeriodMgtAppLocalizedResources();
            init();
            Period = new PeriodDTO { Name = "دوره اول", StartDate = DateTime.Now, EndDate = DateTime.Now };

        }

        public PeriodVM(IPMSController appController, 
                        IPeriodServiceWrapper periodService,
                        IPeriodMgtAppLocalizedResources periodMgtAppLocalizedResources)
        {
            this.appController = appController;
            this.periodService = periodService;
            PeriodMgtAppLocalizedResources = periodMgtAppLocalizedResources;
            init();

        } 

        #endregion

        #region Methods

        private void init()
        {
            Period=new PeriodDTO();
            DisplayName = PeriodMgtAppLocalizedResources.PeriodViewTitle;
        }

        public void Load(PeriodDTO periodParam,ActionType actionTypeParam)
        {
            actionType = actionTypeParam;
            if (actionType == ActionType.ModifyPeriod)
            {
                ShowBusyIndicator("در حال دریافت اطلاعات");
                periodService.GetPeriod((res, exp) =>appController.BeginInvokeOnDispatcher(()=>
                    {
                        HideBusyIndicator();
                        if (exp == null)
                        {
                            Period = res;
                            Period.StartDate = res.StartDate;
                        }

                        else
                            appController.HandleException(exp);
                    }),
                                        periodParam.Id);
            }
        }
      
        private void save()
        {
            if (!period.Validate()) return;

            ShowBusyIndicator();
            if (actionType==ActionType.AddPeriod)
            {
                periodService.AddPeriod((res, exp) => appController.BeginInvokeOnDispatcher(() =>
                    {
                        HideBusyIndicator();
                        if (exp != null)
                            appController.HandleException(exp);
                        else
                            finalizeAction();
                    }), Period);
            }
            else if (actionType == ActionType.ModifyPeriod)
            {
                periodService.UpdatePeriod((res, exp) => appController.BeginInvokeOnDispatcher(() =>
                    {
                        HideBusyIndicator();
                        if (exp != null)
                            appController.HandleException(exp);
                        else
                            finalizeAction();
                    }), Period);
            }
        }
        
        private void finalizeAction()
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
