using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.PeriodManagementApp;
using MITD.Presentation;
using System.Threading;
using System.Windows.Input;
using System.Collections;

namespace MITD.PMS.Presentation.Logic
{
    public class CalculationVM : PeriodMgtWorkSpaceViewModel
    {

        #region Fields

        private readonly IPMSController appController;
        private readonly ICalculationServiceWrapper calculationService;
        private readonly IPolicyServiceWrapper policyServiceWrapper;
        private readonly IEmployeeServiceWrapper employeeService;
        private readonly IJobPositionInPeriodServiceWrapper jobPositionService;
        private readonly IUnitInPeriodServiceWrapper unitService;

        #endregion

        #region Properties & Back Field

        private bool allResultSelected;
        public bool AllResultSelected
        {
            get { return allResultSelected; }
            set
            {
                this.SetField(p => p.AllResultSelected, ref allResultSelected, value);
                SelectionVisible = !AllResultSelected;
                (AddEmployeeCommand.Command as DelegateCommand).RaiseCanExecuteChanged();
                (RemoveEmployeeCommand.Command as DelegateCommand).RaiseCanExecuteChanged();
            }
        }

        private EmployeeCriteria employeeCriteria;
        public EmployeeCriteria EmployeeCriteria
        {
            get { return employeeCriteria; }
            set { this.SetField(p => p.EmployeeCriteria, ref employeeCriteria, value); }
        }

        private bool selectionVisible;
        public bool SelectionVisible
        {
            get { return selectionVisible; }
            set { this.SetField(p => p.SelectionVisible, ref selectionVisible, value); }
        }

        private CalculationDTO calculation;
        public CalculationDTO Calculation
        {
            get { return calculation; }
            set { this.SetField(p => p.Calculation, ref calculation, value); }
        }

        private PagedSortableCollectionView<EmployeeDTOWithActions> employees;
        public PagedSortableCollectionView<EmployeeDTOWithActions> Employees
        {
            get { return employees; }
            set { this.SetField(p => p.Employees, ref employees, value); }
        }

        private ObservableCollection<EmployeeDTOWithActions> calculationEmployees;
        public ObservableCollection<EmployeeDTOWithActions> CalculationEmployees
        {
            get { return calculationEmployees; }
            set { this.SetField(p => p.CalculationEmployees, ref calculationEmployees, value); }
        }


        private List<EmployeeDTOWithActions> selectedCalculationEmployees=new List<EmployeeDTOWithActions>();
        public List<EmployeeDTOWithActions> SelectedCalculationEmployees
        {
            get { return selectedCalculationEmployees; }
            set
            {
                this.SetField(p => p.SelectedCalculationEmployees, ref selectedCalculationEmployees, value);
                (RemoveEmployeeCommand.Command as DelegateCommand).RaiseCanExecuteChanged();
            }
        }

        private List<EmployeeDTOWithActions> selectedEmployees = new List<EmployeeDTOWithActions>();
        public List<EmployeeDTOWithActions> SelectedEmployees
        {
            get { return selectedEmployees; }
            set
            {
                this.SetField(p => p.SelectedEmployees, ref selectedEmployees, value);
                (addEmployeeCommand.Command as DelegateCommand).RaiseCanExecuteChanged();
            }
        }

        #region For Revise
        private OrganChartEmployeesCalculationDTO organChartEmployeesCalculation;
        public OrganChartEmployeesCalculationDTO OrganChartEmployeesCalculation
        {
            get { return organChartEmployeesCalculation; }
            set { this.SetField(p => p.OrganChartEmployeesCalculation, ref organChartEmployeesCalculation, value); }
        }

        private ObservableCollection<TreeCheckBoxViewModel<AbstractOrganChartDTOWithActions>> abstractOrganChartTree;
        public ObservableCollection<TreeCheckBoxViewModel<AbstractOrganChartDTOWithActions>> AbstractOrganChartTree
        {
            get { return abstractOrganChartTree; }
            set { this.SetField(p => p.AbstractOrganChartTree, ref abstractOrganChartTree, value); }
        }

        private TreeElementViewModel<AbstractOrganChartDTOWithActions> selectedAbstractOrganChart;
        public TreeElementViewModel<AbstractOrganChartDTOWithActions> SelectedAbstractOrganChart
        {
            get { return selectedAbstractOrganChart; }
            set { this.SetField(p => p.SelectedAbstractOrganChart, ref selectedAbstractOrganChart, value); }
        }
        #endregion

        private List<PolicyDescriptionDTO> policyList;
        public List<PolicyDescriptionDTO> PolicyList
        {
            get { return policyList; }
            set { this.SetField(p => p.PolicyList, ref policyList, value); }
        }


        private List<UnitInPeriodDTO> unitList;
        public List<UnitInPeriodDTO> UnitList
        {
            get { return unitList; }
            set { this.SetField(p => p.UnitList, ref unitList, value); }
        }

