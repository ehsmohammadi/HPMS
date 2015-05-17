using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;

namespace MITD.PMS.Presentation.BasicInfoApp
{
    public class DeleteCustomFieldService:IActionService<CustomFieldListVM>
    {
        private readonly IPMSController pmsController;
        private readonly ICustomFieldServiceWrapper customFieldService;

        public DeleteCustomFieldService(IPMSController pmsController,ICustomFieldServiceWrapper customFieldService)
        {
            this.pmsController = pmsController;
            this.customFieldService = customFieldService;
        }


        public void DoAction(CustomFieldListVM vm)
        {
            
            if (vm != null)
            {
                if (pmsController.ShowConfirmationBox("آیا از عملیات حذف فیلد اطمینان دارید؟", "حذف فیلد"))
                {
                    customFieldService.DeleteCustomField((res, exp) => pmsController.BeginInvokeOnDispatcher(()=>
                        {
                            if (exp == null)
                            {
                                pmsController.ShowMessage("عملیات حذف فیلد با موفقیت انجام شد");
                                pmsController.Publish(new UpdateCustomFieldListArgs());
                            }
                            else 
                            {
                                pmsController.HandleException(exp);
                            }

                        }
                        ),vm.SelectedCustomField.Id);
                }

            }
            else
                pmsController.ShowMessage("اطلاعات فیلد جهت حذف معتبر نمی باشد");
        }


    }
}

