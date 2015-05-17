using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;
using MITD.Presentation;

namespace MITD.PMS.Presentation.BasicInfoApp
{
    public class ModifyUnitService:IActionService<UnitListVM>
    {
        private readonly IBasicInfoController basicInfoController;
        private readonly IPMSController pmsController;
        private readonly IUnitServiceWrapper unitService;

        public ModifyUnitService(IBasicInfoController basicInfoController
            ,IPMSController pmsController, IUnitServiceWrapper unitService)
        {
            this.basicInfoController = basicInfoController;
            this.pmsController = pmsController;
            this.unitService = unitService;
        }


        public void DoAction(UnitListVM vm)
        {
            if (vm != null)
            {
               unitService.GetUnit((res, exp) => pmsController.BeginInvokeOnDispatcher(() =>
                   {
                       if (exp == null )
                            basicInfoController.ShowUnitView(res, ActionType.ModifyUnit);
                       else
                           pmsController.HandleException(exp);

                   }),vm.SelectedUnit.Id);
            }
            else
                pmsController.ShowMessage("اطلاعات واحد سازمانی جهت ارسال به صفحه ویرایش معتبر نمی باشد");
        }


    }
}

