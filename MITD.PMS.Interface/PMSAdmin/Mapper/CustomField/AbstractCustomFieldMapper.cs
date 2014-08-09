using System;
using System.Collections.Generic;
using MITD.Core;
using MITD.Domain.Model;
using MITD.PMS.Presentation.Contracts;
using MITD.PMSAdmin.Domain.Model.CustomFieldTypes;

namespace MITD.PMS.Interface
{
    public class AbstractCustomFieldMapper : BaseMapper<CustomFieldType, AbstractCustomFieldDTO>, IMapper<CustomFieldType, AbstractCustomFieldDTO>
    { 

        public override AbstractCustomFieldDTO MapToModel(CustomFieldType entity)
        {
            var res = new AbstractCustomFieldDTO();
            res.Id = entity.Id.Id;
            res.MaxValue = entity.MaxValue;
            res.MinValue = entity.MinValue;
            res.Name = entity.Name;
            res.DictionaryName = entity.DictionaryName;
            res.TypeName = entity.TypeId;
            res.EntityId = Convert.ToInt32(entity.EntityId.Value);
            return res;

        }

        public override CustomFieldType MapToEntity(AbstractCustomFieldDTO model)
        {
            throw new NotSupportedException("can not map AbstractCustomFieldDTO to  CustomFieldType Entity");
        }

    }
    
}
