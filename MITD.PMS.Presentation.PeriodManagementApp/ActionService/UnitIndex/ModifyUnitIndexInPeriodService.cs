using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;
using MITD.PMS.Presentation.Logic.Wrapper.PeriodManagement.UnitIndex;


namespace MITD.PMS.Presentation.PeriodManagementApp
{
    public class ModifyUnitIndexInPeriodService : IActionService<UnitIndexInPeriodTreeVM>
    {
        private readonly IPeriodController periodController;
        private readonly IPMSController pmsController;
        private readonly IUnitIndexInPeriodServiceWrapper unitIndexService;

        public ModifyUnitIndexInPeriodService(IPeriodController periodController
            , IPMSController pmsController, IUnitIndexInPeriodServiceWrapper unitIndexService)
        {
            this.periodController = periodController;
            this.pmsController = pmsController;
            this.unitIndexService = unitIndexService;
        }


        public void DoAction(UnitIndexInPeriodTreeVM vm)
        {
            unitIndexService.GetUnitIndexInPeriod((res, exp) =>pmsController.BeginInvokeOnDispatcher(()=>
                {
                    if (exp == null)
                    {
                        if (res != null)
                            periodController.ShowUnitIndexInPeriodView(res, ActionType.ModifyUnitIndexInPeriod);
                        else
                            pmsController.ShowMessage("اطلاعات شاخص جهت ارسال به صفحه ویرایش معتبر نمی باشد");
                    }
                    else
                        pmsController.HandleException(exp);

                }),vm.Period.Id, vm.SelectedAbstractIndexInPeriod.Data.Id);



        }


    }


    
}

