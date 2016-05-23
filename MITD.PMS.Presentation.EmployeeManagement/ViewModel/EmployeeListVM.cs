using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MITD.Core;
using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.EmployeeManagement;
using MITD.PMS.Presentation.EmployeeManagement.Views;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public class EmployeeListVM : EmployeeMgtWorkSpaceViewModel, IEventHandler<UpdateEmployeeListArgs>
    {
        #region Fields

        private readonly IEmployeeController employeeController;
        private readonly IPMSController appController;
        private readonly IEmployeeServiceWrapper employeeService;
        private PeriodDTO period;

        #endregion

        #region Properties

        public PeriodDTO Period
        {
            get { return period; }
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
                EmployeeCommands = createCommands();
                if (View != null)
                    ((IEmployeeListView)View).CreateContextMenu(new ReadOnlyCollection<DataGridCommandViewModel>(EmployeeCommands));
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
        

        #endregion

        #region Constructors

        public EmployeeListVM()
        {
            EmployeeMgtAppLocalizedResources = new EmployeeAppLocalizedResources();
            init();
        }

        public EmployeeListVM(IEmployeeController employeeController, IPMSController appController,
            IEmployeeServiceWrapper employeeService, IEmployeeAppLocalizedResources localizedResources)
        {

            this.appController = appController;
            this.employeeService = employeeService;
            this.employeeController = employeeController;
            EmployeeMgtAppLocalizedResources = localizedResources;
            init();

        }

        #endregion

        #region Methods

        void init()
        {
            period=new PeriodDTO();
            Employees = new PagedSortableCollectionView<EmployeeDTOWithActions>(){PageSize = 20};
            Employees.OnRefresh += (s, args) => Load(period);
            DisplayName = EmployeeMgtAppLocalizedResources.EmployeeListPageTitle+" "+"دوره";
            EmployeeCommands = new List<DataGridCommandViewModel>
            {
                   CommandHelper.GetControlCommands(this, appController, new List<int>{ (int) ActionType.AddEmployee}).FirstOrDefault()
            };
        }

        private List<DataGridCommandViewModel> createCommands()
        {
            return CommandHelper.GetControlCommands(this, appController, selectedEmployee.ActionCodes);
        }

        public void Load(PeriodDTO periodParam)
        {
            period = periodParam;
            DisplayName = EmployeeMgtAppLocalizedResources.EmployeeListPageTitle + " " + "دوره" +" "+ period.Name;
            ShowBusyIndicator("در حال دریافت اطلاعات...");
            employeeService.GetAllEmployees(
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
                }),period.Id, employees.PageSize, employees.PageIndex + 1);
        }

        protected override void OnRequestClose()
        {
            base.OnRequestClose();
            appController.Close(this);
        }

        public void Handle(UpdateEmployeeListArgs eventData)
        {
            Load(period);
        }

        #endregion
    }

}
