using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.PeriodManagementApp;
using MITD.PMS.Presentation.PeriodManagementApp.Views;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public class InquirerInquirySubjectListVM : PeriodMgtWorkSpaceViewModel
    {
        #region Fields

        private readonly IPMSController appController;
        private readonly IInquiryServiceWrapper inquiryService;
        private string inquirerEmployeeNo;
        private long periodId;
        #endregion

        #region Properties

        public string InquirerEmployeeNo { get { return inquirerEmployeeNo; } }
        public long PeriodId { get { return periodId; } }

        private ObservableCollection<InquirySubjectDTO> inquirySubjects;
        public ObservableCollection<InquirySubjectDTO> InquirySubjects
        {
            get { return inquirySubjects; }
            set { this.SetField(p => p.InquirySubjects, ref inquirySubjects, value); }
        }

        private InquirySubjectDTO selectedInquirySubject;
        public InquirySubjectDTO SelectedInquirySubject
        {
            get { return selectedInquirySubject; }
            set
            {
                this.SetField(p => p.SelectedInquirySubject, ref selectedInquirySubject, value);
                if (selectedInquirySubject == null) return;
                InquirySubjectCommands = createCommands();
                if (View != null)
                    ((IEmployeesInquiryListView)View).CreateContextMenu(new ReadOnlyCollection<DataGridCommandViewModel>(InquirySubjectCommands));
            }
        }

        private List<DataGridCommandViewModel> inquirySubjectCommands;
        public List<DataGridCommandViewModel> InquirySubjectCommands
        {
            get { return inquirySubjectCommands; }
            private set
            {
                this.SetField(p => p.InquirySubjectCommands, ref inquirySubjectCommands, value);
                if (InquirySubjectCommands.Count > 0) SelectedCommand = InquirySubjectCommands[0];
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

        public InquirerInquirySubjectListVM()
        {
            PeriodMgtAppLocalizedResources = new PeriodMgtAppLocalizedResources();
            init();
        }

        public InquirerInquirySubjectListVM(IPMSController appController,
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
            DisplayName = "لیست افراد آماده برای نظر سنجی";
            InquirySubjects = new ObservableCollection<InquirySubjectDTO>();
            SelectedInquirySubject = new InquirySubjectDTO();
        }

        private List<DataGridCommandViewModel> createCommands()
        {
            if (SelectedInquirySubject != null)
                return CommandHelper.GetControlCommands(this, appController, SelectedInquirySubject.ActionCodes);
            return new List<DataGridCommandViewModel>();
        }

        public void Load(string inquirerEmployeeNoParam, long periodIdParam)
        {
            periodId = periodIdParam;
            inquirerEmployeeNo = inquirerEmployeeNoParam;
            refresh();
        }

        private void refresh()
        {
            ShowBusyIndicator("در حال دریافت اطلاعات...");
            inquiryService.GetInquirerInquirySubjects(
                (res, exp) =>  appController.BeginInvokeOnDispatcher(() =>
                {
                    HideBusyIndicator();
                    if (exp == null)
                    {
                        InquirySubjects = new ObservableCollection<InquirySubjectDTO>(res);
                        SelectedInquirySubject = res.FirstOrDefault();
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
