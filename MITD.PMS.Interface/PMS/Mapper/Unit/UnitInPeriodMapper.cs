using System;
using MITD.Core;
using MITD.PMS.Domain.Model.Units;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Interface
{
    public class UnitInPeriodMapper : BaseMapper<Unit, UnitInPeriodDTO>, IMapper<Unit, UnitInPeriodDTO>
    {

        public override UnitInPeriodDTO MapToModel(Unit entity)
        {
            var res = new UnitInPeriodDTO
                {

                    Name = entity.Name,
                    UnitId = entity.Id.SharedUnitId.Id,
                    
                };
            if (entity.Parent != null)
                res.ParentId = entity.Parent.Id.SharedUnitId.Id;
            return res;

        }

        public override Unit MapToEntity(UnitInPeriodDTO model)
        {
            throw new InvalidOperationException();

        }

    }

}
