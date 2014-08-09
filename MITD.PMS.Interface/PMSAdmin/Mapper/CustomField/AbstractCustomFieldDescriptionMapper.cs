using System;
using System.Collections.Generic;
using MITD.Core;
using MITD.Domain.Model;
using MITD.PMS.Presentation.Contracts;
using MITD.PMSAdmin.Domain.Model.CustomFieldTypes;

namespace MITD.PMS.Interface
{
    public class AbstractCustomFieldDescriptionMapper : BaseMapper<CustomFieldType, AbstractCustomFieldDescriptionDTO>, IMapper<CustomFieldType, AbstractCustomFieldDescriptionDTO>
    {

        public override AbstractCustomFieldDescriptionDTO MapToModel(CustomFieldType entity)
        {
            var res = new AbstractCustomFieldDescriptionDTO();
            res.Id = entity.Id.Id;
            res.Name = entity.Name;
            return res;

        }

        public override CustomFieldType MapToEntity(AbstractCustomFieldDescriptionDTO model)
        {
            throw new NotSupportedException("can not map AbstractCustomFieldDTO to  CustomFieldType Entity");
        }

    }
    
}
