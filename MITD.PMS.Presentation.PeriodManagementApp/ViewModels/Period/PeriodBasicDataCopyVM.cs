
using System.Collections.Generic;
using System.Linq;
using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.PeriodManagementApp;
using MITD.Presentation;
using System.Collections.ObjectModel;

namespace MITD.PMS.Presentation.Logic
{
    public class PeriodBasicDataCopyVM : PeriodMgtWorkSpaceViewModel
    {
        #region Fields

        private readonly IPMSController appController;
        private readonly IPeriodController periodController;
        private readonly IPeriodServiceWrapper periodService;

        #endregion

        #region Properties & BackField

        private PeriodDTO period;
        public PeriodDTO Period
        {
            get { return period; }
            set { this.SetField(vm => vm.Period, ref period, value); }
        }

        private ObservableCollection<PeriodDescriptionDTO> sourcePeriods;
        public ObservableCollection<PeriodDescriptionDTO> SourcePeriods
        {
            get { return sourcePeriods; }
            set { this.SetField(vm => vm.SourcePeriods, ref sourcePeriods, value); }
        }



        private PeriodDescriptionDTO selectedSourcePeriod;
        public PeriodDescriptionDTO SelectedSourcePeriod
        {
            get { return selectedSourcePeriod; }
            set { this.SetField(vm => vm.SelectedSourcePeriod, ref selectedSourcePeriod, value); }
        }

        private CommandViewModel copyCommand;
        public CommandViewModel CopyCommand
        {
            get
            {
                if (copyCommand == null)
                {
                    copyCommand = new CommandViewModel("کپی اطلاعات پایه", new DelegateCommand(save));
                }
                return copyCommand;
            }
        }

        private CommandViewModel cancelCommand;
        public CommandViewModel CancelCommand
        {
            get
            {
                if (cancelCommand == null)
                {
                    cancelCommand = new CommandViewModel("انصراف", new DelegateCommand(OnRequestClose));
                }
                return cancelCommand;
            }
        }



        #endregion

        #region Constructors

        public PeriodBasicDataCopyVM()
        {
            PeriodMgtAppLocalizedResources = new PeriodMgtAppLocalizedResources();
            init();
        }

        public PeriodBasicDataCopyVM(IPeriodServiceWrapper periodService,
                           IPMSController appController,
                           IPeriodMgtAppLocalizedResources periodMgtAppLocalizedResources,
                            IPeriodController periodController
                           )
        {

            this.periodService = periodService;
            this.appController = appController;
            this.periodController = periodController;
            PeriodMgtAppLocalizedResources = periodMgtAppLocalizedResources;
            init();
        }

        #endregion

        #region Methods

        private void init()
        {
            SelectedSourcePeriod = new PeriodDescriptionDTO { };
            DisplayName = PeriodMgtAppLocalizedResources.PeriodBasicDataCopyViewTitle;
        }

        public void Load(PeriodDTO periodParam)
        {
            Period = periodParam;
            preLoad();
        }

        private void preLoad()
        {
            loadAllPeriods();
        }

        private void loadAllPeriods()
        {
            ShowBusyIndicator("در حال دریافت اطلاعات");
            periodService.GetAllPeriods((res, exp) => appController.BeginInvokeOnDispatcher(() =>
                {
                    HideBusyIndicator();
                    if (exp == null)
                        SourcePeriods = new ObservableCollection<PeriodDescriptionDTO>(res.Where(p => p.Id != Period.Id).ToList());
                    else
                    {
                        appController.HandleException(exp);
                    }
                }));
        }



        private void save()
        {
            PeriodStateDTO state = new PeriodStateDTO() { State = (int)PeriodStateEnum.BasicDataCopying };
            periodService.CopyBasicDataFrom((exp) => appController.BeginInvokeOnDispatcher(() =>
            {
                HideBusyIndicator();
                if (exp == null)
                {
                    finalizeAction();
                    periodController.ShowPeriodBasicDataCopyStatusView(period);
                }
                else
                    appController.HandleException(exp);
            }), SelectedSourcePeriod.Id, Period.Id, state);

        }

        private void finalizeAction()
        {
            // appController.Publish(new UpdatePeriodListArgs());
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

