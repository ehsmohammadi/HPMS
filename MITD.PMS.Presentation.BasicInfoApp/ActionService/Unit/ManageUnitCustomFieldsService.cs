using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;

namespace MITD.PMS.Presentation.BasicInfoApp
{
    public class ManageUnitCustomFieldsService : IActionService<UnitVM>, IActionService<UnitListVM>
    {
        private readonly IBasicInfoController basicInfoController;
        private readonly IPMSController pmsController;
        private readonly IUnitServiceWrapper unitService;

        public ManageUnitCustomFieldsService(IBasicInfoController basicInfoController
            , IPMSController pmsController, IUnitServiceWrapper unitService)
        {
            this.basicInfoController = basicInfoController;
            this.pmsController = pmsController;
            this.unitService = unitService;
        }


        public void DoAction(UnitVM vm)
        {
            if (vm.Unit.Id != 0)
                basicInfoController.ShowUnitCustomFieldManageView(vm.Unit, ActionType.ManageUnitCustomFields);
            else
                basicInfoController.ShowUnitCustomFieldManageView(vm.Unit, ActionType.AddUnitCustomFields);
        }


        public void DoAction(UnitListVM vm)
        {
            if (vm.SelectedUnit.Id != 0)
                basicInfoController.ShowUnitCustomFieldManageView(vm.SelectedUnit, ActionType.ManageUnitCustomFields);
            else
                basicInfoController.ShowUnitCustomFieldManageView(vm.SelectedUnit, ActionType.AddUnitCustomFields);
        }
    }


    
}

