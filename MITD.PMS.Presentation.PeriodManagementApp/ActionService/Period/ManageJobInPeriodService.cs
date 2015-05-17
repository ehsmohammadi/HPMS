using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;
using MITD.Presentation;

namespace MITD.PMS.Presentation.PeriodManagementApp
{
    public class ManageJobInPeriodService : IActionService<PeriodListVM>
    {
        private readonly IPeriodController periodController;
        private readonly IPMSController pmsController;

        public ManageJobInPeriodService(IPeriodController periodController,
                                         IPMSController pmsController)
        {
            this.periodController = periodController;
            this.pmsController = pmsController;
        }


        public void DoAction(PeriodListVM vm)
        {
            if (vm.SelectedPeriod != null)
            {
                periodController.ShowJobInPeriodListView(vm.SelectedPeriod);
            }
            else
                pmsController.ShowMessage("اطلاعات دوره جهت ارسال به صفحه مدیریت شغل ها معتبر نمی باشد");

        }


    }
}

