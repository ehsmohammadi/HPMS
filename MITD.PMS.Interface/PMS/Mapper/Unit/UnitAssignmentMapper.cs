using System;
using MITD.Core;
using MITD.PMS.Domain.Model.Units;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Interface
{
    public class UnitAssignmentMapper : BaseMapper<Unit, UnitInPeriodAssignmentDTO>, IMapper<Unit, UnitInPeriodAssignmentDTO>
    {

        public override UnitInPeriodAssignmentDTO MapToModel(Unit entity)
        {
            var res = new UnitInPeriodAssignmentDTO
            {
                UnitId = entity.Id.SharedUnitId.Id,
                PeriodId = entity.Id.PeriodId.Id,
            };
            return res;

        }

        public override Unit MapToEntity(UnitInPeriodAssignmentDTO model)
        {
            throw new InvalidOperationException();
        }

    }

}
