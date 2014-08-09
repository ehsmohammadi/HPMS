using System;
using System.Collections.Generic;
using MITD.Core;
using MITD.Core.RuleEngine.Model;
using MITD.PMS.Presentation.Contracts;
using MITD.PMSSecurity.Domain;

namespace MITD.PMS.Interface
{
    public class RuleDTOWithActionMapper : BaseMapper<RuleBase, RuleDTOWithAction>, IMapper<RuleBase, RuleDTOWithAction>
    {

        public override RuleDTOWithAction MapToModel(RuleBase entity)
        {
            var res = new RuleDTOWithAction
            {
                Id = entity.Id.Id,
                Name = entity.Name,
                ActionCodes = new List<int>
                {
                    (int) ActionType.AddRule,
                    (int) ActionType.ModifyRule,
                    (int) ActionType.DeleteRule,
                    (int) ActionType.ShowAllRuleTrails,
                }
            };
            return res;

        }

        public override RuleBase MapToEntity(RuleDTOWithAction model)
        {
            throw new Exception("Can not Create RuleFunction At this point");

        }

    }

}
