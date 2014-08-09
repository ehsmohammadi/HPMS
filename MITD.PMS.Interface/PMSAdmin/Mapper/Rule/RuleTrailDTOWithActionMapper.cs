using System;
using System.Collections.Generic;
using MITD.Core;
using MITD.Core.RuleEngine.Model;
using MITD.PMS.Presentation.Contracts;
using MITD.PMSSecurity.Domain;

namespace MITD.PMS.Interface
{
    public class RuleTrailDTOWithActionMapper : BaseMapper<RuleTrail, RuleTrailDTOWithAction>, IMapper<RuleTrail, RuleTrailDTOWithAction>
    {

        public override RuleTrailDTOWithAction MapToModel(RuleTrail entity)
        {
            var res = new RuleTrailDTOWithAction
            {
                Id = entity.Id.Id,
                Name = entity.Name,
                EffectiveDate =  entity.EffectiveDate,
                //PolicyId = 
                RuleId = entity.Current.Id.Id,
                ActionCodes = new List<int>
                {
                    (int) ActionType.ShowRuleTrail
                }
            };
            return res;

        }

        public override RuleTrail MapToEntity(RuleTrailDTOWithAction model)
        {
            throw new Exception("Can not Create RuleFunction At this point");

        }

    }

}
