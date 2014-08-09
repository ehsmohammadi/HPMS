using System;
using System.Threading;
using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.PeriodManagementApp;
using MITD.Presentation;
using System.Windows.Threading;
using System.Collections.Generic;

namespace MITD.PMS.Presentation.Logic
{
    public class PeriodInitializeInquiryStatusVM : PeriodMgtWorkSpaceViewModel
    {
        #region Fields
        private readonly IPMSController appController;
        private readonly IPeriodServiceWrapper periodService;
        private ActionType actionType;
        DispatcherTimer timer;
        #endregion

        #region Properties

        private PeriodStateWithIntializeInquirySummaryDTO summary;
        public PeriodStateWithIntializeInquirySummaryDTO Summary
        {
            get { return summary; }
            set
            {
                this.SetField(vm => vm.Summary, ref summary, value);
                if (View != null)
                    ((IPeriodInitializeInquiryStatusView)View).SetMessagesListScrollToBottom();
            }
        }

        private PeriodDTO period;
        public PeriodDTO Period
        {
            get { return period; }
            set { this.SetField(vm => vm.Period, ref period, value); }
        }



        private CommandViewModel exitCommand;
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

        public PeriodInitializeInquiryStatusVM()
        {
            PeriodMgtAppLocalizedResources = new PeriodMgtAppLocalizedResources();
            Period = new PeriodDTO { };
            Summary = new PeriodStateWithIntializeInquirySummaryDTO
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

        public PeriodInitializeInquiryStatusVM(IPMSController appController,
                          IPeriodServiceWrapper periodService,
                          IPeriodMgtAppLocalizedResources localizedResources)
        {
            this.appController = appController;
            this.periodService = periodService;
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
            DisplayName = "وضعیت اجرای آماده سازی نظرسنجی";
        }

        public void Load(PeriodDTO periodParam, ActionType type)
        {
            Period = periodParam;
            refresh();
        }

        private void refresh()
        {
            var trigger = new AutoResetEvent(false);
            var success = false;
            ShowBusyIndicator();
            string oldStateName = string.Empty;
            if (summary != null)
                oldStateName = summary.StateName;

            ThreadPool.QueueUserWorkItem(s =>
            {

                periodService.GetPeriodStatus((res, exp) => appController.BeginInvokeOnDispatcher(() =>
                {
                    success = exp == null;
                    if (exp == null)
                    {
                        Summary = res;
                    }
                    else appController.HandleException(exp);
                    trigger.Set();
                }), Period.Id);
                trigger.WaitOne();
                appController.BeginInvokeOnDispatcher(HideBusyIndicator);
                if (success)
                {
                    if (summary.StateName == "PeriodInitializingForInquiryState")
                        appController.BeginInvokeOnDispatcher(() => timer.Start());
                    else if (summary.StateName != oldStateName)
                        appController.BeginInvokeOnDispatcher(() =>
                            appController.Publish(new UpdatePeriodListArgs()));
                }
            });

        }

        protected override void OnRequestClose()
        {
            base.OnRequestClose();
            appController.Close(this);
        }

        #endregion
    }

}
