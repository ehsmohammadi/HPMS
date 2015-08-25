using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Castle.Core.Internal;
using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic.Wrapper.PeriodManagement.UnitIndex;
using MITD.PMS.Presentation.PeriodManagementApp;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public sealed class UnitInPeriodInquiryVM : PeriodMgtWorkSpaceViewModel
    {

        #region Command
        private CommandViewModel addInquirerCommand;
        public CommandViewModel AddInquirerCommand
        {
            get
            {
                if (addInquirerCommand == null)
                {
                    addInquirerCommand = new CommandViewModel("افزودن نظر دهنده دلخواه", new DelegateCommand(Add));
                }
                return addInquirerCommand;
            }
        }

        private CommandViewModel removeInquirerCommand;
        public CommandViewModel RemoveInquirerCommand
        {
            get
            {
                if (removeInquirerCommand == null)
                {
                    removeInquirerCommand = new CommandViewModel("حذف نظر دهنده دلخواه", new DelegateCommand(Remove));
                }
                return removeInquirerCommand;
            }
        }

        //private CommandViewModel saveCommand;
        //public CommandViewModel SaveCommand
        //{
        //    get
        //    {
        //        if (saveCommand == null)
        //        {
        //            saveCommand = new CommandViewModel("تایید", new DelegateCommand(getSubjectsInquirers));
        //        }
        //        return saveCommand;
        //    }
        //}

        private CommandViewModel searchCommand;
        public CommandViewModel SearchCommand
        {
            get
            {
                if (searchCommand == null)
                {
                    searchCommand = new CommandViewModel("جستجو...", new DelegateCommand(getSubjectsInquirers));
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
        private ObservableCollection<UnitInPeriodUnitIndexDTO> unitIndexInPeriodList = new ObservableCollection<UnitInPeriodUnitIndexDTO>();
        public ObservableCollection<UnitInPeriodUnitIndexDTO> UnitIndexInPeriodList
        {
            get { return unitIndexInPeriodList; }
            set { this.SetField(vm => vm.UnitIndexInPeriodList, ref unitIndexInPeriodList, value); }
        }
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


        private UnitInPeriodUnitIndexDTO _unitInPeriodUnitIndexDto;
        public UnitInPeriodUnitIndexDTO UnitInPeriodUnitIndexDto
        {
            get { return _unitInPeriodUnitIndexDto; }
            set { this.SetField(s => s.UnitInPeriodUnitIndexDto, ref _unitInPeriodUnitIndexDto, value); }
        }

        //Todo change
        private List<EmployeeDTO> _employees;
        public List<EmployeeDTO> Employees
        {
            get { return _employees; }
            set { this.SetField(vm => vm.Employees, ref _employees, value); }
        }

        private PagedSortableCollectionView<EmployeeDTOWithActions> _inquirers;
        public PagedSortableCollectionView<EmployeeDTOWithActions> Inquirers
        {
            get { return _inquirers; }
            set { this.SetField(vm => vm.Inquirers, ref _inquirers, value); }
        }

        private EmployeeDTOWithActions _inquirer;
        public EmployeeDTOWithActions Inquirer
        {
            get { return _inquirer; }
            set { this.SetField(vm => vm.Inquirer, ref _inquirer, value); }
        }
        private InquiryUnitDTO _selectedCustomInquirer;
        public InquiryUnitDTO SelectedCustomInquirer
        {
            get { return _selectedCustomInquirer; }
            set { this.SetField(vm => vm.SelectedCustomInquirer, ref _selectedCustomInquirer, value); }
        }


        private EmployeeCriteria employeeCriteria;
        public EmployeeCriteria EmployeeCriteria
        {
            get { return employeeCriteria; }
            set { this.SetField(p => p.EmployeeCriteria, ref employeeCriteria, value); }
        }




        private List<InquirySubjectInquirersUnitViewModel> inquirySubjectWithInquirers;
        public List<InquirySubjectInquirersUnitViewModel> InquirySubjectWithInquirers
        {
            get { return inquirySubjectWithInquirers; }
            set { this.SetField(vm => vm.InquirySubjectWithInquirers, ref inquirySubjectWithInquirers, value); }
        }



        #endregion

        #region Constructors

        public UnitInPeriodInquiryVM()
        {
            PeriodMgtAppLocalizedResources = new PeriodMgtAppLocalizedResources();
            init();
           
        }

        public UnitInPeriodInquiryVM(IPMSController appController,
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
            employeeCriteria = new EmployeeCriteria();
            Inquirers = new PagedSortableCollectionView<EmployeeDTOWithActions>();
            SelectedCustomInquirer = new InquiryUnitDTO();
            UnitInPeriodUnitIndexDto=new UnitInPeriodUnitIndexDTO();
            Inquirers.OnRefresh += (s, args) => getSubjectsInquirers();
        }

        private void Load()
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
            Load();
            DisplayName = PeriodMgtAppLocalizedResources.UnitInPeriodInquiryView + " " +
                          unitInPeriodParam.Name;
            getSubjectsInquirers();
        }

    
        private void getSubjectsInquirers()
        {
            ShowBusyIndicator("در حال بارگذاری اطلاعات");
            employeeService.GetAllEmployees(
               (res, exp) => appController.BeginInvokeOnDispatcher(() =>
               {
                   HideBusyIndicator();
                   if (exp == null)
                   {

                       Inquirers.TotalItemCount = res.TotalCount;
                       Inquirers.PageIndex = Math.Max(0, res.CurrentPage - 1);
                       Inquirers.SourceCollection = res.Result;


                   }
                   else appController.HandleException(exp);
               }), Period.Id, EmployeeCriteria, Inquirers.PageSize, Inquirers.PageIndex + 1);



            //unitInPeriodService.GetInquirySubjectWithInquirers((res, exp) => appController.BeginInvokeOnDispatcher(() =>
            //{
            //    HideBusyIndicator();
            //    if (exp == null)
            //    {
            //        employeeService.GetAllEmployees((employeeRes, employeeExp) => appController.BeginInvokeOnDispatcher(() =>
            //        {

            //            if (employeeExp == null)
            //            {
            //                Employees = employeeRes;
            //                Inquirers=new List<InquirerDTO>();
            //                Employees.ForEach(c=>Inquirers.Add(new InquirerDTO()
            //                {
            //                    EmployeeNo = c.PersonnelNo,
            //                    FullName = c.FullName
            //                }));

            //                //InquirySubjectWithInquirers =
            //                //    res.Select(i => new InquirySubjectInquirersUnitViewModel(unitInPeriodService, appController, employeeService)
            //                //    {
            //                //        Period = Period,
            //                //        UnitInPeriodDto = UnitInPeriod,
            //                //        SelectedInquirySubjectWithInquirers = i,
            //                //        Employees =
            //                //            employeeRes.Where(e => !(i.Inquirers.Select(j => j.EmployeeNo).Contains(e.PersonnelNo))).ToList()
            //                //    }).ToList();
            //            }
            //            else
            //                appController.HandleException(employeeExp);
            //        }), Period.Id);
            //    }
            //    else
            //        appController.HandleException(exp);
            //}), Period.Id, unitInPeriod.UnitId);
        }

        private void Remove()
        {
            unitInPeriodService.DeleteInquirer((res, exp) => appController.BeginInvokeOnDispatcher(() =>
            {
                if (exp == null)
                {
                    appController.ShowMessage("نظر دهنده  مورد نظر حذف شد");
                    Load();
                }
                else
                {
                    appController.HandleException(exp);
                }
            }), Period.Id, UnitInPeriod.UnitId, SelectedCustomInquirer.EmployeeNo);
        }
        private void Add()
        {
            unitInPeriodService.AddInquirer((res, exp) => appController.BeginInvokeOnDispatcher(() =>
                {
                    if (exp == null)
                    {
                        appController.ShowMessage("نظر دهنده ها برای واحد مورد نظر بروز رسانی شد");
                        Load();
                    }
                    else
                    {
                        appController.HandleException(exp);
                    }



                }), Period.Id, UnitInPeriod.UnitId, Inquirer.PersonnelNo,UnitInPeriodUnitIndexDto.Id);

        }

        protected override void OnRequestClose()
        {
            base.OnRequestClose();
            appController.Close(this);
        }

        #endregion


    }

}
