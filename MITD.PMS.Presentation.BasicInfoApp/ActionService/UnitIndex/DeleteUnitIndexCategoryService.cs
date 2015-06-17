using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;

namespace MITD.PMS.Presentation.BasicInfoApp
{
    public class DeleteUnitIndexCategoryService:IActionService<UnitIndexTreeVM>
    {
        private readonly IPMSController pmsController;
        private readonly IUnitIndexServiceWrapper unitIndexCategoryService;

        public DeleteUnitIndexCategoryService(IPMSController pmsController,IUnitIndexServiceWrapper unitIndexCategoryService)
        {
            this.pmsController = pmsController;
            this.unitIndexCategoryService = unitIndexCategoryService;
        }


        public void DoAction(UnitIndexTreeVM vm)
        {
            if (vm != null)
            {
                if (pmsController.ShowConfirmationBox("آیا از عملیات حذف دسته شاخص اطمینان دارید؟", "حذف دسته شاخص"))
                {
                    unitIndexCategoryService.DeleteUnitIndexCategory((res, exp) => pmsController.BeginInvokeOnDispatcher(()=>
                        {
                            if (exp == null )
                            {
                                pmsController.ShowMessage("عملیات حذف دسته شاخص با موفقیت انجام شد");
                                pmsController.Publish(new UpdateUnitIndexTreeArgs());
                            }
                            else 
                                pmsController.HandleException(exp);
                        }
                        ),vm.SelectedUnitIndex.Data.Id);
                }

            }
            else
                pmsController.ShowMessage("اطلاعات دسته شاخص جهت  حذف معتبر نمی باشد");
        }


    }
}

