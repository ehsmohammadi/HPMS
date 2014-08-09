using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;

namespace MITD.PMS.Presentation.BasicInfoApp
{
    public class ModifyJobIndexService:IActionService<JobIndexTreeVM>
    {
        private readonly IBasicInfoController basicInfoController;
        private readonly IPMSController pmsController;
        private readonly IJobIndexServiceWrapper jobIndexService;

        public ModifyJobIndexService(IBasicInfoController basicInfoController
            ,IPMSController pmsController, IJobIndexServiceWrapper jobIndexService)
        {
            this.basicInfoController = basicInfoController;
            this.pmsController = pmsController;
            this.jobIndexService = jobIndexService;
        }


        public void DoAction(JobIndexTreeVM vm)
        {
            jobIndexService.GetJobIndex((res, exp) => pmsController.BeginInvokeOnDispatcher(() =>
            {
                if (exp == null)
                {
                    if (res != null)
                        basicInfoController.ShowJobIndexView(res, ActionType.ModifyJobIndex);
                    else
                        pmsController.ShowMessage("اطلاعات فیلد جهت ارسال به صفحه ویرایش معتبر نمی باشد");
                }
                else
                    pmsController.HandleException(exp);

            }), vm.SelectedJobIndex.Data.Id);
        }


    }
}

