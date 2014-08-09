using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;
using MITD.PMS.Presentation.PeriodManagementApp;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public class DeleteJobInPeriodService : IActionService<JobInPeriodListVM>
    {
        private readonly IPMSController pmsController;
        private readonly IJobInPeriodServiceWrapper jobInPeriodService;

        public DeleteJobInPeriodService(IPMSController pmsController, IJobInPeriodServiceWrapper jobInPeriodService)
        {
            this.pmsController = pmsController;
            this.jobInPeriodService = jobInPeriodService;
        }


        public void DoAction(JobInPeriodListVM vm)
        {
            if (vm != null && vm.SelectedJobInPeriod!= null)
            {
                if (pmsController.ShowConfirmationBox("آیا از عملیات حذف شغل از دوره اطمینان دارید؟", "حذف شغل از دوره"))
                {
                    jobInPeriodService.DeleteJobInPeriod((res, exp) =>  pmsController.BeginInvokeOnDispatcher(() =>
                        {
                            {
                                if (exp == null)
                                {
                                    pmsController.ShowMessage("عملیات حذف شغل از دوره با موفقیت انجام شد");
                                    pmsController.Publish(new UpdateJobInPeriodListArgs());
                                }
                                else
                                    pmsController.HandleException(exp);
                            }
                        }), vm.Period.Id, vm.SelectedJobInPeriod.JobId);
                }

            }
            else
                pmsController.ShowMessage("اطلاعات شغل دوره جهت حذف معتبر نمی باشد");
        }


    }
}

