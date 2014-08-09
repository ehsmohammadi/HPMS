using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;
using MITD.Presentation;

namespace MITD.PMS.Presentation.PeriodManagementApp
{
    public class ShowOrganizationChartInPrdMgntService:IActionService
    {
        private readonly IPeriodController periodController;
        private readonly IPMSController pmsController;

        public ShowOrganizationChartInPrdMgntService(IPeriodController periodController
            ,IPMSController pmsController)
        {
            this.periodController = periodController;
            this.pmsController = pmsController;
        }


        public void DoAction(ViewModelBase dto)
        {
            var periodDto = (PeriodDto) dto;
            if (periodDto != null)
            {
                periodController.ShowOrganizationChartInPrdMgnt(periodDto);
            }
            else
                pmsController.ShowMessage("اطلاعات دوره جهت ارسال به صفحه مدیریت چارت سازمانی معتبر نمی باشد");
        }


    }
}

