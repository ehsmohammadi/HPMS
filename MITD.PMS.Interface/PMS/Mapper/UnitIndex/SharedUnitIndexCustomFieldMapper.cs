using System;
using MITD.Core;
using MITD.PMS.Domain.Model.UnitIndices;
using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Domain.Model.Units;

namespace MITD.PMS.Interface.Mappers
{
    public class SharedUnitIndexCustomFieldMapper : BaseMapper<SharedUnitIndexCustomField, AbstractCustomFieldDescriptionDTO>, IMapper<SharedUnitIndexCustomField, AbstractCustomFieldDescriptionDTO>
    {
        public override AbstractCustomFieldDescriptionDTO MapToModel(SharedUnitIndexCustomField entity)
        {
            var res = new AbstractCustomFieldDescriptionDTO()
            {
                Id = entity.Id.Id,
                Name = entity.Name,
                //DictionaryName = entity.DictionaryName,
                //MaxValue = entity.MaxValue,
                //MinValue = entity.MinValue,
            };
            return res;
        }

        public override SharedUnitIndexCustomField MapToEntity(AbstractCustomFieldDescriptionDTO model)
        {
            throw new NotSupportedException("Map AbstractCustomFieldDescriptionDTO to SharedUnitCustomField not supported ");

        }
    }


 
}
