using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MITD.Core;
using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.EmployeeManagement;
using MITD.PMS.Presentation.EmployeeManagement.Views;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public class EmployeeJobPositionsVM : EmployeeMgtWorkSpaceViewModel, IEventHandler<UpdateEmployeeJobPositionAssignment>
    {
        #region Fields

        private readonly IEmployeeController employeeController;
        private readonly IPMSController appController;
        private readonly IEmployeeServiceWrapper employeeService;
        private readonly IJobPositionInPeriodServiceWrapper jobPositionInPeriodService;

        #endregion

        #region Properties
        private PeriodDTO period;
        public PeriodDTO Period
        {
            get { return period; }
            set { this.SetField(p => p.Period, ref period, value); }
        }

        private EmployeeDTO employee;
        public EmployeeDTO Employee
        {
            get { return employee; }
            set { this.SetField(p => p.Employee, ref employee, value); }
        }


        private EmployeeJobPositionsDTO employeeJobPositions;
        public EmployeeJobPositionsDTO EmployeeJobPositions
        {
            get { return employeeJobPositions; }
            set { this.SetField(p => p.EmployeeJobPositions, ref employeeJobPositions, value); }
        }

        private ObservableCollection<TreeElementViewModel<JobPositionInPeriodDTO>> jobPositionTree;
        public ObservableCollection<TreeElementViewModel<JobPositionInPeriodDTO>> JobPositionTree
        {
            get { return jobPositionTree; }
            set { this.SetField(p => p.JobPositionTree, ref jobPositionTree, value); }
        }

        private TreeElementViewModel<JobPositionInPeriodDTO> selectedJobPosition;
        public TreeElementViewModel<JobPositionInPeriodDTO> SelectedJobPosition
        {
            get { return selectedJobPosition; }
            set { this.SetField(p => p.SelectedJobPosition, ref selectedJobPosition, value); }
        }

        private EmployeeJobPositionAssignmentDTO selectedJobPositionDuration;
        public EmployeeJobPositionAssignmentDTO SelectedJobPositionDuration
        {
            get { return selectedJobPositionDuration; }
            set
            {
                this.SetField(p => p.SelectedJobPositionDuration, ref selectedJobPositionDuration, value);
                //EmployeeCommands = createCommands();
                if (View != null && value != null)
                    ((IEmployeeJobPositionsView)View).CreateContextMenu(new ReadOnlyCollection<DataGridCommandViewModel>(createCommands()));
            }
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


        private CommandViewModel addCommand;
        public CommandViewModel AddCommand
        {
            get
            {
                if (addCommand == null)
                {
                    addCommand = new CommandViewModel("اضافه", new DelegateCommand(add));
                }
                return addCommand;
            }
        }



        private CommandViewModel removeCommand;
        public CommandViewModel RemoveCommand
        {
            get
            {
                if (removeCommand == null)
                {
                    removeCommand = new CommandViewModel("حذف", new DelegateCommand(remove));
                }
                return removeCommand;
            }
        }



        #endregion

        #region Constructors

        public EmployeeJobPositionsVM()
        {
            EmployeeMgtAppLocalizedResources = new EmployeeAppLocalizedResources();
            init();
            //Employees.Add(new EmployeeDTODescriptionWithActions { Id = 4, FirstName = "کارمند 1", LastName = "کارمند" });
        }

        public EmployeeJobPositionsVM(IEmployeeController employeeController, IPMSController appController,
            IEmployeeServiceWrapper employeeService, IJobPositionInPeriodServiceWrapper jobPositionInPeriodService, IEmployeeAppLocalizedResources localizedResources)
        {

            this.appController = appController;
            this.employeeService = employeeService;
            this.jobPositionInPeriodService = jobPositionInPeriodService;
            this.employeeController = employeeController;
            EmployeeMgtAppLocalizedResources = localizedResources;
            init();

        }

        #endregion

        #region Methods

        void init()
        {

            EmployeeJobPositions = new EmployeeJobPositionsDTO();
            Employee = new EmployeeDTO();
            Period = new PeriodDTO();

        }

        public void Load(EmployeeDTO employeeParam, PeriodDTO periodParam)
        {

            Period = periodParam;
            Employee = employeeParam;
            DisplayName = "تخصیص پست های سازمانی کارمند:" + " " + Employee.FirstName + " " + Employee.LastName;
            preLoad();
            getEmployeeJobPositions();
        }

        private void preLoad()
        {
            ShowBusyIndicator("در حال دریافت اطلاعات...");
            jobPositionInPeriodService.GetAllJobPositions(
                (res, exp) => appController.BeginInvokeOnDispatcher(() =>
               {

                   if (exp == null)
                   {
                       JobPositionTree = SilverLightTreeViewHelper<JobPositionInPeriodDTO>.prepareListForTreeView(res);
                       HideBusyIndicator();
                   }
                   else
                   {
                       HideBusyIndicator();
                       appController.HandleException(exp);
                   }
               }), Period.Id);
        }

        private List<DataGridCommandViewModel> createCommands()
        {
            return CommandHelper.GetControlCommands(this, appController, SelectedJobPositionDuration.ActionCodes);
        }

        private void getEmployeeJobPositions()
        {
            employeeService.GetEmployeeJobPositionsInPeriod((res, exp) => appController.BeginInvokeOnDispatcher(() =>
            {
                if (exp == null)
                {
                    EmployeeJobPositions = res;
                    if (EmployeeJobPositions.EmployeeJobPositionAssignmentList == null)
                        EmployeeJobPositions.EmployeeJobPositionAssignmentList = new ObservableCollection<EmployeeJobPositionAssignmentDTO>();
                }
                else
                {
                    appController.HandleException(exp);
                }
            }),
                employee.PersonnelNo, period.Id);
        }



        private void save()
        {
            employeeService.AssignJobPositionsToEmployee((res, exp) => appController.BeginInvokeOnDispatcher(() =>
            {
                if (exp == null)
                {
                    EmployeeJobPositions = EmployeeJobPositions;
                    appController.ShowMessage("عملیات با موفقیت انجام شد");
                    OnRequestClose();
                }
                else
                {
                    appController.HandleException(exp);
                }
            }), Period.Id, Employee.PersonnelNo, EmployeeJobPositions);
        }

        private void add()
        {
            if (SelectedJobPosition == null ||
                EmployeeJobPositions.EmployeeJobPositionAssignmentList.Select(c => c.JobPositionId)
                    .Contains(SelectedJobPosition.Data.JobPositionId))
                return;
            var jobPositionAssignment = new EmployeeJobPositionAssignmentDTO()
            {
                JobPositionName = SelectedJobPosition.Data.Name,
                JobPositionId = SelectedJobPosition.Data.JobPositionId,
                FromDate = Period.StartDate,
                ToDate = Period.EndDate
            };

            employeeController.ShowEmployeeJobCustomFieldsView(employee, period, jobPositionAssignment,
                                                               ActionType.AddEmployeeJobCustomFields);
        }

        private void remove()
        {
            if (SelectedJobPositionDuration == null)
            {
                appController.ShowMessage("هیچ موقعیت شغلی برای حذف انتخاب نشده است");
                return;
            }
            EmployeeJobPositions.EmployeeJobPositionAssignmentList.Remove(SelectedJobPositionDuration);
        }

        protected override void OnRequestClose()
        {
            base.OnRequestClose();
            appController.Close(this);
        }

        #endregion

        public void Handle(UpdateEmployeeJobPositionAssignment eventData)
        {
            if (!employeeJobPositions.PeriodId.Equals(eventData.Period.Id) || Employee.PersonnelNo != eventData.Employee.PersonnelNo)
                return;
            if (eventData.Action == ActionType.AddEmployeeJobCustomFields)
            {
                if (EmployeeJobPositions.EmployeeJobPositionAssignmentList.Count > 0)
                    appController.ShowMessage("هر کارمند می تواند تنها یک پست سازمانی داشته باشد ");
                else
                    EmployeeJobPositions.EmployeeJobPositionAssignmentList.Add(eventData.EmployeeJobPositionAssignment);
            }
            else
            {
                var selectedJobPos = employeeJobPositions.EmployeeJobPositionAssignmentList.Single(
                    c => c.Id == eventData.EmployeeJobPositionAssignment.Id);
                selectedJobPos = eventData.EmployeeJobPositionAssignment;
            }

        }
    }

}
