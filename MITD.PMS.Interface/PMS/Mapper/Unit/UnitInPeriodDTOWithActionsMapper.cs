using System;
using System.Collections.Generic;
using MITD.Core;
using MITD.PMS.Domain.Model.Units;
using MITD.PMS.Presentation.Contracts;
using MITD.PMSSecurity.Domain;

namespace MITD.PMS.Interface
{
    public class UnitInPeriodWithActionMapper : BaseMapper<Unit, UnitInPeriodDTOWithActions>, IMapper<Unit, UnitInPeriodDTOWithActions>
    {

        public override UnitInPeriodDTOWithActions MapToModel(Unit entity)
        {
            var res = new UnitInPeriodDTOWithActions
            {
                Name = entity.Name,
                UnitId = entity.Id.SharedUnitId.Id,
                ActionCodes = new List<int>
                {
                    (int) ActionType.AddUnitInPeriod,
                    (int) ActionType.DeleteUnitInPeriod
                }
            };
            if (entity.Parent != null)
                res.ParentId = entity.Parent.Id.SharedUnitId.Id;
            return res;

        }

        public override Unit MapToEntity(UnitInPeriodDTOWithActions model)
        {
            throw new InvalidOperationException();
        }

    }

}
