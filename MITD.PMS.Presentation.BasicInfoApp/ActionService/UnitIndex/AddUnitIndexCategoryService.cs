using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;

namespace MITD.PMS.Presentation.BasicInfoApp
{ 
    public class AddUnitIndexCategoryService : IActionService<UnitIndexTreeVM>
    {
        private readonly IBasicInfoController basicInfoController;

        public AddUnitIndexCategoryService(IBasicInfoController basicInfoController)
        {
            this.basicInfoController = basicInfoController;
        }


        public void DoAction(UnitIndexTreeVM vm)
        {
            var unitIndexCategory = new UnitIndexCategoryDTO();
            if (vm.SelectedUnitIndex != null)
                unitIndexCategory.ParentId = vm.SelectedUnitIndex.Data.Id;
            else
                unitIndexCategory.ParentId = null;
            basicInfoController.ShowUnitIndexCategoryView(unitIndexCategory, ActionType.AddUnitIndexCategory);
        }
    }
}

