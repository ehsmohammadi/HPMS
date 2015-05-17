using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;
using MITD.Presentation;

namespace MITD.PMS.Presentation.BasicInfoApp
{
    public class ShowLogService:IActionService<LogListVM>
    {
        private readonly IBasicInfoController basicInfoController;
        private readonly IPMSController pmsController;
        private readonly ILogServiceWrapper logService;

        public ShowLogService(IBasicInfoController basicInfoController
            ,IPMSController pmsController, ILogServiceWrapper logService)
        {
            this.basicInfoController = basicInfoController;
            this.pmsController = pmsController;
            this.logService = logService;
        }


        public void DoAction(LogListVM vm)
        {
            if (vm != null)
            {
               logService.GetLog((res, exp) => pmsController.BeginInvokeOnDispatcher(() =>
                   {
                       if (exp == null )
                            basicInfoController.ShowLogView(res);
                       else
                           pmsController.HandleException(exp);

                   }),vm.SelectedLog.Id);
            }
            else
                pmsController.ShowMessage("اطلاعات لاگ جهت ارسال به صفحه ویرایش معتبر نمی باشد");
        }


    }
}

