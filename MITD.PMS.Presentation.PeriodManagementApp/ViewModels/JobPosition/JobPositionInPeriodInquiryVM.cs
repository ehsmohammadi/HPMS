using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Castle.Core.Internal;
using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.PeriodManagementApp;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public sealed class JobPositionInPeriodInquiryVM : PeriodMgtWorkSpaceViewModel
    {
        #region Fields

        private readonly IPMSController appController;
        private readonly IEmployeeServiceWrapper employeeService;
        private readonly IJobPositionInPeriodServiceWrapper jobPositionInPeriodService;

        #endregion

        #region Properties & Back Fields

        private PeriodDTO period;
        public PeriodDTO Period
        {
            get { return period; }
            set { this.SetField(s => s.Period, ref period, value); }
        }

        private JobPositionInPeriodDTO jobPositionInPeriod;
        public JobPositionInPeriodDTO JobPositionInPeriod
        {
            get { return jobPositionInPeriod; }
            set { this.SetField(s => s.JobPositionInPeriod, ref jobPositionInPeriod, value); }
        }

        private List<InquirySubjectInquirersViewModel> inquirySubjectWithInquirers;
        public List<InquirySubjectInquirersViewModel> InquirySubjectWithInquirers
        {
            get { return inquirySubjectWithInquirers; }
            set { this.SetField(vm => vm.InquirySubjectWithInquirers, ref inquirySubjectWithInquirers, value); }
        }

        //private InquirySubjectWithInquirersDTO selectedInquirySubjectWithInquirers;
        //public InquirySubjectWithInquirersDTO SelectedInquirySubjectWithInquirers
        //{
        //    get { return selectedInquirySubjectWithInquirers; }
        //    set
        //    {
        //        this.SetField(vm => vm.SelectedInquirySubjectWithInquirers, ref selectedInquirySubjectWithInquirers, value);
        //        if (selectedInquirySubjectWithInquirers != null)
        //        {
        //            Inquirers = new ObservableCollection<InquirerDTO>(SelectedInquirySubjectWithInquirers.Inquirers);
        //            CustomInquirerList = new ObservableCollection<InquirerDTO>(SelectedInquirySubjectWithInquirers.CustomInquirers);
        //        }
        //    }
        //}

        //private ObservableCollection<InquirerDTO> inquirers;
        //public ObservableCollection<InquirerDTO> Inquirers
        //{
        //    get { return inquirers; }
        //    set { this.SetField(vm => vm.Inquirers, ref inquirers, value); }
        //}


        //private InquirerDTO selectedCustomInquirer;
        //public InquirerDTO SelectedCustomInquirer
        //{
        //    get { return selectedCustomInquirer; }
        //    set { this.SetField(vm => vm.SelectedCustomInquirer, ref selectedCustomInquirer, value); }
        //}

        //private ObservableCollection<InquirerDTO> customInquirerList;
        //public ObservableCollection<InquirerDTO> CustomInquirerList
        //{
        //    get { return customInquirerList; }
        //    set { this.SetField(vm => vm.CustomInquirerList, ref customInquirerList, value); }
        //}


        //private EmployeeDTO selectedEmployee;
        //public EmployeeDTO SelectedEmployee
        //{
        //    get { return selectedEmployee; }
        //    set { this.SetField(vm => vm.SelectedEmployee, ref selectedEmployee, value); }
        //}

        //private List<EmployeeDTO> employees;
        //public List<EmployeeDTO> Employees
        //{
        //    get { return employees; }
        //    set { this.SetField(vm => vm.Employees, ref employees, value); }
        //}



        //private CommandViewModel addInquirerCommand;
        //public CommandViewModel AddInquirerCommand
        //{
        //    get
        //    {
        //        if (addInquirerCommand == null)
        //        {
        //            addInquirerCommand = new CommandViewModel("افزودن نظر دهنده دلخواه", new DelegateCommand(addInquirer));
        //        }
        //        return addInquirerCommand;
        //    }
        //}

        //private CommandViewModel removeInquirerCommand;
        //public CommandViewModel RemoveInquirerCommand
        //{
        //    get
        //    {
        //        if (removeInquirerCommand == null)
        //        {
        //            removeInquirerCommand = new CommandViewModel("حذف نظر دهنده دلخواه", new DelegateCommand(removeInquirer));
        //        }
        //        return removeInquirerCommand;
        //    }
        //}

        //private CommandViewModel saveCommand;
        //public CommandViewModel SaveCommand
        //{
        //    get
        //    {
        //        if (saveCommand == null)
        //        {
        //            saveCommand = new CommandViewModel("تایید", new DelegateCommand(save));
        //        }
        //        return saveCommand;
        //    }
        //}

        //private CommandViewModel cancelCommand;
        //public CommandViewModel CancelCommand
        //{
        //    get
        //    {
        //        if (cancelCommand == null)
        //        {
        //            cancelCommand = new CommandViewModel("انصراف", new DelegateCommand(OnRequestClose));
        //        }
        //        return cancelCommand;
        //    }
        //}

        #endregion

        #region Constructors

        public JobPositionInPeriodInquiryVM()
        {
            PeriodMgtAppLocalizedResources = new PeriodMgtAppLocalizedResources();
            init();
        }

        public JobPositionInPeriodInquiryVM(IPMSController appController,
                             IEmployeeServiceWrapper employeeService,
                             IJobPositionInPeriodServiceWrapper jobPositionInPeriodService,
                             IPeriodMgtAppLocalizedResources periodMgtAppLocalizedResources)
        {
            this.appController = appController;
            this.employeeService = employeeService;
            this.jobPositionInPeriodService = jobPositionInPeriodService;
            PeriodMgtAppLocalizedResources = periodMgtAppLocalizedResources;
            init();

        }

        #endregion

        #region Methods

        private void init()
        {
            //Inquirers = new ObservableCollection<InquirerDTO>();
            //CustomInquirerList = new ObservableCollection<InquirerDTO>();

        }

        public void Load(PeriodDTO periodDTOParam, JobPositionInPeriodDTO jobPositionInPeriodParam)
        {

            Period = periodDTOParam;
            JobPositionInPeriod = jobPositionInPeriodParam;
            DisplayName = PeriodMgtAppLocalizedResources.JobPositionInPeriodInquiryView + " " +
                          jobPositionInPeriodParam.Name;
            getSubjectsInquirers();
        }

        //private void preLoad()
        //{
            
        //}

        private void getSubjectsInquirers()
        {
            ShowBusyIndicator("در حال بارگذاری اطلاعات");
            jobPositionInPeriodService.GetInquirySubjectWithInquirers((res, exp) => appController.BeginInvokeOnDispatcher(() =>
            {
                HideBusyIndicator();
                if (exp == null)
                {
                    //InquirySubjectWithInquirers = res;
                    //SelectedInquirySubjectWithInquirers = res.First();
                    //employeeService.GetAllEmployees((employeeRes, employeeExp) =>  appController.BeginInvokeOnDispatcher(() =>
                    employeeService.GetAllEmployees((employeeRes, employeeExp) =>  appController.BeginInvokeOnDispatcher(() =>
                    {

                        if (employeeExp == null)
                        {
                            InquirySubjectWithInquirers =
                                res.Select(i => new InquirySubjectInquirersViewModel(jobPositionInPeriodService,appController,employeeService)
                                {
                                    Period = Period,
                                    JobPositionInPeriod = JobPositionInPeriod,
                                    SelectedInquirySubjectWithInquirers = i,
                                    Employees =
                                        employeeRes.Where(e => !(i.Inquirers.Select(j => j.EmployeeNo).Contains(e.PersonnelNo))).ToList()
                                }).ToList();
                        }
                        else
                            appController.HandleException(employeeExp);
                    }), Period.Id);
                }
                else
                    appController.HandleException(exp);
            }),Period.Id, jobPositionInPeriod.JobPositionId);
        }

        //private void addInquirer()
        //{
        //    if (SelectedEmployee == null || CustomInquirerList.Select(c => c.EmployeeNo).Contains(SelectedEmployee.PersonnelNo))
        //        return;
        //    var customInquirer = new InquirerDTO
        //    {
        //        EmployeeNo = SelectedEmployee.PersonnelNo,
        //        FullName = SelectedEmployee.FirstName + " " + SelectedEmployee.LastName
        //    };
        //    CustomInquirerList.Add(customInquirer);
        //}

        //private void removeInquirer()
        //{
        //    if (SelectedEmployee == null)
        //    {
        //        appController.ShowMessage("هیچ کارمندی برای حذف انتخاب نشده است");
        //        return;
        //    }
        //    CustomInquirerList.Remove(SelectedCustomInquirer);
        //}

        //private void save()
        //{
        //    //SelectedInquirySubjectWithInquirers.CustomInquirers.Clear();
        //    //SelectedInquirySubjectWithInquirers.Inquirers.Clear();
        //    //SelectedInquirySubjectWithInquirers.CustomInquirers.AddRange(CustomInquirerList);
        //    //SelectedInquirySubjectWithInquirers.Inquirers.AddRange(Inquirers);
        //    //jobPositionInPeriodService.UpdateInquirySubjectInquirers((res, exp) => appController.BeginInvokeOnDispatcher(() =>
        //    //{
        //    //    if (exp == null)
        //    //    {
        //    //        Inquirers = new ObservableCollection<InquirerDTO>(res.Inquirers);
        //    //        CustomInquirerList = new ObservableCollection<InquirerDTO>(res.CustomInquirers);
        //    //        SelectedInquirySubjectWithInquirers = res;
        //    //        appController.ShowMessage("نظر دهنده ها برای کارمند مورد نظر بروز رسانی شد");
        //    //    }
        //    //    else
        //    //        appController.HandleException(exp);
                    
        //    //}), Period.Id, JobPositionInPeriod.JobPositionId, SelectedInquirySubjectWithInquirers);

        //}

        protected override void OnRequestClose()
        {
            base.OnRequestClose();
            appController.Close(this);
        }

        #endregion


    }

}
