using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;

namespace MITD.PMS.Presentation.BasicInfoApp
{
    public class ModifyJobService : IActionService<JobListVM>
    {
        private readonly IBasicInfoController basicInfoController;
        private readonly IPMSController pmsController;
        private readonly IJobServiceWrapper jobService;

        public ModifyJobService(IBasicInfoController basicInfoController
            ,IPMSController pmsController, IJobServiceWrapper jobService)
        {
            this.basicInfoController = basicInfoController;
            this.pmsController = pmsController;
            this.jobService = jobService;
        }


        public void DoAction(JobListVM vm)
        {
            jobService.GetJob((res, exp) => pmsController.BeginInvokeOnDispatcher(() =>
                {
                    if (exp == null)
                    {
                        if (res != null)
                            basicInfoController.ShowJobView(res, ActionType.ModifyJob);
                        else
                            pmsController.ShowMessage("اطلاعات شغل جهت ارسال به صفحه ویرایش معتبر نمی باشد");
                    }
                    else
                        pmsController.HandleException(exp);

                }), vm.SelectedJob.Id);



        }


    }


    
}