        private List<JobPositionInPeriodDTO> jobPositionList;
        public List<JobPositionInPeriodDTO> JobPositionList
        {
            get { return jobPositionList; }
            set { this.SetField(p => p.JobPositionList, ref jobPositionList, value); }
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

        private CommandViewModel addCalculationCommand;
        public CommandViewModel AddCalculationCommand
        {
            get
            {
                if (addCalculationCommand == null)
                {
                    addCalculationCommand = new CommandViewModel("ایجاد محاسبه", new DelegateCommand(addCalculation));
                }
                return addCalculationCommand;
            }
        }

        private CommandViewModel addEmployeeCommand;
        public CommandViewModel AddEmployeeCommand
        {
            get
            {
                if (addEmployeeCommand == null)
                {
                    addEmployeeCommand = new CommandViewModel("addEmployee", new DelegateCommand(addEmployee,
                        () => !AllResultSelected));
                }
                return addEmployeeCommand;
            }
        }

        private CommandViewModel removeEmployeeCommand;
        public CommandViewModel RemoveEmployeeCommand
        {
            get
            {
                if (removeEmployeeCommand == null)
                {
                    removeEmployeeCommand = new CommandViewModel("removeEmployee", new DelegateCommand(removeEmployee,
                       () => SelectedCalculationEmployees != null &&
                           !AllResultSelected));
                }
                return removeEmployeeCommand;
            }
        }

        private ICommand selectionChangedCommand;
        public ICommand SelectionChangedCommand
        {
            get
            {
                if (selectionChangedCommand == null)
                {
                    selectionChangedCommand = new DelegateCommand<IList>(
                        list=>
                        {
                            selectedEmployees.Clear();
                            foreach (var item in list)
                            {
                                selectedEmployees.Add(item as EmployeeDTOWithActions);
                            }
                            OnPropertyChanged("SelectedEmployees");
                        });
                }
                return selectionChangedCommand;
            }
        }
        
        private ICommand calculationSelectionChangedCommand;
        public ICommand CalculationSelectionChangedCommand
        {
            get
            {
                if (calculationSelectionChangedCommand == null)
                {
                    calculationSelectionChangedCommand = new DelegateCommand<IList>(
                        list =>
                        {
                            selectedCalculationEmployees.Clear();
                            foreach (var item in list)
                            {
                                selectedCalculationEmployees.Add(item as EmployeeDTOWithActions);
                            }
                            OnPropertyChanged("SelectedCalculationEmployees");
                        });
                }
                return calculationSelectionChangedCommand;
            }
        }
        #endregion

        #region Constructors

        public CalculationVM()
        {
            PeriodMgtAppLocalizedResources = new PeriodMgtAppLocalizedResources();
            init();
        }

        public CalculationVM(IPMSController appController,
                               IPeriodMgtAppLocalizedResources periodMgtAppLocalizedResources,
                               ICalculationServiceWrapper calculationService,
                               IPolicyServiceWrapper policyServiceWrapper, 
                               IEmployeeServiceWrapper employeeService,
                               IJobPositionInPeriodServiceWrapper jobPositionService,
                               IUnitInPeriodServiceWrapper unitService)
        {

            this.appController = appController;
            this.calculationService = calculationService;
            this.policyServiceWrapper = policyServiceWrapper;
            this.employeeService = employeeService;
            this.jobPositionService = jobPositionService;
            this.unitService = unitService;
            PeriodMgtAppLocalizedResources = periodMgtAppLocalizedResources;
            init();
        }

        #endregion

        #region Methods

        void init()
        {
            DisplayName = "محاسبه عملکرد کارکنان در دوره";
            Employees = new PagedSortableCollectionView<EmployeeDTOWithActions>();
            CalculationEmployees = new ObservableCollection<EmployeeDTOWithActions>();
            policyList = new List<PolicyDescriptionDTO>();
            EmployeeCriteria = new EmployeeCriteria();
            Employees.OnRefresh += (s, args) => getEmployees(null);
            AllResultSelected = false;
        }

        public void Load(CalculationDTO caclculationParam, ActionType action)
        {
            preLoad();
            Calculation = caclculationParam;
        }

        private void searchEmployee()
        {
            ShowBusyIndicator("در حال دریافت اطلاعات...");
            employeeService.GetAllEmployees(
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
                }), Calculation.PeriodId,EmployeeCriteria, employees.PageSize, employees.PageIndex + 1);
        }

        private void preLoad()
        {
            var trigger1 = new AutoResetEvent(false);
            var trigger2 = new AutoResetEvent(false);
            var trigger3 = new AutoResetEvent(false);
            var trigger4 = new AutoResetEvent(false);
            ThreadPool.QueueUserWorkItem(s =>
            {
                appController.BeginInvokeOnDispatcher(() => ShowBusyIndicator("در حال دریافت اطلاعات..."));
                getPolicies(trigger1);
                getUnits(trigger3);
                getJobPositions(trigger4);
                getEmployees(trigger2);
                trigger1.WaitOne();
                trigger3.WaitOne();
                trigger4.WaitOne();
                trigger2.WaitOne();
                appController.BeginInvokeOnDispatcher(HideBusyIndicator);
            });
        }

