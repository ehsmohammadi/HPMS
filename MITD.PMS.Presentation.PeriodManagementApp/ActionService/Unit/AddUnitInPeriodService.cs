using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.Logic;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public class AddUnitInPeriodService : IActionService<UnitInPeriodTreeVM>
    {
        private readonly IPeriodController periodController;

        public AddUnitInPeriodService(IPeriodController periodController)
        {
            this.periodController = periodController;
        }


        public void DoAction(UnitInPeriodTreeVM vm)
        {

            var unitInPeriod = new UnitInPeriodAssignmentDTO
            {
                PeriodId = vm.Period.Id,
                
            };
            if (vm.SelectedUnitInPeriod != null)
                unitInPeriod.ParentUnitId = vm.SelectedUnitInPeriod.Data.UnitId;
            else
                unitInPeriod.ParentUnitId = null;

            periodController.ShowUnitInPeriodView(unitInPeriod, ActionType.AddUnitInPeriod);
        }


    }
}

