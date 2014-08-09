using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.EmployeeManagement;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public sealed class EmployeeJobCustomFieldsVM : EmployeeMgtWorkSpaceViewModel
    {
        #region Fields

        private readonly IPMSController appController;
        private readonly IEmployeeServiceWrapper employeeService;
        private readonly ICustomFieldServiceWrapper customFieldService;
        private readonly IJobPositionInPeriodServiceWrapper jobPositionInPeriodService;
        private readonly IJobServiceWrapper jobService;
        private ActionType actionType;

        #endregion

        #region Properties

        private EmployeeDTO employee;
        public EmployeeDTO Employee
        {
            get { return employee; }
            set { this.SetField(vm => vm.Employee, ref employee, value); }
        }

        private PeriodDTO period;
        public PeriodDTO Period
        {
            get { return period; }
            set { this.SetField(vm => vm.Period, ref period, value); }
        }

        private EmployeeJobPositionAssignmentDTO jobPositionAssignment;
        public EmployeeJobPositionAssignmentDTO JobPositionAssignment
        {
            get { return jobPositionAssignment; }
            set { this.SetField(vm => vm.JobPositionAssignment, ref jobPositionAssignment, value); }
        }


        private List<CustomFieldValueDTO> customFieldValueList;
        public List<CustomFieldValueDTO> CustomFieldValueList
        {
            get { return customFieldValueList; }
            set { this.SetField(p => p.CustomFieldValueList, ref customFieldValueList, value); }
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

        public EmployeeJobCustomFieldsVM()
        {
            EmployeeMgtAppLocalizedResources = new EmployeeAppLocalizedResources();
            init();
            //Employee = new Employee { Name = "دوره اول", StartDate = DateTime.Now, EndDate = DateTime.Now };
        }
        public EmployeeJobCustomFieldsVM(IPMSController appController,
                          IEmployeeServiceWrapper employeeService,
                          IEmployeeAppLocalizedResources localizedResources,
                          ICustomFieldServiceWrapper customFieldService,
                          IJobPositionInPeriodServiceWrapper jobPositionInPeriodService,
                          IJobServiceWrapper jobService
            )
        {
            this.appController = appController;
            this.employeeService = employeeService;
            this.customFieldService = customFieldService;
            this.jobPositionInPeriodService = jobPositionInPeriodService;
            this.jobService = jobService;
            EmployeeMgtAppLocalizedResources = localizedResources;
            init();

        }

        #endregion

        #region Methods

        private void init()
        {
            Employee = new EmployeeDTO();
            Period = new PeriodDTO();
            JobPositionAssignment = new EmployeeJobPositionAssignmentDTO();
            DisplayName = EmployeeMgtAppLocalizedResources.EmployeeViewTitle;
        }

        public void Load(EmployeeDTO employeeParam, PeriodDTO periodParam,
                     EmployeeJobPositionAssignmentDTO jobPositionAssignmentParam, ActionType actionTypeParam)
        {
            actionType = actionTypeParam;
            Employee = employeeParam;
            Period = periodParam;
            JobPositionAssignment = jobPositionAssignmentParam;

            jobPositionInPeriodService.GetJobInPeriodByJobPostionId((res, exp) => appController.BeginInvokeOnDispatcher(() =>
                {
                    if (exp == null)
                    {
                        setEmployeeCustomFields(res.CustomFields);
                    }
                    else
                        appController.HandleException(exp);

                }), period.Id, JobPositionAssignment.JobPositionId);

        }

        private void setEmployeeCustomFields(List<CustomFieldDTO> jobCustomFields)
        {
            CustomFieldValueList =
                jobCustomFields.Select(
                    c =>
                    new CustomFieldValueDTO { Id = c.Id, Name = c.Name, IsReadOnly = c.IsReadOnly, Value = "" })
                   .ToList();
            if (actionType == ActionType.ModifyEmployeeJobCustomFields)
            {
                foreach (var itm in customFieldValueList)
                {
                    if (JobPositionAssignment.CustomFieldValueList!=null && JobPositionAssignment.CustomFieldValueList.Select(c=>c.Id).Contains(itm.Id))
                        itm.Value = JobPositionAssignment.CustomFieldValueList.Single(c => c.Id == itm.Id).Value;
                }
            }
        }

        private void save()
        {
            if (JobPositionAssignment.Validate())
            {
                JobPositionAssignment.CustomFieldValueList = new ObservableCollection<CustomFieldValueDTO>(CustomFieldValueList);
                appController.Publish(new UpdateEmployeeJobPositionAssignment(jobPositionAssignment, Period, employee, actionType));
                OnRequestClose();
            }
        }

        protected override void OnRequestClose()
        {
            base.OnRequestClose();
            appController.Close(this);
        }

        #endregion
    }

}
