using System.Collections.Generic;
using MITD.Core;
using MITD.Domain.Model;
using MITD.PMS.Presentation.Contracts;
using MITD.PMSAdmin.Domain.Model.Units;

namespace MITD.PMS.Interface
{
    public class UnitMapper : BaseMapper<Unit, UnitDTO>, IMapper<Unit, UnitDTO>
    {
         
        public override UnitDTO MapToModel(Unit entity)
        {
            var res = new UnitDTO
                {
                    Id = entity.Id.Id,
                    Name = entity.Name,
                    DictionaryName = entity.DictionaryName
                };
            return res;

        }

        public override Unit MapToEntity(UnitDTO model)
        {
            var res = new Unit(new UnitId(model.Id), model.Name, model.DictionaryName);
            return res;

        }

    }

}
