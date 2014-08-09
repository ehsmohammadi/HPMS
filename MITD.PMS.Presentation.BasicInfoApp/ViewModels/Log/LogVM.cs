using MITD.PMS.Presentation.Contracts;
using MITD.Presentation;
using MITD.PMS.Presentation.BasicInfoApp;

namespace MITD.PMS.Presentation.Logic
{
    public class LogVM : BasicInfoWorkSpaceViewModel
    {
        #region Fields

        private readonly IPMSController appController;
        private readonly ILogServiceWrapper logService;
        private ActionType actionType;

        #endregion

        #region Properties

        private LogDTO log;
        public LogDTO Log
        {
            get { return log; }
            set { this.SetField(vm => vm.Log, ref log, value); }
        }


        private CommandViewModel cancelCommand;
        public CommandViewModel CancelCommand
        {
            get
            {
                if (cancelCommand == null)
                {
                    cancelCommand = new CommandViewModel("بستن",new DelegateCommand(OnRequestClose));
                }
                return cancelCommand;
            }
        } 

        #endregion

        #region Constructors

        public LogVM()
        {
            Log = new LogDTO { Code = "adddd", Title= "exp in jfj" };
        }
        public LogVM(IPMSController appController, ILogServiceWrapper logService)
        {
            this.appController = appController;
            this.logService = logService;
            Log = new LogDTO();
            DisplayName = "لاگ ثبت شده";
        } 

        #endregion

        #region Methods

        public void Load(LogDTO logParam)
        {
            ShowBusyIndicator();
            logService.GetLog((res, exp) => appController.BeginInvokeOnDispatcher(() =>
                {
                    HideBusyIndicator();
                    if (exp == null)
                        Log = res;
                    else
                        appController.HandleException(exp);
                }), logParam.Id);
        }

        private void FinalizeAction()
        {
            appController.Publish(new UpdateLogListArgs());
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

