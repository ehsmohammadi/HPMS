using System.Collections.Generic;
using System.Linq;
using MITD.PMS.Presentation.Contracts;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public class InquirySubjectInquirersUnitViewModel : ViewModelBase
    {
        private readonly IUnitInPeriodServiceWrapper _unitInPeriodServiceWrapper;
        private readonly IEmployeeServiceWrapper employeeService;
        private readonly IPMSController appController;

        #region Properties & Back Fields

        private PeriodDTO period;
        public PeriodDTO Period
        {
            get { return period; }
            set { this.SetField(s => s.Period, ref period, value); }
        }

        private UnitInPeriodDTO _unitInPeriodDto;

        public UnitInPeriodDTO UnitInPeriodDto
        {
            get { return _unitInPeriodDto; }
            set
            {
                this.SetField(s => s.UnitInPeriodDto, ref _unitInPeriodDto, value);

            }
        }

        private InquirySubjectWithInquirersDTO selectedInquirySubjectWithInquirers;
        public InquirySubjectWithInquirersDTO SelectedInquirySubjectWithInquirers
        {
            get { return selectedInquirySubjectWithInquirers; }
            set
            {
                this.SetField(vm => vm.SelectedInquirySubjectWithInquirers, ref selectedInquirySubjectWithInquirers, value);
                EmployeeName = selectedInquirySubjectWithInquirers.EmployeeName;
            }
        }


        private InquirerDTO selectedCustomInquirer;
        public InquirerDTO SelectedCustomInquirer
        {
            get { return selectedCustomInquirer; }
            set { this.SetField(vm => vm.SelectedCustomInquirer, ref selectedCustomInquirer, value); }
        }


        private EmployeeDTO selectedEmployee;
        public EmployeeDTO SelectedEmployee
        {
            get { return selectedEmployee; }
            set { this.SetField(vm => vm.SelectedEmployee, ref selectedEmployee, value); }
        }

        private List<EmployeeDTO> employees;
        public List<EmployeeDTO> Employees
        {
            get { return employees; }
            set { this.SetField(vm => vm.Employees, ref employees, value); }
        }

        private string employeeName;
        public string EmployeeName
        {
            get { return employeeName; }
            set { this.SetField(p => p.EmployeeName, ref employeeName, value); }
        }



        private CommandViewModel addInquirerCommand;
        public CommandViewModel AddInquirerCommand
        {
            get
            {
                if (addInquirerCommand == null)
                {
                    addInquirerCommand = new CommandViewModel("افزودن نظر دهنده دلخواه", new DelegateCommand(addInquirer));
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
                    removeInquirerCommand = new CommandViewModel("حذف نظر دهنده دلخواه", new DelegateCommand(removeInquirer));
                }
                return removeInquirerCommand;
            }
        }

        private CommandViewModel saveCommand;
        public CommandViewModel SaveCommand
        {
            get
            {
                if (saveCommand == null)
                {
                    saveCommand = new CommandViewModel("تایید", new DelegateCommand(save));
                }
                return saveCommand;
            }
        }

        #endregion

        public InquirySubjectInquirersUnitViewModel(IUnitInPeriodServiceWrapper unitInPeriodServiceWrapper, IPMSController appController
            ,IEmployeeServiceWrapper employeeService)
        {
            this._unitInPeriodServiceWrapper = unitInPeriodServiceWrapper;
            this.appController = appController;
            this.employeeService = employeeService;
        }

        #region Methods

        private void addInquirer()
        {
            if (SelectedEmployee == null || SelectedInquirySubjectWithInquirers.CustomInquirers.Select(c => c.EmployeeNo).Contains(SelectedEmployee.PersonnelNo))
                return;
            employeeService.GetEmployeeUnitsInPeriod((res, exp) => appController.BeginInvokeOnDispatcher(() =>
            {
                if (exp == null)
                {
                    //foreach (var jobPosition in res.EmployeeJobPositionAssignmentList)
                    //{
                    //    var customInquirer = new InquirerDTO()
                    //        {
                    //            EmployeeNo = SelectedEmployee.PersonnelNo,
                    //            FullName = SelectedEmployee.FirstName + " " + SelectedEmployee.LastName,
                    //            EmployeeJobPositionId = jobPosition.JobPositionId,
                    //            EmployeeJobPositionName = jobPosition.JobPositionName
                    //        };

                    //    if (!SelectedInquirySubjectWithInquirers.Inquirers.Any(r => r.EmployeeNo == customInquirer.EmployeeNo && r.EmployeeJobPositionId == customInquirer.EmployeeJobPositionId)
                    //         && !SelectedInquirySubjectWithInquirers.CustomInquirers.Any(r => r.EmployeeNo == customInquirer.EmployeeNo && r.EmployeeJobPositionId == customInquirer.EmployeeJobPositionId))
                    //        SelectedInquirySubjectWithInquirers.CustomInquirers.Add(customInquirer);
                    //}
                }
                else
                    appController.HandleException(exp);
            }), SelectedEmployee.PersonnelNo, period.Id);

            
        }

        private void removeInquirer()
        {
            if (SelectedEmployee == null)
            {
                return;
            }
            SelectedInquirySubjectWithInquirers.CustomInquirers.Remove(SelectedCustomInquirer);
        }
        private void save()
        {
            _unitInPeriodServiceWrapper.UpdateInquirySubjectInquirers((res, exp) => appController.BeginInvokeOnDispatcher(() =>
            {
                if (exp == null)
                {
                    SelectedInquirySubjectWithInquirers = res;
                    appController.ShowMessage("نظر دهنده ها برای واحد مورد نظر بروز رسانی شد");
                }
                else
                    appController.HandleException(exp);

            }), Period.Id, UnitInPeriodDto.UnitId, SelectedInquirySubjectWithInquirers);

        }


        #endregion


    }

}
