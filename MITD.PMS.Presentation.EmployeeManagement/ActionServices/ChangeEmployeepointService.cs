using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;
using MITD.Presentation;

namespace MITD.PMS.Presentation.EmployeeManagement
{
    public class ChangeEmployeepointService:IActionService<EmployeeListVM>
    {
        private readonly IEmployeeController employeeController;
        private readonly IPMSController pmsController;
        private readonly IEmployeeServiceWrapper employeeService;

        public ChangeEmployeepointService(IEmployeeController employeeController
            ,IPMSController pmsController,
            IEmployeeServiceWrapper employeeService)
        { 
            this.employeeController = employeeController;
            this.pmsController = pmsController;
            this.employeeService = employeeService;
        }


        public void DoAction(EmployeeListVM vm)
        {
            employeeService.GetEmployee((res, exp) => pmsController.BeginInvokeOnDispatcher( ()=>
            {
                if (exp == null)
                {
                    if (res != null)
                        employeeController.ShowEmployeeView(res, ActionType.ChangeEmployeePoint);
                    else
                        pmsController.ShowMessage("اطلاعات کارمند جهت ارسال به صفحه ویرایش معتبر نمی باشد");
                }
                else
                    pmsController.HandleException(exp);

            }),vm.Period.Id, vm.SelectedEmployee.PersonnelNo);
        }


    }
}

