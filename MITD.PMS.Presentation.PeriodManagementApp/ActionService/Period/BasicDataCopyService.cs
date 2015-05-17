using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;

namespace MITD.PMS.Presentation.PeriodManagementApp
{
    public class BasicDataCopyService : IActionService<PeriodListVM>
    {
        private readonly IPeriodController periodController;
        private readonly IPMSController pmsController;
        private readonly IPeriodServiceWrapper periodService;

        public BasicDataCopyService(IPMSController pmsController, IPeriodServiceWrapper periodService, IPeriodController periodController)
        {
            this.pmsController = pmsController;
            this.periodService = periodService;
            this.periodController = periodController;
        }


        public void DoAction(PeriodListVM vm)
        {
            if (vm.SelectedPeriod != null)
            {
                periodController.ShowPeriodBasicDataCopyView(vm.SelectedPeriod);
            }
            else
                pmsController.ShowMessage("اطلاعات دوره جهت ارسال به صفحه کپی اطلاعات پایه  معتبر نمی باشد");

            

        }
    }
}

