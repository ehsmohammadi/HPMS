using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;

namespace MITD.PMS.Presentation.BasicInfoApp
{
    public class ModifyUnitIndexCategoryService : IActionService<UnitIndexTreeVM>
    {
        private readonly IBasicInfoController basicInfoController;
        private readonly IPMSController pmsController;
        private readonly IUnitIndexServiceWrapper unitIndexCategoryService;

        public ModifyUnitIndexCategoryService(IBasicInfoController basicInfoController
            ,IPMSController pmsController, IUnitIndexServiceWrapper unitIndexCategoryService)
        {
            this.basicInfoController = basicInfoController;
            this.pmsController = pmsController;
            this.unitIndexCategoryService = unitIndexCategoryService;
        }


        public void DoAction(UnitIndexTreeVM jlvm)
        {
            unitIndexCategoryService.GetUnitIndexCategory((res, exp) => pmsController.BeginInvokeOnDispatcher(() =>
                {
                    if (exp == null)
                    {
                        if (res != null)
                            basicInfoController.ShowUnitIndexCategoryView(res, ActionType.ModifyUnitIndexCategory);
                        else
                            pmsController.ShowMessage("اطلاعات دسته شاخص شغل جهت ارسال به صفحه ویرایش معتبر نمی باشد");
                    }
                    else
                        pmsController.HandleException(exp);

                }), jlvm.SelectedUnitIndex.Data.Id);



        }


    }


    
}

