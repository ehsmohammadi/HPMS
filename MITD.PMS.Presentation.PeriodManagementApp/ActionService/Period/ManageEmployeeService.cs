using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;
using MITD.Presentation;

namespace MITD.PMS.Presentation.PeriodManagementApp
{
    public class ManageEmployeeService : IActionService<PeriodListVM>
    {
        private readonly IPMSController pmsController;

        public ManageEmployeeService(IPMSController pmsController)
        {
            this.pmsController = pmsController;
        }


        public void DoAction(PeriodListVM vm)
        {
            if (vm.SelectedPeriod != null)
            {
                pmsController.ShowEmployeeListView(vm.SelectedPeriod);
            }
            else
                pmsController.ShowMessage("اطلاعات دوره جهت ارسال به صفحه مدیریت کارمندان معتبر نمی باشد");
        }


    }
}

