using System;
using MITD.Core;
using MITD.Core.RuleEngine.Model;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Interface
{
    public class RuleTrailDTOMapper : BaseMapper<RuleTrail, RuleTrailDTO>, IMapper<RuleTrail, RuleTrailDTO>
    {

        public override RuleTrailDTO MapToModel(RuleTrail entity)
        {
            var res = new RuleTrailDTO
            {
                Id = entity.Id.Id,
                Name = entity.Name,
                RuleText = entity.RuleText,
                ExcuteTime = Convert.ToInt32(entity.RuleType.Value),
                ExcuteOrder = entity.ExecuteOrder,
                EffectiveDate = entity.EffectiveDate,
                RuleId = entity.Current.Id.Id,
                
            };
            return res;

        }

        public override RuleTrail MapToEntity(RuleTrailDTO model)
        {
            throw new Exception("Can not Create RuleFunction At this point");

        }

    }

}
