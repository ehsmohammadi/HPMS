using MITD.Core;
using MITD.PMS.Presentation.Contracts;
using MITD.PMSAdmin.Domain.Model.Policies;
using System;

namespace MITD.PMS.Interface
{
    public class PolicyMapper : BaseMapper<Policy, PolicyDTO>, IMapper<Policy, PolicyDTO>
    {
         
        public override PolicyDTO MapToModel(Policy entity)
        {
            var res = new PolicyDTO
                {
                    Id = entity.Id.Id,
                    Name = entity.Name,
                    DictionaryName = entity.DictionaryName
                };
            return res;

        }

        public override Policy MapToEntity(PolicyDTO model)
        {
            throw new Exception();

        }

    }

}
