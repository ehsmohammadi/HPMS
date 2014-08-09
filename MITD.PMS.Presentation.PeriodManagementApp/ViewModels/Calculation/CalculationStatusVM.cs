using System;
using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.PeriodManagementApp;
using MITD.Presentation;
using System.Windows.Threading;
using System.Threading;
using System.ComponentModel;
using System.Collections.Generic;

namespace MITD.PMS.Presentation.Logic
{
    public class CalculationStatusVM : PeriodMgtWorkSpaceViewModel 
    {
        #region Fields
        private readonly IPMSController appController;
        private readonly IPeriodController periodController;
        private readonly ICalculationServiceWrapper calculationService;
        private ActionType actionType;
        DispatcherTimer timer;
        #endregion

        #region Properties

        private CalculationStateWithRunSummaryDTO summary;
        public CalculationStateWithRunSummaryDTO Summary
        {
            get { return summary; }
            set
            {
                this.SetField(vm => vm.Summary, ref summary, value);
                if(View != null)
                    ((ICalculationStatusView)View).SetMessagesListScrollToBottom();
            }
        }

        private CalculationDTO calculation;
        public CalculationDTO Calculation
        {
            get { return calculation; }
            set { this.SetField(vm => vm.Calculation, ref calculation, value); }
        }

        private CommandViewModel pauseCommand;
        public CommandViewModel PauseCommand
        {
            get
            {
                if (pauseCommand == null)
                {
                    pauseCommand = new CommandViewModel("وقفه",new DelegateCommand(pause));
                }
                return pauseCommand;
            }
        }

        
        private CommandViewModel resumeCommand;
        public CommandViewModel ResumeCommand
        {
            get
            {
                if (resumeCommand == null)
                {
                    resumeCommand = new CommandViewModel("ادامه", new DelegateCommand(resume));
                }
                return resumeCommand;
            }
        }

        private CommandViewModel stopCommand;
        public CommandViewModel StopCommand
        {
            get
            {
                if (stopCommand == null)
                {
                    stopCommand = new CommandViewModel("قطع عملیات", new DelegateCommand(stop));
                }
                return stopCommand;
            }
        }

        private CommandViewModel showErrorsCommand;
        public CommandViewModel ShowErrorsCommand
        {
            get
            {
                if (showErrorsCommand == null)
                {
                    showErrorsCommand = new CommandViewModel("نمایش خطاهای محاسبه", new DelegateCommand(showErrors));
                }
                return showErrorsCommand;
            }
        }

        private CommandViewModel exitCommand;
        private long calculationId;
        public CommandViewModel ExitCommand
        {
            get
            {
                if (exitCommand == null)
                {
                    exitCommand = new CommandViewModel("خروج", new DelegateCommand(OnRequestClose));
                }
                return exitCommand;
            }
        } 

        #endregion

        #region Constructors

        public CalculationStatusVM()
        {
            PeriodMgtAppLocalizedResources = new PeriodMgtAppLocalizedResources();
            Calculation = new CalculationDTO { };
            Summary = new CalculationStateWithRunSummaryDTO
            {
                Percent = 20,
                StateName = "Running",
                MessageList = new List<string>
                { 
                    {"sdkafhsadjfksdajfsdkjf"},
                    {"kfs skfhdjf"}
                }
            };
            init();
        }

        public CalculationStatusVM(IPMSController appController, 
                          IPeriodController periodController,
                          ICalculationServiceWrapper calculationService,
                          IPeriodMgtAppLocalizedResources localizedResources)
        {
            this.appController = appController;
            this.periodController = periodController;
            this.calculationService = calculationService;
            PeriodMgtAppLocalizedResources = localizedResources;
            init();

            timer = new DispatcherTimer();

            timer.Tick +=
                delegate(object s, EventArgs args)
                {
                    timer.Stop();
                    refresh();
                };

            timer.Interval = new TimeSpan(0, 0, 5); 
        } 

        #endregion

        #region Methods

        private void init()
        {
            DisplayName = "وضعیت اجرای محاسبه";
        }

        public void Load(long calculationId)
        {
            this.calculationId = calculationId;
            refresh();
        }

        private void refresh()
        {
            var trigger1 = new AutoResetEvent(false);
            var trigger2 = new AutoResetEvent(false);
            var success = false;
            ShowBusyIndicator();
            string oldStateName = string.Empty;
            if(summary!=null)
                oldStateName = summary.StateName;
            ThreadPool.QueueUserWorkItem(s =>
            {
                calculationService.GetCalculationState((res, exp) =>
                {
                    appController.BeginInvokeOnDispatcher(() =>
                        {
                            success = exp == null;
                            if (exp == null)
                            {
                                Summary = res;
                            }
                            else appController.HandleException(exp);
                            trigger1.Set();
                        });

                }, appController.CurrentPriod.Id, calculationId);

                calculationService.GetCalculation((res, exp) =>
                {
                    appController.BeginInvokeOnDispatcher(() =>
                        {
                            success = exp == null;
                            if (exp == null)
                            {
                                Calculation = res;
                            }
                            else appController.HandleException(exp);
                            trigger2.Set();
                        });
                }, appController.CurrentPriod.Id, calculationId);
                trigger1.WaitOne();
                trigger2.WaitOne();
                appController.BeginInvokeOnDispatcher(() => HideBusyIndicator());
                if (success)
                {
                    if (summary.StateName == "CalculationRunningState")
                        appController.BeginInvokeOnDispatcher(() => timer.Start()); 
                    else if (summary.StateName != oldStateName)
                        appController.BeginInvokeOnDispatcher(() => 
                            appController.Publish<UpdateCalculationListArgs>(new UpdateCalculationListArgs()));
                }
            });
        }

        private void pause()
        {
            calculationService.ChangeCalculationState((exp) => appController.BeginInvokeOnDispatcher(() =>
            {
                if (exp != null)
                    appController.HandleException(exp);
                else
                {
                    appController.Publish(new UpdateCalculationListArgs());
                }

            }), appController.CurrentPriod.Id, Calculation.Id, new CalculationStateDTO { State = (int)CalculationStateEnum.Paused });
        }
        
        private void resume()
        {
            calculationService.ChangeCalculationState((exp) => appController.BeginInvokeOnDispatcher(() =>
            {
                if (exp != null)
                    appController.HandleException(exp);
                else
                {
                    appController.Publish(new UpdateCalculationListArgs());
                }

            }), appController.CurrentPriod.Id, Calculation.Id, new CalculationStateDTO { State = (int)CalculationStateEnum.Running });
        }
        
        private void stop()
        {
            calculationService.ChangeCalculationState((exp) => appController.BeginInvokeOnDispatcher(() =>
            {
                if (exp != null)
                    appController.HandleException(exp);
                else
                {
                    appController.Publish(new UpdateCalculationListArgs());
                }

            }), appController.CurrentPriod.Id, Calculation.Id, new CalculationStateDTO { State = (int)CalculationStateEnum.Canceled });
        }

        private void showErrors()
        {
            periodController.ShowCalculationExceptionListView(Calculation);
        }

        protected override void OnRequestClose()
        {
            base.OnRequestClose();
            appController.Close(this);
        } 

        #endregion
    }
    
}
