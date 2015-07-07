using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.Core;
using MITD.PMS.Domain.Model.Units;
using MITD.PMS.Presentation.Contracts;
using MITD.PMSAdmin.Domain.Model.CustomFieldTypes;
using MITD.PMS.Domain.Model.Jobs;

namespace MITD.PMS.Interface
{
    public class SharedUnitCustomFieldMapper : BaseMapper<UnitCustomField, CustomFieldDTO>, IMapper<UnitCustomField, CustomFieldDTO>
    {
        public override CustomFieldDTO MapToModel(UnitCustomField entity)
        {
            var res = new CustomFieldDTO()
            {
                Id = entity.Id.SharedUnitCustomFieldId.Id,
                Name = entity.Name,
                DictionaryName = entity.DictionaryName,
                MaxValue = entity.MaxValue,
                MinValue = entity.MinValue,
                TypeId = entity.TypeId,
                EntityId = Convert.ToInt32(EntityTypeEnum.Unit.Value)
            };
            return res;
        }

        public override UnitCustomField MapToEntity(CustomFieldDTO model)
        {
            throw new NotImplementedException();
        }
    }


 
}
