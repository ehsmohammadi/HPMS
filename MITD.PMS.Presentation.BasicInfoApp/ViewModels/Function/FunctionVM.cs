using MITD.PMS.Presentation.BasicInfoApp;
using MITD.PMS.Presentation.Contracts;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public sealed class FunctionVM : BasicInfoWorkSpaceViewModel
    {
        #region Fields
       
        private readonly IPMSController appController;       
        private readonly IFunctionServiceWrapper functionService;

        private ActionType actionType;
        
        #endregion

        #region Properties & BackField

        private FunctionDTO function;
        public FunctionDTO Function
        {
            get { return function; }
            set { this.SetField(vm => vm.Function, ref function, value); }
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

        public FunctionVM()
        {
          BasicInfoAppLocalizedResources = new BasicInfoAppLocalizedResources();
          Function = new FunctionDTO{};
          init();

        }
        public FunctionVM(IPMSController appController, IFunctionServiceWrapper functionService,IBasicInfoAppLocalizedResources basicInfoAppLocalizedResources)
        {
            this.appController = appController;
            this.functionService = functionService;
            BasicInfoAppLocalizedResources = basicInfoAppLocalizedResources; 
            init();
        } 
        

        #endregion

        #region Methods

        private void init()
        {
            DisplayName = BasicInfoAppLocalizedResources.FunctionViewTitle;
        }

        public void Load(FunctionDTO functionParam,ActionType actionTypeParam)
        {
            actionType = actionTypeParam;
            Function = functionParam;
            if (actionType == ActionType.ModifyFunction )
            {
                ShowBusyIndicator();
                functionService.GetFunction( (res, exp) => appController.BeginInvokeOnDispatcher(() =>
                    {
                        HideBusyIndicator();
                        if (exp == null)
                            Function = res;
                        else
                            appController.HandleException(exp);
                    }),
                                        functionParam.Id);
            }
        }
     
        private void save()
        {
            if (!function.Validate()) return;

            ShowBusyIndicator();
            if (actionType == ActionType.CreateFunction)
            {
                functionService.AddFunction((res, exp) => appController.BeginInvokeOnDispatcher(() =>
                    {
                        HideBusyIndicator();
                        if (exp != null)
                            appController.HandleException(exp);
                        else
                            finalizeAction();
                    }), function);
            }
            else if (actionType == ActionType.ModifyFunction)
            {
                functionService.UpdateFunction((res, exp) => appController.BeginInvokeOnDispatcher(() =>
                    {
                        HideBusyIndicator();
                        if (exp != null)
                            appController.HandleException(exp);
                        else
                            finalizeAction();
                    }), function);
            }
        }
        
        private void finalizeAction()
        {
            appController.Publish(new UpdateFunctionListArgs());
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
