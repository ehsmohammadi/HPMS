using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;

namespace MITD.PMS.Presentation.BasicInfoApp
{
    public class DeleteUnitIndexService:IActionService<UnitIndexTreeVM>
    {
        private readonly IPMSController pmsController;
        private readonly IUnitIndexServiceWrapper unitIndexService;

        public DeleteUnitIndexService(IPMSController pmsController,IUnitIndexServiceWrapper unitIndexService)
        {
            this.pmsController = pmsController;
            this.unitIndexService = unitIndexService;
        }


        public void DoAction(UnitIndexTreeVM vm)
        {
            if (vm != null)
            {
                if (pmsController.ShowConfirmationBox("آیا از عملیات حذف شاخص اطمینان دارید؟", "حذف شاخص"))
                {
                    unitIndexService.DeleteUnitIndex((res, exp) => pmsController.BeginInvokeOnDispatcher(()=>
                        {
                            if (exp == null )
                            {
                                pmsController.ShowMessage("عملیات حذف شاخص با موفقیت انجام شد");
                                pmsController.Publish(new UpdateUnitIndexTreeArgs());
                            }
                            else 
                                pmsController.HandleException(exp);
                        }
                        ),vm.SelectedUnitIndex.Data.Id);
                }

            }
            else
                pmsController.ShowMessage("اطلاعات شاخص جهت حذف معتبر نمی باشد");
        }


    }
}

