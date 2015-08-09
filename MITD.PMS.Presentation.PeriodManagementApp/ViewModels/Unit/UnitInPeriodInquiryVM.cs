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
    public sealed class UnitInPeriodInquiryVM : PeriodMgtWorkSpaceViewModel
    {
        #region Fields

        private readonly IPMSController appController;
        private readonly IEmployeeServiceWrapper employeeService;
        private readonly IUnitInPeriodServiceWrapper unitInPeriodService;

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

        //Todo change
        private List<EmployeeDTO> _employees;
        public List<EmployeeDTO> Employees
        {
            get { return _employees; }
            set { this.SetField(vm => vm.Employees, ref _employees, value); }
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
                             IPeriodMgtAppLocalizedResources periodMgtAppLocalizedResources)
        {
            this.appController = appController;
            this.employeeService = employeeService;
            this.unitInPeriodService = unitInPeriodService;
            PeriodMgtAppLocalizedResources = periodMgtAppLocalizedResources;
            init();

        }

        #endregion

        #region Methods

        private void init()
        {
           
        }

        public void Load(PeriodDTO periodDTOParam, UnitInPeriodDTO unitInPeriodParam)
        {

            Period = periodDTOParam;
            UnitInPeriod = unitInPeriodParam;
            DisplayName = PeriodMgtAppLocalizedResources.UnitInPeriodInquiryView + " " +
                          unitInPeriodParam.Name;
            getSubjectsInquirers();
        }


        private void getSubjectsInquirers()
        {
            ShowBusyIndicator("در حال بارگذاری اطلاعات");
            unitInPeriodService.GetInquirySubjectWithInquirers((res, exp) => appController.BeginInvokeOnDispatcher(() =>
            {
                HideBusyIndicator();
                if (exp == null)
                {
                    employeeService.GetAllEmployees((employeeRes, employeeExp) => appController.BeginInvokeOnDispatcher(() =>
                    {

                        if (employeeExp == null)
                        {
                           // Employees = employeeRes;
                            InquirySubjectWithInquirers =
                                res.Select(i => new InquirySubjectInquirersUnitViewModel(unitInPeriodService, appController, employeeService)
                                {
                                    Period = Period,
                                    UnitInPeriodDto = UnitInPeriod,
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
            }), Period.Id, unitInPeriod.UnitId);
        }

       

        protected override void OnRequestClose()
        {
            base.OnRequestClose();
            appController.Close(this);
        }

        #endregion


    }

}
