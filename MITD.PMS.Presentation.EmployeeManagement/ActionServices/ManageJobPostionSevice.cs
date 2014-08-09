using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;
using MITD.Presentation;

namespace MITD.PMS.Presentation.EmployeeManagement
{
    public class ManageJobPostionSevice : IActionService<EmployeeListVM>
    {
        private readonly IEmployeeController employeeController;
        private readonly IPMSController pmsController;

        public ManageJobPostionSevice(IEmployeeController employeeController
            , IPMSController pmsController)
        {
            this.employeeController = employeeController;
            this.pmsController = pmsController;
        }

        public void DoAction(EmployeeListVM vm)
        {
            if (vm.SelectedEmployee == null)
                return;
            employeeController.ShowEmployeeJobPositionsView(vm.SelectedEmployee,vm.Period);
        }


    }
}

