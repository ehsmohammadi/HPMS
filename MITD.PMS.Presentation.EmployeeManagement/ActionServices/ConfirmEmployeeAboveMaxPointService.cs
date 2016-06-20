using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;

namespace MITD.PMS.Presentation.PeriodManagementApp
{
    public class ConfirmEmployeeAboveMaxPointService : IActionService<EmployeeListVM>
    {

        private readonly IPMSController pmsController;
        private readonly IEmployeeServiceWrapper employeeService;


        public ConfirmEmployeeAboveMaxPointService(IPMSController pmsController, IEmployeeServiceWrapper employeeService)
        {
            this.pmsController = pmsController;
            this.employeeService = employeeService;

        }

        public void DoAction(EmployeeListVM vm)
        {
            var employee = vm.SelectedEmployee;
            if (employee == null)
            {
                pmsController.ShowMessage("کارمندی انتخاب نشده است");
                return;
            }
            if (pmsController.ShowConfirmationBox("آیا مخواهید نمره کارمند مورد نظر را تایید کنید ؟", "تایید نمره کارمند"))
            {
                employeeService.ConfirmEmployeeAboveMaxPoint(exp => pmsController.BeginInvokeOnDispatcher(() =>
                {
                    if (exp != null)
                        pmsController.HandleException(exp);
                    else
                    {
                        pmsController.Publish(new UpdateEmployeeListArgs());
                    }
                }), employee.PersonnelNo,vm.Period.Id);
            }

        }
        //public void DoAction(PeriodListVM vm)
        //{
        //    var period = vm.SelectedPeriod;
        //    if (period == null)
        //    {
        //        pmsController.ShowMessage("دوره ای انتخاب نشده است");
        //        return;
        //    }
        //    if (pmsController.ShowConfirmationBox("برگشت دوره میتواند موجب حذف بخشی از اطلاعات شود، آیا از برگشت دوره به وضعیت قبل اطمینان دارید ؟", "برگشت دوره"))
        //    {
        //        periodService.RollBackPeriodState(exp => pmsController.BeginInvokeOnDispatcher(() =>
        //        {
        //            if (exp != null)
        //                pmsController.HandleException(exp);
        //            else
        //            {
        //                pmsController.Publish(new UpdatePeriodListArgs());
        //                pmsController.GetCurrentPeriod();
        //            }
        //        }), period.Id);
        //    }

        //}

    }
}

