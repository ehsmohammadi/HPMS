using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;
using MITD.Presentation;

namespace MITD.PMS.Presentation.PeriodManagementApp
{
    public class ManageJobPositionInPeriodService : IActionService<PeriodListVM>
    {
        private readonly IPeriodController periodController;
        private readonly IPMSController pmsController;

        public ManageJobPositionInPeriodService(IPeriodController periodController
            ,IPMSController pmsController)
        {
            this.periodController = periodController;
            this.pmsController = pmsController;
        }


        public void DoAction(PeriodListVM vm)
        {
            if (vm.SelectedPeriod != null)
            {
                periodController.ShowJobPositionInPeriodTreeView(vm.SelectedPeriod);
            }
            else
                pmsController.ShowMessage("اطلاعات دوره جهت ارسال به صفحه مدیریت پست های چارت سازمانی معتبر نمی باشد");
        }


    }
}

