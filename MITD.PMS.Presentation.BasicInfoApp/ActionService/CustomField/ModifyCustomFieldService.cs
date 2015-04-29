using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;
 
namespace MITD.PMS.Presentation.BasicInfoApp
{
    public class ModifyCustomFieldService:IActionService<CustomFieldListVM>
    {
        private readonly IBasicInfoController basicInfoController;
        private readonly IPMSController pmsController;
        private readonly ICustomFieldServiceWrapper customFieldService;

        public ModifyCustomFieldService(IBasicInfoController basicInfoController
            ,IPMSController pmsController, ICustomFieldServiceWrapper customFieldService)
        {
            this.basicInfoController = basicInfoController;
            this.pmsController = pmsController;
            this.customFieldService = customFieldService;
        }


        public void DoAction(CustomFieldListVM vm)
        {

            customFieldService.GetCustomField( (res, exp) => pmsController.BeginInvokeOnDispatcher(() =>
            {
                if (exp == null)
                {
                    if (res != null)
                        basicInfoController.ShowCustomFieldView(res, ActionType.ModifyCustomField);
                    else
                        pmsController.ShowMessage("اطلاعات فیلد جهت ارسال به صفحه ویرایش معتبر نمی باشد");
                }
                else
                    pmsController.HandleException(exp);

            }), vm.SelectedCustomField.Id);
        }


    }
}

