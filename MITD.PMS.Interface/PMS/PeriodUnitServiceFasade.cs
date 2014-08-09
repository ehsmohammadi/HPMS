using System.Collections.Generic;
using System.Linq;
using Castle.Core;
using MITD.Core;
using MITD.PMS.Application.Contracts;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Domain.Model.Units;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Interface
{
    //[Interceptor(typeof(Interception))]
    public class PeriodUnitServiceFacade : IPeriodUnitServiceFacade
    {
        private readonly IUnitService unitService;
        private readonly IMapper<Unit, UnitInPeriodAssignmentDTO> unitAssignmentMapper;
        private readonly IMapper<Unit, UnitInPeriodDTOWithActions> unitInPeriodDTOWithActionsMapper;
        private readonly IMapper<Unit, UnitInPeriodDTO> unitInPeriodDTOMapper;
        private readonly IUnitRepository unitRep;

        public PeriodUnitServiceFacade(IUnitService unitService,
            IMapper<Unit,UnitInPeriodAssignmentDTO> unitAssignmentMapper,
            IMapper<Unit,UnitInPeriodDTOWithActions> unitInPeriodDTOWithActionsMapper,
            IMapper<Unit, UnitInPeriodDTO> unitInPeriodDTOMapper,
            IUnitRepository unitRep)
        {
            this.unitService = unitService;
            this.unitAssignmentMapper = unitAssignmentMapper;
            this.unitInPeriodDTOWithActionsMapper = unitInPeriodDTOWithActionsMapper;
            this.unitInPeriodDTOMapper = unitInPeriodDTOMapper;
            this.unitRep = unitRep;
            
        }


        public UnitInPeriodAssignmentDTO AssignUnit(long periodId, UnitInPeriodAssignmentDTO unitInPeriod)
        {
            var unit = unitService.AssignUnit(new PeriodId(periodId), new SharedUnitId(unitInPeriod.UnitId),
                unitInPeriod.ParentUnitId != null ? new SharedUnitId(unitInPeriod.ParentUnitId.Value) : null);
            return unitAssignmentMapper.MapToModel(unit);
        }

        public string RemoveUnit(long periodId, long unitId)
        {
            unitService.RemoveUnit(new PeriodId(periodId), new SharedUnitId(unitId));
            return "Unit with Id " + unitId + " removed";
        }

        public IEnumerable<UnitInPeriodDTOWithActions> GetUnitsWithActions(long periodId)
        {
            var units=unitRep.GetUnits(new PeriodId(periodId));
            return units.Select(u => unitInPeriodDTOWithActionsMapper.MapToModel(u));
        }

        public IEnumerable<UnitInPeriodDTO> GetUnits(long periodId)
        {
            var units = unitRep.GetUnits(new PeriodId(periodId));
            return units.Select(u => unitInPeriodDTOMapper.MapToModel(u));
        }

        public UnitInPeriodDTO GetUnit(long periodId, long unitId)
        {
            var unit = unitRep.GetBy(new UnitId(new PeriodId(periodId),new SharedUnitId(unitId) ));
            return unitInPeriodDTOMapper.MapToModel(unit);
        }
    }
}
