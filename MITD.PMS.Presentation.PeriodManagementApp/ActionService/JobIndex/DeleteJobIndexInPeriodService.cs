using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public class DeleteJobIndexInPeriodService:IActionService<JobIndexInPeriodTreeVM>
    {
        private readonly IPMSController pmsController;
        private readonly IJobIndexInPeriodServiceWrapper jobIndexInPeriodService;

        public DeleteJobIndexInPeriodService(IPMSController pmsController,IJobIndexInPeriodServiceWrapper jobIndexInPeriodService)
        {
            this.pmsController = pmsController;
            this.jobIndexInPeriodService = jobIndexInPeriodService;
        }


        public void DoAction(JobIndexInPeriodTreeVM vm)
        {
            if (vm != null)
            {
                if (pmsController.ShowConfirmationBox("آیا از عملیات حذف شاخص اطمینان دارید؟", "حذف شاخص"))
                {
                    jobIndexInPeriodService.DeleteJobIndexInPeriod((res, exp) =>  pmsController.BeginInvokeOnDispatcher( ()=>
                        {
                            if (exp == null )
                            {
                                pmsController.ShowMessage("عملیات حذف شاخص با موفقیت انجام شد");
                                pmsController.Publish(new UpdateJobIndexInPeriodTreeArgs());
                            }
                            else 
                                pmsController.HandleException(exp);
                            

                        })
                        ,vm.Period.Id,vm.SelectedAbstractIndexInPeriod.Data.Id);
                }

            }
            else
                pmsController.ShowMessage("اطلاعات شاخص جهت حذف معتبر نمی باشد");
        }


    }
}

