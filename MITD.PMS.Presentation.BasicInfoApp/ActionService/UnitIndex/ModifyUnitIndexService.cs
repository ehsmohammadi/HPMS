using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;

namespace MITD.PMS.Presentation.BasicInfoApp
{
    public class ModifyUnitIndexService:IActionService<UnitIndexTreeVM>
    {
        private readonly IBasicInfoController basicInfoController;
        private readonly IPMSController pmsController;
        private readonly IUnitIndexServiceWrapper unitIndexService;

        public ModifyUnitIndexService(IBasicInfoController basicInfoController
            ,IPMSController pmsController, IUnitIndexServiceWrapper unitIndexService)
        {
            this.basicInfoController = basicInfoController;
            this.pmsController = pmsController;
            this.unitIndexService = unitIndexService;
        }


        public void DoAction(UnitIndexTreeVM vm)
        {
            unitIndexService.GetUnitIndex((res, exp) => pmsController.BeginInvokeOnDispatcher(() =>
            {
                if (exp == null)
                {
                    if (res != null)
                        basicInfoController.ShowUnitIndexView(res, ActionType.ModifyUnitIndex);
                    else
                        pmsController.ShowMessage("اطلاعات فیلد جهت ارسال به صفحه ویرایش معتبر نمی باشد");
                }
                else
                    pmsController.HandleException(exp);

            }), vm.SelectedUnitIndex.Data.Id);
        }


    }
}

