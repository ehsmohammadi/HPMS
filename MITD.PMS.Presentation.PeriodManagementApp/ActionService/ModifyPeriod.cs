using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;
using MITD.Presentation;

namespace MITD.PMS.Presentation.PeriodManagementApp
{
    public class ModifyPeriodService:IActionService
    {
        private readonly IPeriodController periodController;
        private readonly IPMSController pmsController;

        public ModifyPeriodService(IPeriodController periodController
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
               periodController.ShowPeriod(periodDto);
            }
            else
                pmsController.ShowMessage("اطلاعات دوره جهت ارسال به صفحه ویرایش معتبر نمی باشد");
        }


    }
}

