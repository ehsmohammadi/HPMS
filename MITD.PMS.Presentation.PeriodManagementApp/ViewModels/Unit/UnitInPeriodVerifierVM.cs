using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic.Wrapper.PeriodManagement.UnitIndex;
using MITD.PMS.Presentation.PeriodManagementApp;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public sealed class UnitInPeriodVerifierVM : PeriodMgtWorkSpaceViewModel
    {

        #region Command

        private CommandViewModel addVerifierCommand;
        public CommandViewModel AddVerifierCommand
        {
            get
            {
                if (addVerifierCommand == null)
                {
                    addVerifierCommand = new CommandViewModel("افزودن تاييد كننده", new DelegateCommand(add));
                }
                return addVerifierCommand;
            }
        }


        private CommandViewModel removeVerifierCommand;
        public CommandViewModel RemoveVerifierCommand
        {
            get
            {
                if (removeVerifierCommand == null)
                {
                    removeVerifierCommand = new CommandViewModel("حذف تاييد كننده", new DelegateCommand(remove));
                }
                return removeVerifierCommand;
            }
        }

        private CommandViewModel searchCommand;
        public CommandViewModel SearchCommand
        {
            get
            {
                if (searchCommand == null)
                {
                    searchCommand = new CommandViewModel("جستجو...", new DelegateCommand(getAllEmployee));
                }
                return searchCommand;
            }
        }

        #endregion

        #region Fields

        private readonly IPMSController appController;
        private readonly IEmployeeServiceWrapper employeeService;
        private readonly IUnitInPeriodServiceWrapper unitInPeriodService;
        private readonly IUnitIndexInPeriodServiceWrapper unitIndexInPeriodService;
        #endregion

        #region Properties & Back Fields

        private PeriodDTO period;
        public PeriodDTO Period
        {
            get { return period; }
            set { this.SetField(s => s.Period, ref period, value); }
        }

        private UnitInPeriodDTO unitInPeriod;
        public UnitInPeriodDTO UnitInPeriod
        {
            get { return unitInPeriod; }
            set { this.SetField(s => s.UnitInPeriod, ref unitInPeriod, value); }
        }



        private PagedSortableCollectionView<EmployeeDTOWithActions> employees;
        public PagedSortableCollectionView<EmployeeDTOWithActions> Employees
        {
            get { return employees; }
            set { this.SetField(vm => vm.Employees, ref employees, value); }
        }

        private EmployeeDTOWithActions selectedEmployee;
        public EmployeeDTOWithActions SelectedEmployee
        {
            get { return selectedEmployee; }
            set { this.SetField(vm => vm.SelectedEmployee, ref selectedEmployee, value); }
        }


        private UnitVerifierDTO selectedVerifier;
        public UnitVerifierDTO SelectedVerifier
        {
            get { return selectedVerifier; }
            set { this.SetField(vm => vm.SelectedVerifier, ref selectedVerifier, value); }
        }


        private EmployeeCriteria employeeCriteria;
        public EmployeeCriteria EmployeeCriteria
        {
            get { return employeeCriteria; }
            set { this.SetField(p => p.EmployeeCriteria, ref employeeCriteria, value); }
        }




        //private List<InquirySubjectInquirersUnitViewModel> inquirySubjectWithInquirers;
        //public List<InquirySubjectInquirersUnitViewModel> InquirySubjectWithInquirers
        //{
        //    get { return inquirySubjectWithInquirers; }
        //    set { this.SetField(vm => vm.InquirySubjectWithInquirers, ref inquirySubjectWithInquirers, value); }
        //}



        #endregion

        #region Constructors

        public UnitInPeriodVerifierVM()
        {
            PeriodMgtAppLocalizedResources = new PeriodMgtAppLocalizedResources();
            init();

        }

        public UnitInPeriodVerifierVM(IPMSController appController,
                             IEmployeeServiceWrapper employeeService,
                             IUnitInPeriodServiceWrapper unitInPeriodService,
                             IPeriodMgtAppLocalizedResources periodMgtAppLocalizedResources,
                             IUnitIndexInPeriodServiceWrapper unitIndexInPeriodServiceWrapper)
        {
            this.appController = appController;
            this.employeeService = employeeService;
            this.unitInPeriodService = unitInPeriodService;
            this.unitIndexInPeriodService = unitIndexInPeriodServiceWrapper;
            PeriodMgtAppLocalizedResources = periodMgtAppLocalizedResources;
            init();

        }

        #endregion

        #region Methods

        private void init()
        {
            DisplayName = "مديريت تاييد كننده هاي واحد ";
            employeeCriteria = new EmployeeCriteria();
            Employees = new PagedSortableCollectionView<EmployeeDTOWithActions>();
            SelectedVerifier = new UnitVerifierDTO();
            Employees.OnRefresh += (s, args) => getAllEmployee();
        }

        private void load()
        {
            ShowBusyIndicator("در حال دریافت اطلاعات...");
            unitInPeriodService.GetUnitInPeriod((res, exp) => appController.BeginInvokeOnDispatcher(() =>
            {
                HideBusyIndicator();
                if (exp == null)
                {
                    UnitInPeriod = res;

                }
            }), Period.Id, UnitInPeriod.UnitId);
        }

        public void Load(PeriodDTO periodDTOParam, UnitInPeriodDTO unitInPeriodParam)
        {

            Period = periodDTOParam;
            UnitInPeriod = unitInPeriodParam;
            load();
            getAllEmployee();
        }


        private void getAllEmployee()
        {
            ShowBusyIndicator("در حال بارگذاری اطلاعات");
            employeeService.GetAllEmployees(
               (res, exp) => appController.BeginInvokeOnDispatcher(() =>
               {
                   HideBusyIndicator();
                   if (exp == null)
                   {

                       Employees.TotalItemCount = res.TotalCount;
                       Employees.PageIndex = Math.Max(0, res.CurrentPage - 1);
                       Employees.SourceCollection = res.Result;


                   }
                   else appController.HandleException(exp);
               }), Period.Id, EmployeeCriteria, Employees.PageSize, Employees.PageIndex + 1);



           
        }

        private void remove()
        {
            if (SelectedVerifier == null)
            {
                appController.ShowMessage("لطفا برای تاييد كننده آن را انتخاب کنید");
                return;
            }
            unitInPeriodService.DeleteVerifier((res, exp) => appController.BeginInvokeOnDispatcher(() =>
            {
                if (exp == null)
                {
                    appController.ShowMessage("تاييد كننده  مورد نظر حذف شد");
                    load();
                }
                else
                {
                    appController.HandleException(exp);
                }
            }), Period.Id, UnitInPeriod.UnitId, SelectedVerifier.EmployeeNo);


        }
        private void add()
        {
            if (SelectedEmployee == null)
            {
                appController.ShowMessage("لطفا برای اضافه کرد تاييد كننده فرد مورد نظر خود را انتخاب کنید");
                return;
            }
            unitInPeriodService.AddVerifier((res, exp) => appController.BeginInvokeOnDispatcher(() =>
                {
                    if (exp == null)
                    {
                        appController.ShowMessage("تاييد كننده ها برای واحد مورد نظر بروز رسانی شد");
                        load();
                    }
                    else
                    {
                        appController.HandleException(exp);
                    }

                }), Period.Id, UnitInPeriod.UnitId, SelectedEmployee.PersonnelNo);

        }

        protected override void OnRequestClose()
        {
            base.OnRequestClose();
            appController.Close(this);
        }

        #endregion


    }

}