        private void getEmployees(AutoResetEvent trigger)
        {
            if (trigger == null)
                ShowBusyIndicator("در حال دریافت اطلاعات...");
            employeeService.GetAllEmployees(
               (res, exp) => appController.BeginInvokeOnDispatcher(() =>
                {
                    if (trigger != null)
                        trigger.Set();
                    else
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
                }), Calculation.PeriodId, employees.PageSize, employees.PageIndex + 1);
        }

        private void getPolicies(AutoResetEvent trigger)
        {
            policyServiceWrapper.GetAllPolicys( (res, exp) => appController.BeginInvokeOnDispatcher(() =>
                {
                    trigger.Set();
                    if (exp == null)
                        appController.BeginInvokeOnDispatcher(() =>
                            {
                                PolicyList = res;
                            });
                    else
                        appController.HandleException(exp);
                }));
        }

        private void getUnits(AutoResetEvent trigger)
        {
            unitService.GetAllUnits( (res, exp) => appController.BeginInvokeOnDispatcher(() =>
            {
                trigger.Set();
                if (exp == null)
                    appController.BeginInvokeOnDispatcher(() =>
                    {
                        UnitList = res;
                    });
                else
                    appController.HandleException(exp);
            }),Calculation.PeriodId);
        }

        private void getJobPositions(AutoResetEvent trigger)
        {
            jobPositionService.GetAllJobPositions( (res, exp) => appController.BeginInvokeOnDispatcher(() =>
            {
                trigger.Set();
                if (exp == null)
                    appController.BeginInvokeOnDispatcher(() =>
                    {
                        JobPositionList = res;
                    });
                else
                    appController.HandleException(exp);
            }), Calculation.PeriodId);
        }

        private void addEmployee()
        {
            var message = string.Empty;
            foreach (var employee in selectedEmployees)
            {
                if (!calculationEmployees.Contains(employee))
                    CalculationEmployees.Add(employee);
                else
                {
                    message += "کارمند" + employee.LastName + "  قبلا انتخاب شده است" + "//";

                }
            }
            if (String.IsNullOrEmpty(message))
                return;
            appController.ShowMessage(message);
        }

        private void removeEmployee()
        {
            if (selectedCalculationEmployees.Count > 0)
            {
                var lst = new List<EmployeeDTOWithActions>();
                lst.AddRange(selectedCalculationEmployees);
                lst.ForEach(e => calculationEmployees.Remove(e));
                OnPropertyChanged("CalculationEmployees");
            }
            else
            {
                appController.ShowMessage("کارمندی انتخاب نشده است");
            }
        }

        private void addCalculation()
        {
            if (CalculationEmployees.Count == 0 && !AllResultSelected)
            {
                appController.ShowMessage("حداقل یک کارمند انتخاب کنید");
                return;
            }
            ShowBusyIndicator("در حال دریافت اطلاعات...");
            var stringbuilder = new StringBuilder();
            if (AllResultSelected)
            {
                employeeService.GetAllEmployeeNo(
                    (res, exp) => appController.BeginInvokeOnDispatcher(() =>
                    {
                        if (exp == null)
                        {
                            appController.BeginInvokeOnDispatcher(() =>
                            {
                                res.ForEach(p => stringbuilder.Append(p + ";"));
                                addCalculation(stringbuilder);
                            });
                        }
                        else appController.HandleException(exp);
                    }), Calculation.PeriodId, EmployeeCriteria);
            }
            else
            {
                CalculationEmployees.Select(e => e.PersonnelNo).ToList().ForEach(p => stringbuilder.Append(p + ";"));
                addCalculation(stringbuilder);
            }
               
            
        }

        private void addCalculation(StringBuilder stringbuilder)
        {
            if (stringbuilder.Length > 0)
                stringbuilder.Remove(stringbuilder.Length - 1, 1);
            Calculation.EmployeeIdList = stringbuilder.ToString();
            ShowBusyIndicator("در حال دریافت اطلاعات...");
            calculationService.AddCalculation((res, exp) => appController.BeginInvokeOnDispatcher(() =>
            {
                HideBusyIndicator();
                if (exp == null)
                {
                    Calculation = res;
                    appController.ShowMessage("محاسبه با موفقیت اضافه شد.");
                    appController.Publish(new UpdateCalculationListArgs());
                    appController.Close(this);
                }
                else
                    appController.HandleException(exp);
            }), Calculation);
        }

        protected override void OnRequestClose()
        {
            base.OnRequestClose();
            appController.Close(this);
        }



        #endregion


    }

}
