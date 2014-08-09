using System;
using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.PeriodManagementApp;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public sealed class PrepareToExecInquiryVM : PeriodMgtWorkSpaceViewModel
    {
        #region Fields
    
        private readonly IPMSController appController;
        private readonly IPeriodServiceWrapper periodService;
        private ActionType actionType;

        private long periodId;

        #endregion

        #region Properties & BackField

        private bool isIAgree;
        public bool IsIAgree
        {
            get { return isIAgree; }
            set { this.SetField(vm => vm.IsIAgree, ref isIAgree, value); }
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

        public PrepareToExecInquiryVM()
        {
            
            PeriodMgtAppLocalizedResources=new PeriodMgtAppLocalizedResources();
            init();
           
        }

        public PrepareToExecInquiryVM(IPMSController appController, 
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
            DisplayName = PeriodMgtAppLocalizedResources.PeriodViewTitle;
        }

        public void Load(long periodIdParam)
        {
            periodId = periodIdParam;
        }
      
        private void save()
        {
            //if (!IsIAgree)
            //{
            //    appController.ShowMessage("برای تغییر وضعیت دوره با شرایط فوق ابتدا تیک تایید را بزنید");
            //    return;
            //}
            
            //ShowBusyIndicator();
            //periodService.ChangePeriodStateToBeginInquiry((res, exp) => appController.BeginInvokeOnDispatcher(() =>
            //        {
            //            HideBusyIndicator();
            //            if (exp != null)
            //                appController.HandleException(exp);
            //            else
            //                finalizeAction();
            //        }), periodId);
            
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
