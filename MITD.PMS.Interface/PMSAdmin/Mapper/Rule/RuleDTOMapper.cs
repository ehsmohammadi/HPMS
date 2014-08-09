using System;
using MITD.Core;
using MITD.Core.RuleEngine.Model;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Interface
{
    public class RuleDTOMapper : BaseMapper<RuleBase, RuleDTO>, IMapper<RuleBase, RuleDTO>
    {

        public override RuleDTO MapToModel(RuleBase entity)
        {
            var res = new RuleDTO
            {
                Id = entity.Id.Id,
                Name = entity.Name,
                RuleText = entity.RuleText,
                ExcuteTime = Convert.ToInt32(entity.RuleType.Value),
                
                ExcuteOrder = entity.ExecuteOrder
            };
            return res;

        }

        public override RuleBase MapToEntity(RuleDTO model)
        {
            throw new Exception("Can not Create RuleFunction At this point");

        }

    }

}
