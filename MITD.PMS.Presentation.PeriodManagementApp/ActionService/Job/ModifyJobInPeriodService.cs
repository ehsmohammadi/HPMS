using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;


namespace MITD.PMS.Presentation.PeriodManagementApp
{
    public class ModifyJobInPeriodService : IActionService<JobInPeriodListVM>
    {
        private readonly IPeriodController periodController;
        private readonly IPMSController pmsController;
        private readonly IJobInPeriodServiceWrapper jobInPeriodService;

        public ModifyJobInPeriodService(IPeriodController periodController
            , IPMSController pmsController, IJobInPeriodServiceWrapper jobInPeriodService)
        {
            this.periodController = periodController;
            this.pmsController = pmsController;
            this.jobInPeriodService = jobInPeriodService;
        }


        public void DoAction(JobInPeriodListVM vm)
        {
            jobInPeriodService.GetJobInPeriod((res, exp) =>pmsController.BeginInvokeOnDispatcher(()=>
                {
                    if (exp == null)
                    {
                        if (res != null)
                            periodController.ShowJobInPeriodView(vm.Period.Id, vm.SelectedJobInPeriod.JobId, ActionType.ModifyJobInPeriod);
                        else
                            pmsController.ShowMessage("اطلاعات شغل دوره جهت ارسال به صفحه ویرایش معتبر نمی باشد");
                    }
                    else
                        pmsController.HandleException(exp);

                }), vm.Period.Id, vm.SelectedJobInPeriod.JobId);



        }


    }


    
}

