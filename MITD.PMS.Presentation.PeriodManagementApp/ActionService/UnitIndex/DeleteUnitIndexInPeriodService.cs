using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;
using MITD.PMS.Presentation.Logic.Wrapper.PeriodManagement.UnitIndex;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public class DeleteUnitIndexInPeriodService:IActionService<UnitIndexInPeriodTreeVM>
    {
        private readonly IPMSController pmsController;
        private readonly IUnitIndexInPeriodServiceWrapper unitIndexInPeriodService;

        public DeleteUnitIndexInPeriodService(IPMSController pmsController,IUnitIndexInPeriodServiceWrapper unitIndexInPeriodService)
        {
            this.pmsController = pmsController;
            this.unitIndexInPeriodService = unitIndexInPeriodService;
        }


        public void DoAction(UnitIndexInPeriodTreeVM vm)
        {
            if (vm != null)
            {
                if (pmsController.ShowConfirmationBox("آیا از عملیات حذف شاخص اطمینان دارید؟", "حذف شاخص"))
                {
                    unitIndexInPeriodService.DeleteUnitIndexInPeriod((res, exp) =>  pmsController.BeginInvokeOnDispatcher( ()=>
                        {
                            if (exp == null )
                            {
                                pmsController.ShowMessage("عملیات حذف شاخص با موفقیت انجام شد");
                                pmsController.Publish(new UpdateUnitIndexInPeriodTreeArgs());
                            }
                            else 
                                pmsController.HandleException(exp);
                            

                        })
                        ,vm.Period.Id,vm.SelectedAbstractIndexInPeriod.Data.Id);
                }

            }
            else
                pmsController.ShowMessage("اطلاعات شاخص جهت حذف معتبر نمی باشد");
        }


    }
}

