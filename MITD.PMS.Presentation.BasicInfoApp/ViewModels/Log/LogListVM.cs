using MITD.Core;
using MITD.PMS.Presentation.Contracts;
using MITD.Presentation;
using MITD.PMS.Presentation.BasicInfoApp;
using MITD.PMS.Presentation.BasicInfoApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Castle.Core.Internal;

namespace MITD.PMS.Presentation.Logic
{
    public class LogListVM : BasicInfoWorkSpaceViewModel, IEventHandler<UpdateLogListArgs>
    {
        #region Fields

        private readonly IPMSController appController;
        private readonly ILogServiceWrapper logService;

        #endregion

        #region Properties & Back fields

        private PagedSortableCollectionView<LogDTOWithActions> logs;
        public PagedSortableCollectionView<LogDTOWithActions> Logs
        {
            get { return logs; }
            set { this.SetField(p => p.Logs, ref logs, value); }
        }

        private LogDTOWithActions selectedLog;
        public LogDTOWithActions SelectedLog
        {
            get { return selectedLog; }
            set
            {
                this.SetField(p => p.SelectedLog, ref selectedLog, value);
                if (selectedLog == null) return;
                LogCommands = createCommands();
                if (View != null)
                    ((ILogListView)View).CreateContextMenu(new ReadOnlyCollection<DataGridCommandViewModel>(LogCommands));
            }
        }

        private List<DataGridCommandViewModel> logCommands;
        public List<DataGridCommandViewModel> LogCommands
        {
            get { return logCommands; }
            private set
            {
                this.SetField(p => p.LogCommands, ref logCommands, value);
                if (LogCommands.Count > 0) SelectedCommand = LogCommands[0];
            }

        }

        private DataGridCommandViewModel selectedCommand;
        public DataGridCommandViewModel SelectedCommand
        {
            get { return selectedCommand; }
            set { this.SetField(p => p.SelectedCommand, ref selectedCommand, value); }
        }

        #endregion

        #region Constructors

        public LogListVM()
        {
            init();
            Logs.Add(new LogDTOWithActions { Id = new Guid(), Code = "dffd" });
        }

        public LogListVM(IPMSController appController,
            ILogServiceWrapper logService, IBasicInfoAppLocalizedResources basicInfoAppLocalizedResources)
        {
            this.appController = appController;
            this.logService = logService;
            BasicInfoAppLocalizedResources = basicInfoAppLocalizedResources;
            DisplayName = BasicInfoAppLocalizedResources.LogListViewTitle;
            init();

        }

        #endregion

        #region Methods

        void init()
        {
            Logs = new PagedSortableCollectionView<LogDTOWithActions> { PageSize = 20 };
            Logs.OnRefresh += (s, args) => Load();
            LogCommands = new List<DataGridCommandViewModel>();
        }

        private List<DataGridCommandViewModel> createCommands()
        {
            return CommandHelper.GetControlCommands(this, appController, SelectedLog.ActionCodes);
        }

        public void Load()
        {
            ShowBusyIndicator("در حال دریافت اطلاعات...");
            logService.GetAllLogs(
                (res, exp) => appController.BeginInvokeOnDispatcher(() =>
                {
                    HideBusyIndicator();
                    if (exp == null)
                    {
                        Logs.SourceCollection = res.Result;
                        Logs.TotalItemCount = res.TotalCount;
                        Logs.PageIndex = Math.Max(0, res.CurrentPage - 1);
                        HideBusyIndicator();
                    }
                    else
                    {
                        HideBusyIndicator();
                        appController.HandleException(exp);
                    } 
                        
                }), logs.PageSize, logs.PageIndex + 1);
        }


        protected override void OnRequestClose()
        {
            base.OnRequestClose();
            appController.Close(this);
        }

        
        public void Handle(UpdateLogListArgs eventData)
        {
            Load();
        }

        #endregion


    }
}
