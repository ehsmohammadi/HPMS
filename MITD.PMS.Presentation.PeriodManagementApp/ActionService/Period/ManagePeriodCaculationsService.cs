using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;

namespace MITD.PMS.Presentation.PeriodManagementApp
{
    public class ManagePeriodCaculationsService : IActionService<PeriodListVM>
    {
        private readonly IPeriodController periodController;
        private readonly IPMSController pmsController;
        private readonly IPeriodServiceWrapper periodService;

        public ManagePeriodCaculationsService(IPMSController pmsController, IPeriodServiceWrapper periodService, IPeriodController periodController)
        {
            this.pmsController = pmsController;
            this.periodService = periodService;
            this.periodController = periodController;
        }


        public void DoAction(PeriodListVM vm)
        {
            var period = vm.SelectedPeriod;
            if (period == null)
            {
                pmsController.ShowMessage("دوره ای انتخاب نشده است");
                return;
            }
            periodController.ShowCalculationListView(period);

        }
    }
}

