using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;

namespace MITD.PMS.Presentation.BasicInfoApp
{
    public class AddCustomFieldService : IActionService<CustomFieldListVM>
    {
        private readonly IBasicInfoController basicInfoController;

        public AddCustomFieldService( IBasicInfoController basicInfoController)
        {
            this.basicInfoController = basicInfoController;
        }


        public void DoAction(CustomFieldListVM vm)
        {
            var customField = new CustomFieldDTO();
            basicInfoController.ShowCustomFieldView(customField, ActionType.CreateCustomField);
        }


    }
}

