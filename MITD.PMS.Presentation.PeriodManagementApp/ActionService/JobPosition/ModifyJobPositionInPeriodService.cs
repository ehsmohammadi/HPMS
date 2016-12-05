using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;


namespace MITD.PMS.Presentation.PeriodManagementApp
{
    public class ModifyJobPositionInPeriodService : IActionService<JobPositionInPeriodTreeVM>
    {
        private readonly IPeriodController periodController;
        private readonly IPMSController pmsController;
        private readonly IJobPositionInPeriodServiceWrapper jobPositionInPeriodService;

        public ModifyJobPositionInPeriodService(IPeriodController periodController
            , IPMSController pmsController, IJobPositionInPeriodServiceWrapper jobPositionInPeriodService)
        {
            this.periodController = periodController;
            this.pmsController = pmsController;
            this.jobPositionInPeriodService = jobPositionInPeriodService;
        }


        public void DoAction(JobPositionInPeriodTreeVM vm)
        {
            jobPositionInPeriodService.GetJobPositionInPeriod((res, exp) =>pmsController.BeginInvokeOnDispatcher(()=>
                {
                    if (exp == null)
                    {
                        if (res != null)
                            periodController.ShowJobPositionInPeriodView(res, ActionType.ModifyJobPositionInPeriod);
                        else
                            pmsController.ShowMessage("اطلاعات شغل دوره جهت ارسال به صفحه ویرایش معتبر نمی باشد");
                    }
                    else
                        pmsController.HandleException(exp);

                }), vm.Period.Id, vm.SelectedJobPositionInPeriod.Data.Id);



        }


    }


    
}

