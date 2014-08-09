using System;
using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.PeriodManagementApp;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public sealed class JobIndexGroupInPeriodVM : PeriodMgtWorkSpaceViewModel
    {
        #region Fields
    
        private readonly IPMSController appController;
        private readonly IJobIndexInPeriodServiceWrapper jobIndexService;
        private ActionType actionType;

        #endregion

        #region Properties & BackField

        private JobIndexGroupInPeriodDTO jobIndexGroupInPeriod;
        public JobIndexGroupInPeriodDTO JobIndexGroupInPeriod
        {
            get { return jobIndexGroupInPeriod; }
            set { this.SetField(vm => vm.JobIndexGroupInPeriod, ref jobIndexGroupInPeriod, value); }
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

        public JobIndexGroupInPeriodVM()
        {
            
            PeriodMgtAppLocalizedResources=new PeriodMgtAppLocalizedResources();
            init();
            JobIndexGroupInPeriod = new JobIndexGroupInPeriodDTO {Name = "دوره اول"};

        }

        public JobIndexGroupInPeriodVM(IPMSController appController,
                        IJobIndexInPeriodServiceWrapper jobIndexService,
                        IPeriodMgtAppLocalizedResources periodMgtAppLocalizedResources)
        {
            this.appController = appController;
            this.jobIndexService = jobIndexService;
            PeriodMgtAppLocalizedResources = periodMgtAppLocalizedResources;
            init();

        } 

        #endregion

        #region Methods

        private void init()
        {
            JobIndexGroupInPeriod=new JobIndexGroupInPeriodDTO();
            DisplayName = PeriodMgtAppLocalizedResources.JobIndexGroupInPeriodViewTitle;
        }

        public void Load(JobIndexGroupInPeriodDTO jobIndexGroupInPeriodParam,ActionType actionTypeParam)
        {
            actionType = actionTypeParam;
            JobIndexGroupInPeriod = jobIndexGroupInPeriodParam;
            //if (actionType == ActionType.ModifyPeriod)
            //{
            //    ShowBusyIndicator();
            //    jobIndexService.GetPeriod((res, exp) =>
            //        {
            //            HideBusyIndicator();
            //            if (exp == null)
            //                JobIndexGroupInPeriod = res;
            //            else
            //                appController.HandleException(exp);
            //        },
            //                            periodParam.Id);
            //}
        }
      
        private void save()
        {
            if (!JobIndexGroupInPeriod.Validate()) return;

            ShowBusyIndicator();
            if (actionType == ActionType.AddJobIndexGroupInPeriod)
            {
                jobIndexService.AddJobIndexGroupInPeriod((res, exp) => appController.BeginInvokeOnDispatcher(() =>
                    {
                        HideBusyIndicator();
                        if (exp != null)
                            appController.HandleException(exp);
                        else
                            finalizeAction();
                    }), JobIndexGroupInPeriod);
            }
            else if (actionType == ActionType.ModifyJobIndexGroupInPeriod)
            {
                jobIndexService.UpdateJobIndexGroupInPeriod((res, exp) => appController.BeginInvokeOnDispatcher(() =>
                    {
                        HideBusyIndicator();
                        if (exp != null)
                            appController.HandleException(exp);
                        else
                            finalizeAction();
                    }), JobIndexGroupInPeriod);
            }
        }
        
        private void finalizeAction()
        {
            appController.Publish(new UpdateJobIndexInPeriodTreeArgs());
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
