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
    public class ClaimStateMapper : BaseMapper<ClaimState, ClaimStateDTO>, IMapper<ClaimState, ClaimStateDTO>
    {
        public override ClaimStateDTO MapToModel(ClaimState entity)
        {
            var res = new ClaimStateDTO()
                {
                    Id = int.Parse(entity.Value),
                    Name = entity.ToString(),
                };
            return res;
        }

        public override ClaimState MapToEntity(ClaimStateDTO model)
        {
            return ClaimState.GetAll<ClaimState>().Single(s => s.Value == model.Id.ToString());
        }
    }


}
