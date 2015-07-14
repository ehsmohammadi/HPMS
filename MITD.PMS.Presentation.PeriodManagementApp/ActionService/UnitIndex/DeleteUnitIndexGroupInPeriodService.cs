using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;
using MITD.PMS.Presentation.Logic.Wrapper.PeriodManagement.UnitIndex;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public class DeleteUnitIndexGroupInPeriodService:IActionService<UnitIndexInPeriodTreeVM>
    {
        private readonly IPMSController pmsController;
        private readonly IUnitIndexInPeriodServiceWrapper unitIndexInPeriodService;

        public DeleteUnitIndexGroupInPeriodService(IPMSController pmsController, IUnitIndexInPeriodServiceWrapper unitIndexInPeriodService)
        {
            this.pmsController = pmsController;
            this.unitIndexInPeriodService = unitIndexInPeriodService;
        }


        public void DoAction(UnitIndexInPeriodTreeVM vm)
        {
            if (vm != null)
            {
                if (pmsController.ShowConfirmationBox("آیا از عملیات حذف گروه شاخص اطمینان دارید؟", "حذف گروه شاخص"))
                {
                    unitIndexInPeriodService.DeleteUnitIndexGroupInPeriod((res, exp) => pmsController.BeginInvokeOnDispatcher( ()=>
                        {
                            if (exp == null )
                            {
                                pmsController.ShowMessage("عملیات حذف گروه شاخص با موفقیت انجام شد");
                                pmsController.Publish(new UpdateUnitIndexInPeriodTreeArgs());
                            }
                            else 
                                pmsController.HandleException(exp);
                        })
                       ,vm.Period.Id ,vm.SelectedAbstractIndexInPeriod.Data.Id);
                }
            }
            else
                pmsController.ShowMessage("اطلاعات واحد چارت سازمانی جهت حذف معتبر نمی باشد");
        }


    }
}

