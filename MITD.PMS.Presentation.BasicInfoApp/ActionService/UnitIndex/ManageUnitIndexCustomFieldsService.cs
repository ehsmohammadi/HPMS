using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;

namespace MITD.PMS.Presentation.BasicInfoApp
{
    public class ManageUnitIndexCustomFieldsService : IActionService<UnitIndexVM>
    {
        private readonly IBasicInfoController basicInfoController;
        private readonly IPMSController pmsController;
        private readonly IUnitIndexServiceWrapper unitIndexService;

        public ManageUnitIndexCustomFieldsService(IBasicInfoController basicInfoController
            ,IPMSController pmsController, IUnitIndexServiceWrapper unitIndexService)
        {
            this.basicInfoController = basicInfoController;
            this.pmsController = pmsController;
            this.unitIndexService = unitIndexService;
        }


        public void DoAction(UnitIndexVM vm)
        {
            if (vm.UnitIndex.Id != 0)
                basicInfoController.ShowUnitIndexCustomFieldManageView(vm.UnitIndex, ActionType.ManageUnitIndexCustomFields);
            else
                basicInfoController.ShowUnitIndexCustomFieldManageView(vm.UnitIndex, ActionType.AddUnitIndexCustomFields);
        }


    }


    
}

