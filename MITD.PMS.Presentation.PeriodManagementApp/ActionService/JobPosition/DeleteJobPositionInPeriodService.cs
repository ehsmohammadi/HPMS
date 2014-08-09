using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public class DeleteJobPositionInPeriodService:IActionService<JobPositionInPeriodTreeVM>
    {
        private readonly IPMSController pmsController;
        private readonly IJobPositionInPeriodServiceWrapper jobPositionInPeriodService;

        public DeleteJobPositionInPeriodService(IPMSController pmsController,IJobPositionInPeriodServiceWrapper jobPositionInPeriodService)
        {
            this.pmsController = pmsController;
            this.jobPositionInPeriodService = jobPositionInPeriodService;
        }


        public void DoAction(JobPositionInPeriodTreeVM vm)
        {
            if (vm != null)
            {
                if (pmsController.ShowConfirmationBox("آیا از عملیات حذف واحد چارت سازمانی اطمینان دارید؟", "حذف واحد چارت سازمانی"))
                {
                    jobPositionInPeriodService.DeleteJobPositionInPeriod((res, exp) => pmsController.BeginInvokeOnDispatcher(() =>
                    {
                        if (exp == null)
                        {
                            pmsController.ShowMessage("عملیات حذف واحد چارت سازمانی با موفقیت انجام شد");
                            pmsController.Publish(new UpdateJobPositionInPeriodTreeArgs());
                        }
                        else
                        {
                            pmsController.HandleException(exp);
                        }
                                
                    }),vm.Period.Id,vm.SelectedJobPositionInPeriod.Data.Id);
                }

            }
            else
                pmsController.ShowMessage("اطلاعات واحد چارت سازمانی جهت حذف معتبر نمی باشد");
        }


    }
}

