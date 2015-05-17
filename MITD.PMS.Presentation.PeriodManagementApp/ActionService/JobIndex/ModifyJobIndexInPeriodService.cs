using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;


namespace MITD.PMS.Presentation.PeriodManagementApp
{
    public class ModifyJobIndexInPeriodService : IActionService<JobIndexInPeriodTreeVM>
    {
        private readonly IPeriodController periodController;
        private readonly IPMSController pmsController;
        private readonly IJobIndexInPeriodServiceWrapper jobIndexService;

        public ModifyJobIndexInPeriodService(IPeriodController periodController
            , IPMSController pmsController, IJobIndexInPeriodServiceWrapper jobIndexService)
        {
            this.periodController = periodController;
            this.pmsController = pmsController;
            this.jobIndexService = jobIndexService;
        }


        public void DoAction(JobIndexInPeriodTreeVM vm)
        {
            jobIndexService.GetJobIndexInPeriod((res, exp) =>pmsController.BeginInvokeOnDispatcher(()=>
                {
                    if (exp == null)
                    {
                        if (res != null)
                            periodController.ShowJobIndexInPeriodView(res, ActionType.ModifyJobIndexInPeriod);
                        else
                            pmsController.ShowMessage("اطلاعات شاخص جهت ارسال به صفحه ویرایش معتبر نمی باشد");
                    }
                    else
                        pmsController.HandleException(exp);

                }),vm.Period.Id, vm.SelectedAbstractIndexInPeriod.Data.Id);



        }


    }


    
}

