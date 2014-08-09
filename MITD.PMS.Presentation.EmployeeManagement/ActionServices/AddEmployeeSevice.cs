using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;
using MITD.Presentation;

namespace MITD.PMS.Presentation.EmployeeManagement
{
    public class AddEmployeeService : IActionService<EmployeeListVM>
    {
        private readonly IEmployeeController employeeController;
        private readonly IPMSController pmsController;

        public AddEmployeeService(IEmployeeController employeeController
            , IPMSController pmsController)
        {
            this.employeeController = employeeController;
            this.pmsController = pmsController;
        }

        public void DoAction(EmployeeListVM vm)
        {
            var employee = new EmployeeDTO {PeriodId = vm.Period.Id};
            employeeController.ShowEmployeeView(employee, ActionType.AddEmployee);
        }


    }
}

