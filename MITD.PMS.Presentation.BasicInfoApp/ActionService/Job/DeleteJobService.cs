using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;

namespace MITD.PMS.Presentation.BasicInfoApp
{
    public class DeleteJobService:IActionService<JobListVM>
    {
        private readonly IPMSController pmsController;
        private readonly IJobServiceWrapper jobService;

        public DeleteJobService(IPMSController pmsController,IJobServiceWrapper jobService)
        {
            this.pmsController = pmsController;
            this.jobService = jobService;
        }


        public void DoAction(JobListVM vm)
        {
            if (pmsController.ShowConfirmationBox("آیا از عملیات حذف شغل اطمینان دارید؟", "حذف شغل"))
            {
                jobService.DeleteJob((res, exp) => pmsController.BeginInvokeOnDispatcher(() =>
                    {
                        if (exp == null)
                        {
                            pmsController.ShowMessage("عملیات حذف شغل با موفقیت انجام شد");
                            pmsController.Publish(new UpdateJobListArgs());
                        }
                        else
                            pmsController.HandleException(exp);
                    }), vm.SelectedJob.Id);

            }
        }


    }
}

