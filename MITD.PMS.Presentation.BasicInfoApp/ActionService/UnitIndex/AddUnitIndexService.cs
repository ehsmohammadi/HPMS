using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;

namespace MITD.PMS.Presentation.BasicInfoApp
{
    public class AddUnitIndexService : IActionService<UnitIndexTreeVM>
    {
        private readonly IBasicInfoController basicInfoController;

        public AddUnitIndexService(IBasicInfoController basicInfoController)
        {
            this.basicInfoController = basicInfoController;
        }


        public void DoAction(UnitIndexTreeVM vm)
        {
            var unitIndex = new UnitIndexDTO();
            if (vm.SelectedUnitIndex != null)
                unitIndex.ParentId = vm.SelectedUnitIndex.Data.Id;
            basicInfoController.ShowUnitIndexView(unitIndex, ActionType.AddUnitIndex);
        }


    }
}

