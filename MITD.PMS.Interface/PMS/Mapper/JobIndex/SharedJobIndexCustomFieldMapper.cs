using System;
using MITD.Core;
using MITD.PMS.Domain.Model.JobIndices;
using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Domain.Model.Jobs;

namespace MITD.PMS.Interface.Mappers
{
    public class SharedJobIndexCustomFieldMapper : BaseMapper<SharedJobIndexCustomField, AbstractCustomFieldDescriptionDTO>, IMapper<SharedJobIndexCustomField, AbstractCustomFieldDescriptionDTO>
    {
        public override AbstractCustomFieldDescriptionDTO MapToModel(SharedJobIndexCustomField entity)
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

        public override SharedJobIndexCustomField MapToEntity(AbstractCustomFieldDescriptionDTO model)
        {
            throw new NotSupportedException("Map AbstractCustomFieldDescriptionDTO to SharedJobCustomField not supported ");

        }
    }


 
}
