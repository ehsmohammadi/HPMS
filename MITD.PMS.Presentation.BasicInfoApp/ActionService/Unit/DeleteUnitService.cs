using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;
using MITD.Presentation;

namespace MITD.PMS.Presentation.BasicInfoApp
{
    public class DeleteUnitService:IActionService<UnitListVM>
    {
        private readonly IPMSController pmsController;
        private readonly IUnitServiceWrapper unitService;

        public DeleteUnitService(IPMSController pmsController,IUnitServiceWrapper unitService)
        {
            this.pmsController = pmsController;
            this.unitService = unitService;
        }


        public void DoAction(UnitListVM vm)
        {
            if (vm != null)
            {
                if (pmsController.ShowConfirmationBox("آیا از عملیات حذف واحد سازمانی اطمینان دارید؟", "حذف واحد سازمانی"))
                {
                    unitService.DeleteUnit((res, exp) => pmsController.BeginInvokeOnDispatcher(() =>
                    {
                        if (exp == null)
                        {
                            pmsController.ShowMessage("عملیات حذف واحد سازمانی با موفقیت انجام شد");
                            pmsController.Publish(new UpdateUnitListArgs());
                        }
                        else
                            pmsController.HandleException(exp);
                    }),vm.SelectedUnit.Id);
                }

            }
            else
                pmsController.ShowMessage("اطلاعات واحد سازمانی جهت حذف معتبر نمی باشد");
        }


    }
}

