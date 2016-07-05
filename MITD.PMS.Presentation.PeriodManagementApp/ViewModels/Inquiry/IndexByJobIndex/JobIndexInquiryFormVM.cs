using System.Collections.Generic;
using System.Windows;
using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.PeriodManagementApp;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public sealed class JobIndexInquiryFormVM : PeriodMgtWorkSpaceViewModel 
    {

        #region Fields

        private readonly IPMSController appController;
        private readonly IInquiryServiceWrapper inquiryService;
        private ActionType actionType;
        private long periodId;
        
        #endregion

        #region Properties

        private PeriodDTO period;
        public PeriodDTO Period
        {
            get { return period; }
            set { this.SetField(vm => vm.Period, ref period, value); }
        }

        private InquirySubjectInquiryFormListDTO inquirySubjectInquirers;
        public InquirySubjectInquiryFormListDTO InquirySubjectInquirers
        {
            get { return inquirySubjectInquirers; }
            set { this.SetField(vm => vm.InquirySubjectInquirers, ref inquirySubjectInquirers, value); }
        }

        private InquiryFormInquirerDTO selectedInquirer;
        public InquiryFormInquirerDTO SelectedInquirer
        {
            get { return selectedInquirer; }
            set { this.SetField(vm => vm.SelectedInquirer, ref selectedInquirer, value); }
        }

        private InquiryFormByIndexDTO inquiryForm;
        public InquiryFormByIndexDTO InquiryForm
        {
            get { return inquiryForm; }
            set { this.SetField(vm => vm.InquiryForm, ref inquiryForm, value); }
        }


        private CommandViewModel saveCommand;
        public CommandViewModel SaveCommand
        {
            get
            {
                if (saveCommand == null)
                {
                    saveCommand = new CommandViewModel("ذخیره", new DelegateCommand(save));
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
                    cancelCommand = new CommandViewModel("انصراف", new DelegateCommand(OnRequestClose));
                }
                return cancelCommand;
            }
        } 

        #endregion

        #region Constructors

        public JobIndexInquiryFormVM()
        {
            PeriodMgtAppLocalizedResources=new PeriodMgtAppLocalizedResources();
            init();
            //Period = new Period { Name = "دوره اول", StartDate = DateTime.Now, EndDate = DateTime.Now };
        }
        public JobIndexInquiryFormVM(IPMSController appController, 
                          IInquiryServiceWrapper inquiryService,
                          IPeriodMgtAppLocalizedResources localizedResources)
        {
            this.appController = appController;
            this.inquiryService = inquiryService;
            PeriodMgtAppLocalizedResources = localizedResources;
            init();
            
        } 

        #endregion

        #region Methods

        private void init()
        {
            inquirySubjectInquirers = new InquirySubjectInquiryFormListDTO();
        }


        public void Load(InquiryFormByIndexDTO inquiryFormDTOParam, ActionType actionTypeParam)
        {
            periodId = inquiryFormDTOParam.PeriodId;
            actionType = actionTypeParam;
            InquiryForm = inquiryFormDTOParam;
            DisplayName = "فرم نظرسنجی" + " ";
            foreach (var jobIndexValueDTO in InquiryForm.EmployeeValueList)
            {
                jobIndexValueDTO.Grades = fillGradesList();
            }
        }

        private List<Grade> fillGradesList()
        {
            return new List<Grade>
            {
                new Grade("عالی", "100"),
                new Grade("خوب", "80"),
                new Grade("مورد انتظار", "60"),
                new Grade("نیاز به آموزش و مراقبت", "40"),
                new Grade("نامطلوب", "20")
            };
        }

       
        private void save()
        {
            ShowBusyIndicator();
            UserStateDTO userState = appController.CurrentUserState;
            //InquiryForm.JobIndexValueList = SelectedInquirer.JobIndexValueList;
            //inquiryService.UpdateInquirySubjectForm((res, exp) => appController.BeginInvokeOnDispatcher(() =>
            //        {
            //            HideBusyIndicator();
            //            if (exp != null)
            //                appController.HandleException(exp);
            //            else
            //            {
            //                OnRequestClose();
            //                appController.ShowMessage("فرم نظرسنجی ثبت شد");
            //            }

            //        }), inquiryForm);
            
        }
       
        protected override void OnRequestClose()
        {
            base.OnRequestClose();
            appController.Close(this);
        } 

        #endregion
    }
    
}
