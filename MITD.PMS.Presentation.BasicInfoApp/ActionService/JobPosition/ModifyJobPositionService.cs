using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;
using MITD.Presentation;

namespace MITD.PMS.Presentation.BasicInfoApp
{
    public class ModifyJobPositionService:IActionService<JobPositionListVM>
    {
        private readonly IBasicInfoController basicInfoController;
        private readonly IPMSController pmsController;
        private readonly IJobPositionServiceWrapper jobPositionService;

        public ModifyJobPositionService(IBasicInfoController basicInfoController
            ,IPMSController pmsController, IJobPositionServiceWrapper jobPositionService)
        {
            this.basicInfoController = basicInfoController;
            this.pmsController = pmsController;
            this.jobPositionService = jobPositionService;
        }


        public void DoAction(JobPositionListVM vm)
        {
            if (vm != null)
            {
               jobPositionService.GetJobPosition((res, exp) => pmsController.BeginInvokeOnDispatcher(() =>
                   {
                       if (exp == null )
                            basicInfoController.ShowJobPositionView(res, ActionType.ModifyJobPosition);
                       else
                           pmsController.HandleException(exp);

                   }),vm.SelectedJobPosition.Id);
            }
            else
                pmsController.ShowMessage("اطلاعات پست سازمانی جهت ارسال به صفحه ویرایش معتبر نمی باشد");
        }


    }
}

