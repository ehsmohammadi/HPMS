using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;


namespace MITD.PMS.Presentation.PeriodManagementApp
{
    public class ModifyUnitInPeriodService : IActionService<UnitInPeriodTreeVM>
    {
        private readonly IPeriodController periodController;
        private readonly IPMSController pmsController;
        private readonly IUnitInPeriodServiceWrapper UnitInPeriodService;

        public ModifyUnitInPeriodService(IPeriodController periodController
            , IPMSController pmsController, IUnitInPeriodServiceWrapper UnitInPeriodService)
        {
            this.periodController = periodController;
            this.pmsController = pmsController;
            this.UnitInPeriodService = UnitInPeriodService;
        }


        public void DoAction(UnitInPeriodTreeVM vm)
        {
            UnitInPeriodService.GetUnitInPeriod((res, exp) =>pmsController.BeginInvokeOnDispatcher(()=>
                {
                    if (exp == null)
                    {
                        if (res != null)
                            periodController.ShowUnitInPeriodView(vm.Period.Id, vm.SelectedUnitInPeriod.Data.Id, ActionType.ModifyUnitInPeriod);
                        else
                            pmsController.ShowMessage("اطلاعات واحد دوره جهت ارسال به صفحه ویرایش معتبر نمی باشد");
                    }
                    else
                        pmsController.HandleException(exp);

                }), vm.Period.Id, vm.SelectedUnitInPeriod.Data.Id);



        }


    }


    
}

