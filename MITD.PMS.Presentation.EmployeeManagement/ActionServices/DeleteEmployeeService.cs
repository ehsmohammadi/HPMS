using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;
using MITD.Presentation;

namespace MITD.PMS.Presentation.EmployeeManagement
{
    public class DeleteEmployeeService:IActionService<EmployeeListVM>
    {
        private readonly IPMSController pmsController;
        private readonly IEmployeeServiceWrapper employeeService;

        public DeleteEmployeeService(IPMSController pmsController,IEmployeeServiceWrapper employeeService)
        {
            this.pmsController = pmsController;
            this.employeeService = employeeService;
        }


        public void DoAction(EmployeeListVM vm)
        {
                if (pmsController.ShowConfirmationBox("آیا از عملیات حذف کارمند اطمینان دارید؟", "حذف کارمند"))
                {
                    employeeService.DeleteEmployee((res, exp) => pmsController.BeginInvokeOnDispatcher(() =>
                    {
                        if (exp == null)
                        {
                            pmsController.ShowMessage("عملیات حذف کارمند با موفقیت انجام شد");
                            pmsController.Publish(new UpdateEmployeeListArgs());
                        }
                        else
                        {
                            pmsController.HandleException(exp);
                        }
                    }),vm.Period.Id, vm.SelectedEmployee.PersonnelNo);
                }

           
        }


    }
}

