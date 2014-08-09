using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.Core;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Presentation.Contracts;
using MITD.PMSAdmin.Domain.Model.CustomFieldTypes;
using MITD.PMS.Domain.Model.Claims;

namespace MITD.PMS.Interface
{
    public class ClaimTypeMapper : BaseMapper<ClaimTypeEnum, ClaimTypeDTO>, IMapper<ClaimTypeEnum, ClaimTypeDTO>
    {
        public override ClaimTypeDTO MapToModel(ClaimTypeEnum entity)
        {
            var res = new ClaimTypeDTO()
                {
                    Id = int.Parse(entity.Value),
                    Name = entity.DisplayName,
                };
            return res;
        }

        public override ClaimTypeEnum MapToEntity(ClaimTypeDTO model)
        {
            return ClaimTypeEnum.GetAll<ClaimTypeEnum>().Single(s => s.Value == model.Id.ToString());
        }
    }


}
