using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public class DeleteUnitInPeriodService:IActionService<UnitInPeriodTreeVM>
    {
        private readonly IPMSController pmsController;
        private readonly IUnitInPeriodServiceWrapper unitInPeriodService;

        public DeleteUnitInPeriodService(IPMSController pmsController,IUnitInPeriodServiceWrapper unitInPeriodService)
        {
            this.pmsController = pmsController;
            this.unitInPeriodService = unitInPeriodService;
        }


        public void DoAction(UnitInPeriodTreeVM vm)
        {
            if (vm != null)
            {
                if (pmsController.ShowConfirmationBox("آیا از عملیات حذف واحد چارت سازمانی اطمینان دارید؟", "حذف واحد چارت سازمانی"))
                {
                    unitInPeriodService.DeleteUnitInPeriod((res, exp) => pmsController.BeginInvokeOnDispatcher(() =>
                    {
                        if (exp == null)
                        {
                            pmsController.ShowMessage("عملیات حذف واحد چارت سازمانی با موفقیت انجام شد");
                            pmsController.Publish(new UpdateUnitInPeriodTreeArgs());
                        }
                        else
                        {
                            pmsController.HandleException(exp);
                        }
                    }),vm.Period.Id,vm.SelectedUnitInPeriod.Data.Id);
                }

            }
            else
                pmsController.ShowMessage("اطلاعات واحد چارت سازمانی جهت حذف معتبر نمی باشد");
        }


    }
}

