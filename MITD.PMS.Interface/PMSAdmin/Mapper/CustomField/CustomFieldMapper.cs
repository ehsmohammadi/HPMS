using System;
using System.Collections.Generic;
using MITD.Core;
using MITD.Domain.Model;
using MITD.PMS.Presentation.Contracts;
using MITD.PMSAdmin.Domain.Model.CustomFieldTypes;

namespace MITD.PMS.Interface
{
    public class CustomFieldMapper : BaseMapper<CustomFieldType, CustomFieldDTO>, IMapper<CustomFieldType, CustomFieldDTO>
    { 

        public override CustomFieldDTO MapToModel(CustomFieldType entity)
        {
            var res = new CustomFieldDTO
                {
                    Id = entity.Id.Id,
                    EntityId = Convert.ToInt32(entity.EntityId.Value),
                    MaxValue = entity.MaxValue,
                    MinValue = entity.MinValue,
                    Name = entity.Name,
                    DictionaryName = entity.DictionaryName,
                    TypeId = entity.TypeId,

                };
            return res;

        }

        public override CustomFieldType MapToEntity(CustomFieldDTO model)
        {

            var res = new CustomFieldType(new CustomFieldTypeId(model.Id), model.Name, model.DictionaryName,
                                          model.MinValue, model.MaxValue, (EntityTypeEnum.FromValue<EntityTypeEnum>(model.EntityId.ToString())), model.TypeId);
            return res;

        }

    }

}
