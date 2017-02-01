using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MITD.Core;
using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.PeriodManagementApp;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public class SubordinatesConfirmationVM : PeriodMgtWorkSpaceViewModel//, IEventHandler<UpdateEmployeeListArgs>
    {
        #region Fields

        private readonly IPMSController appController;
        private readonly IEmployeeServiceWrapper employeeService;
        private PeriodDTO period;
        private string managerEmployeeNo;

        #endregion

        #region Properties

        public PeriodDTO Period
        {
            get { return period; }
        }

        private EmployeeCriteria employeeCriteria;
        public EmployeeCriteria EmployeeCriteria
        {
            get { return employeeCriteria; }
            set { this.SetField(p => p.EmployeeCriteria, ref employeeCriteria, value); }
        }


        private PagedSortableCollectionView<EmployeeDTOWithActions> employees;
        public PagedSortableCollectionView<EmployeeDTOWithActions> Employees
        {
            get { return employees; }
            set { this.SetField(p => p.Employees, ref employees, value); }
        }

        private EmployeeDTOWithActions selectedEmployee;
        public EmployeeDTOWithActions SelectedEmployee
        {
            get { return selectedEmployee; }
            set
            {
                this.SetField(p => p.SelectedEmployee, ref selectedEmployee, value);
                if(value==null)
                    return;
                EmployeeCommands = createCommands();
                if (View != null)
                    ((ISubordinatesConfirmationView)View).CreateContextMenu(new ReadOnlyCollection<DataGridCommandViewModel>(EmployeeCommands));
            }
        }

        private List<DataGridCommandViewModel> employeeCommands;
        public List<DataGridCommandViewModel> EmployeeCommands
        {
            get { return employeeCommands; }
            private set
            {
                this.SetField(p => p.EmployeeCommands, ref employeeCommands, value);
                if (EmployeeCommands.Count > 0) SelectedCommand = EmployeeCommands[0];
            }

        }

        private DataGridCommandViewModel selectedCommand;
        public DataGridCommandViewModel SelectedCommand
        {
            get { return selectedCommand; }
            set { this.SetField(p => p.SelectedCommand, ref selectedCommand, value); }
        }

        private CommandViewModel searchCommand;
        public CommandViewModel SearchCommand
        {
            get
            {
                if (searchCommand == null)
                {
                    searchCommand = new CommandViewModel("ایجاد محاسبه", new DelegateCommand(searchEmployee));
                }
                return searchCommand;
            }
        }

        

        #endregion

        #region Constructors

        public SubordinatesConfirmationVM()
        {
            PeriodMgtAppLocalizedResources = new PeriodMgtAppLocalizedResources();
            init();
        }

        public SubordinatesConfirmationVM( IPMSController appController,
            IEmployeeServiceWrapper employeeService, IPeriodMgtAppLocalizedResources localizedResources)
        {

            this.appController = appController;
            this.employeeService = employeeService;
            PeriodMgtAppLocalizedResources = localizedResources;
            init();

        }

        #endregion

        #region Methods

        void init()
        {
            period=new PeriodDTO();
            managerEmployeeNo=String.Empty;
            Employees = new PagedSortableCollectionView<EmployeeDTOWithActions>(){PageSize = 20};
            Employees.OnRefresh += (s, args) => Load(managerEmployeeNo,period);
             DisplayName = "ليست همكاران زير مجموعه";
            EmployeeCriteria = new EmployeeCriteria();
            EmployeeCommands = new List<DataGridCommandViewModel>
            {
                   CommandHelper.GetControlCommands(this, appController, new List<int>{ (int) ActionType.AddEmployee}).FirstOrDefault()
            };
        }

        private List<DataGridCommandViewModel> createCommands()
        {
            return CommandHelper.GetControlCommands(this, appController, selectedEmployee.ActionCodes);
        }

        public void Load(string employeeNo, PeriodDTO periodParam)
        {
            period = periodParam;
            managerEmployeeNo = employeeNo;
            ShowBusyIndicator("در حال دریافت اطلاعات...");
            employeeService.GetSubordinatesEmployees(
                 (res, exp) => appController.BeginInvokeOnDispatcher(() =>
                {
                    HideBusyIndicator();
                    if (exp == null)
                    {
                        employees.SourceCollection = res.Result;
                        employees.TotalItemCount = res.TotalCount;
                        employees.PageIndex = Math.Max(0, res.CurrentPage - 1);
                    }
                    else appController.HandleException(exp);
                }), managerEmployeeNo, period.Id, EmployeeCriteria, employees.PageSize, employees.PageIndex + 1);
        }

        private void searchEmployee()
        {
            ShowBusyIndicator("در حال دریافت اطلاعات...");
            employeeService.GetSubordinatesEmployees(
                (res, exp) => appController.BeginInvokeOnDispatcher(() =>
                {
                    HideBusyIndicator();
                    if (exp == null)
                    {
                        appController.BeginInvokeOnDispatcher(() =>
                        {
                            Employees.TotalItemCount = res.TotalCount;
                            Employees.PageIndex = Math.Max(0, res.CurrentPage - 1);

                            Employees.SourceCollection = res.Result.ToList();
                        });
                    }
                    else appController.HandleException(exp);
                }),managerEmployeeNo, Period.Id, EmployeeCriteria, employees.PageSize, employees.PageIndex + 1);
        }

        protected override void OnRequestClose()
        {
            base.OnRequestClose();
            appController.Close(this);
        }

        //public void Handle(UpdateEmployeeListArgs eventData)
        //{
        //    Load(period);
        //}

        #endregion
    }

}
