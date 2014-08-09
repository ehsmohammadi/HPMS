using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;
using MITD.Presentation;

namespace MITD.PMS.Presentation.BasicInfoApp
{
    public class DeleteJobPositionService:IActionService<JobPositionListVM>
    {
        private readonly IPMSController pmsController;
        private readonly IJobPositionServiceWrapper jobPositionService;

        public DeleteJobPositionService(IPMSController pmsController,IJobPositionServiceWrapper jobPositionService)
        {
            this.pmsController = pmsController;
            this.jobPositionService = jobPositionService;
        }


        public void DoAction(JobPositionListVM vm)
        {
            if (vm != null)
            {
                if (pmsController.ShowConfirmationBox("آیا از عملیات حذف پست سازمانی اطمینان دارید؟", "حذف پست سازمانی"))
                {
                    jobPositionService.DeleteJobPosition((res, exp) => pmsController.BeginInvokeOnDispatcher(() =>
                    {
                        if (exp == null)
                        {
                            pmsController.ShowMessage("عملیات حذف پست سازمانی با موفقیت انجام شد");
                            pmsController.Publish(new UpdateJobPositionListArgs());
                        }
                        else
                            pmsController.HandleException(exp);
                    }),vm.SelectedJobPosition.Id);
                }

            }
            else
                pmsController.ShowMessage("اطلاعات پست سازمانی جهت حذف معتبر نمی باشد");
        }


    }
}

