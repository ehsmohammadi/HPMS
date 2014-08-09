using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.Core;
using MITD.PMS.Presentation.Contracts;
using MITD.PMSAdmin.Domain.Model.CustomFieldTypes;
using MITD.PMS.Domain.Model.Jobs;

namespace MITD.PMS.Interface
{
    public class SharedJobCustomFieldMapper : BaseMapper<JobCustomField, CustomFieldDTO>, IMapper<JobCustomField, CustomFieldDTO>
    {
        public override CustomFieldDTO MapToModel(JobCustomField entity)
        {
            var res = new CustomFieldDTO()
            {
                Id = entity.Id.SharedJobCustomFieldId.Id,
                Name = entity.Name,
                DictionaryName = entity.DictionaryName,
                MaxValue = entity.MaxValue,
                MinValue = entity.MinValue,
                TypeId = entity.TypeId,
                EntityId = Convert.ToInt32(EntityTypeEnum.Job.Value)
            };
            return res;
        }

        public override JobCustomField MapToEntity(CustomFieldDTO model)
        {
            throw new NotImplementedException();
        }
    }


 
}
