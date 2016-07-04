using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.PeriodManagementApp;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public class JobIndexInquiryListVM : PeriodMgtWorkSpaceViewModel
    {
        #region Fields

        private readonly IPMSController appController;
        private readonly IInquiryServiceWrapper inquiryService;


        #endregion

        #region Properties

        private string inquirerEmployeeNo;
        public string InquirerEmployeeNo { get { return inquirerEmployeeNo; } }

        private long periodId;
        public long PeriodId { get { return periodId; } }

        private ObservableCollection<InquiryIndexDTO> inquiryIndices;
        public ObservableCollection<InquiryIndexDTO> InquiryIndices
        {
            get { return inquiryIndices; }
            set { this.SetField(p => p.InquiryIndices, ref inquiryIndices, value); }
        }

        private InquiryIndexDTO selectedInquiryIndex;
        public InquiryIndexDTO SelectedInquiryIndex
        {
            get { return selectedInquiryIndex; }
            set
            {
                this.SetField(p => p.SelectedInquiryIndex, ref selectedInquiryIndex, value);
                if (selectedInquiryIndex == null) return;
                InquiryIndexCommands = createCommands();
                if (View != null)
                    ((IJobIndexInquiryListView)View).CreateContextMenu(new ReadOnlyCollection<DataGridCommandViewModel>(InquiryIndexCommands));
            }
        }

        private List<DataGridCommandViewModel> inquiryIndexCommands;
        public List<DataGridCommandViewModel> InquiryIndexCommands
        {
            get { return inquiryIndexCommands; }
            private set
            {
                this.SetField(p => p.InquiryIndexCommands, ref inquiryIndexCommands, value);
                if (InquiryIndexCommands.Count > 0) SelectedCommand = InquiryIndexCommands[0];
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

        public JobIndexInquiryListVM()
        {
            PeriodMgtAppLocalizedResources = new PeriodMgtAppLocalizedResources();
            init();
        }

        public JobIndexInquiryListVM(IPMSController appController,
                            IInquiryServiceWrapper inquiryService,
                            IPeriodMgtAppLocalizedResources periodMgtAppLocalizedResources)
        {

            this.appController = appController;
            this.inquiryService = inquiryService;
            PeriodMgtAppLocalizedResources = periodMgtAppLocalizedResources;
            init();


        }

        #endregion

        #region Methods

        void init()
        {
            DisplayName = "لیست شاخص ها برای نظر سنجی";
            InquiryIndices = new ObservableCollection<InquiryIndexDTO>();
            SelectedInquiryIndex = new InquiryIndexDTO();
        }

        private List<DataGridCommandViewModel> createCommands()
        {
            if (SelectedInquiryIndex != null)
                return CommandHelper.GetControlCommands(this, appController, SelectedInquiryIndex.ActionCodes);
            return new List<DataGridCommandViewModel>();
        }

        public void Load(string inquirerEmployeeNoParam, long periodIdParam)
        {
            periodId = periodIdParam;
            if (!string.IsNullOrWhiteSpace(inquirerEmployeeNoParam))
            {
                inquirerEmployeeNo = inquirerEmployeeNoParam;
                refresh();
            }
            else
            {
                appController.ShowMessage("شماره پرسنلی شما در سستم موجود نمی باشد");
            }
        }

        private void refresh()
        {
            ShowBusyIndicator("در حال دریافت اطلاعات...");
            inquiryService.GetInquirerInquiryIndices(
                (res, exp) =>  appController.BeginInvokeOnDispatcher(() =>
                {
                    HideBusyIndicator();
                    if (exp == null)
                    {
                        InquiryIndices = new ObservableCollection<InquiryIndexDTO>(res);
                        SelectedInquiryIndex = res.FirstOrDefault();
                    }
                    else appController.HandleException(exp);
                }), periodId, inquirerEmployeeNo);
        }

        protected override void OnRequestClose()
        {
            base.OnRequestClose();
            appController.Close(this);
        }

        #endregion
    }

}
