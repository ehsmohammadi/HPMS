using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;

namespace MITD.PMS.Presentation.BasicInfoApp
{
    public class DeleteJobIndexService:IActionService<JobIndexTreeVM>
    {
        private readonly IPMSController pmsController;
        private readonly IJobIndexServiceWrapper jobIndexService;

        public DeleteJobIndexService(IPMSController pmsController,IJobIndexServiceWrapper jobIndexService)
        {
            this.pmsController = pmsController;
            this.jobIndexService = jobIndexService;
        }


        public void DoAction(JobIndexTreeVM vm)
        {
            if (vm != null)
            {
                if (pmsController.ShowConfirmationBox("آیا از عملیات حذف شاخص اطمینان دارید؟", "حذف شاخص"))
                {
                    jobIndexService.DeleteJobIndex((res, exp) => pmsController.BeginInvokeOnDispatcher(()=>
                        {
                            if (exp == null )
                            {
                                pmsController.ShowMessage("عملیات حذف شاخص با موفقیت انجام شد");
                                pmsController.Publish(new UpdateJobIndexTreeArgs());
                            }
                            else 
                                pmsController.HandleException(exp);
                        }
                        ),vm.SelectedJobIndex.Data.Id);
                }

            }
            else
                pmsController.ShowMessage("اطلاعات شاخص جهت حذف معتبر نمی باشد");
        }


    }
}

