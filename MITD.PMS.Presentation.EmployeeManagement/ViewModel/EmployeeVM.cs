using System;
using System.Collections.ObjectModel;
using System.Linq;
using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.EmployeeManagement;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public sealed class EmployeeVM : EmployeeMgtWorkSpaceViewModel
    {
        #region Fields

        private readonly IPMSController appController;
        private readonly IEmployeeServiceWrapper employeeService;
        private readonly ICustomFieldServiceWrapper customFieldService;
        private ActionType actionType;

        #endregion

        #region Properties

        private EmployeeDTO employee;
        public EmployeeDTO Employee
        {
            get { return employee; }
            set { this.SetField(vm => vm.Employee, ref employee, value); }
        }

        private bool isModifying;
        public bool IsModifying
        {
            get { return isModifying; }
            set { this.SetField(vm => vm.IsModifying, ref isModifying, value); }
        }

        private ObservableCollection<CheckBoxListViewModel<CustomFieldValueDTO>> customFieldValueList;
        public ObservableCollection<CheckBoxListViewModel<CustomFieldValueDTO>> CustomFieldValueList
        {
            get { return customFieldValueList; }
            set { this.SetField(vm => vm.CustomFieldValueList, ref customFieldValueList, value); }
        }

        private CommandViewModel saveCommand;
        public CommandViewModel SaveCommand
        {
            get
            {
                if (saveCommand == null)
                {
                    saveCommand = new CommandViewModel(EmployeeMgtAppLocalizedResources.SaveTitle, new DelegateCommand(save));
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
                    cancelCommand = new CommandViewModel(EmployeeMgtAppLocalizedResources.CancelTitle, new DelegateCommand(OnRequestClose));
                }
                return cancelCommand;
            }
        }

        #endregion

        #region Constructors

        public EmployeeVM()
        {
            EmployeeMgtAppLocalizedResources = new EmployeeAppLocalizedResources();
            init();
            //Employee = new Employee { Name = "دوره اول", StartDate = DateTime.Now, EndDate = DateTime.Now };
        }
        public EmployeeVM(IPMSController appController,
                          IEmployeeServiceWrapper employeeService,
                          IEmployeeAppLocalizedResources localizedResources,
                          ICustomFieldServiceWrapper customFieldService)
        {
            this.appController = appController;
            this.employeeService = employeeService;
            this.customFieldService = customFieldService;
            EmployeeMgtAppLocalizedResources = localizedResources;
            init();

        }

        #endregion

        #region Methods

        private void init()
        {
            Employee = new EmployeeDTO();
            DisplayName = EmployeeMgtAppLocalizedResources.EmployeeViewTitle;
            IsModifying = false;
        }

        public void Load(EmployeeDTO employeeParam, ActionType actionTypeParam)
        {
            actionType = actionTypeParam;
            Employee = employeeParam;
            if (actionType.Equals(ActionType.ModifyEmployee))
                IsModifying = true;
            getEmployeeCustomFields();


        }

        private void getEmployeeCustomFields()
        {
            customFieldService.GetAllCustomFields((res, exp) => appController.BeginInvokeOnDispatcher(() =>
            {
                if (exp == null)
                {
                    CustomFieldValueList =
                        CheckBoxListViewHelper<CustomFieldValueDTO>.PrepareListForCheckBoxListView(
                            res.Select(r => new CustomFieldValueDTO { Id = r.Id, IsReadOnly = r.IsReadOnly, Name = r.Name }).ToList());
                    if (actionType != ActionType.ModifyEmployee || Employee.CustomFields == null) return;
                    foreach (var itm in CustomFieldValueList)
                    {
                        if (Employee.CustomFields.Select(c => c.Id).Contains(itm.Data.Id))
                        {
                            itm.IsChecked = true;
                            itm.Data.Value = Employee.CustomFields.Single(e => e.Id == itm.Data.Id).Value;
                        }
                        else
                            itm.IsChecked = false;
                    }
                }
                else
                {
                    appController.HandleException(exp);
                }
            }), "Employee");
        }


        private void save()
        {
            if (!employee.Validate()) return;

            ShowBusyIndicator();
            employee.CustomFields =
                customFieldValueList.Where(c => c.IsChecked).Select(c => c.Data as CustomFieldValueDTO).ToList();
            if (actionType == ActionType.AddEmployee)
            {
                employeeService.AddEmployee((res, exp) => appController.BeginInvokeOnDispatcher(() =>
                    {
                        HideBusyIndicator();
                        if (exp != null)
                            appController.HandleException(exp);
                        else
                            FinalizeAction();
                    }), employee);
            }
            else if (actionType == ActionType.ModifyEmployee)
            {
                employeeService.UpdateEmployee((res, exp) => appController.BeginInvokeOnDispatcher(() =>
                    {
                        HideBusyIndicator();
                        if (exp != null)
                            appController.HandleException(exp);
                        else
                            FinalizeAction();
                    }), employee);
            }
        }

        private void FinalizeAction()
        {
            appController.Publish(new UpdateEmployeeListArgs());
            OnRequestClose();
        }


        protected override void OnRequestClose()
        {
            base.OnRequestClose();
            appController.Close(this);
        }

        #endregion
    }

}
