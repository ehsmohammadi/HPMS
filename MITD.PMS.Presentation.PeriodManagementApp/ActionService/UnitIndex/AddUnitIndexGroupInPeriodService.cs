using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public class AddUnitIndexGroupInPeriodService : IActionService<UnitIndexInPeriodTreeVM>
    {
        private readonly IPeriodController periodController;

        public AddUnitIndexGroupInPeriodService(IPeriodController periodController)
        {
            this.periodController = periodController;
        }


        public void DoAction(UnitIndexInPeriodTreeVM vm)
        {

            var unitIndexGroupInPeriod = new UnitIndexGroupInPeriodDTO
                {
                    PeriodId = vm.Period.Id            
                };
            if (vm.SelectedAbstractIndexInPeriod == null)
                unitIndexGroupInPeriod.ParentId = null;
            else
                unitIndexGroupInPeriod.ParentId = vm.SelectedAbstractIndexInPeriod.Data.Id;


            periodController.ShowUnitIndexGroupInPeriodView(unitIndexGroupInPeriod, ActionType.AddUnitIndexGroupInPeriod);
        }


    }
}

