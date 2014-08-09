using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;
using MITD.Presentation;

namespace MITD.PMS.Presentation.EmployeeManagement
{
    public class ModifyEmployeeJobCustomFieldsSevice : IActionService<EmployeeJobPositionsVM>
    {
        private readonly IEmployeeController employeeController;
        private readonly IPMSController pmsController;

        public ModifyEmployeeJobCustomFieldsSevice(IEmployeeController employeeController
            , IPMSController pmsController)
        {
            this.employeeController = employeeController;
            this.pmsController = pmsController;
        }

        public void DoAction(EmployeeJobPositionsVM vm)
        {
            if (vm.SelectedJobPositionDuration == null)
                return;
            employeeController.ShowEmployeeJobCustomFieldsView(vm.Employee, vm.Period, vm.SelectedJobPositionDuration,
                                                         ActionType.ModifyEmployeeJobCustomFields);
        }


    }
}

