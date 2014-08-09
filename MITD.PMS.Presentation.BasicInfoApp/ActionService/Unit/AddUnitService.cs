using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;
using MITD.Presentation;

namespace MITD.PMS.Presentation.BasicInfoApp
{
    public class AddUnitService : IActionService<UnitListVM>
    {
        private readonly IBasicInfoController basicInfoController;

        public AddUnitService(IBasicInfoController basicInfoController)
        {
            this.basicInfoController = basicInfoController;
        }


        public void DoAction(UnitListVM vm)
        {
            var unit = new UnitDTO();
            basicInfoController.ShowUnitView(unit, ActionType.AddUnit);
        }


    }
}

