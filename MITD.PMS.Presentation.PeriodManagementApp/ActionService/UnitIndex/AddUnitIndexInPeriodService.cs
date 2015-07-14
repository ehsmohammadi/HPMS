using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public class AddUnitIndexInPeriodService : IActionService<UnitIndexInPeriodTreeVM>
    {
        private readonly IPeriodController periodController;

        public AddUnitIndexInPeriodService(IPeriodController periodController)
        {
            this.periodController = periodController;
        }


        public void DoAction(UnitIndexInPeriodTreeVM vm)
        {
            if (vm.SelectedAbstractIndexInPeriod == null)
                return;
            var unitIndexInPeriod = new UnitIndexInPeriodDTO();
            unitIndexInPeriod.PeriodId = vm.Period.Id; //vm.PeriodAbstractIndexes.PeriodId;
            unitIndexInPeriod.ParentId = vm.SelectedAbstractIndexInPeriod.Data.Id;
            
            periodController.ShowUnitIndexInPeriodView(unitIndexInPeriod, ActionType.AddUnitIndexInPeriod);
        }


    }
}

