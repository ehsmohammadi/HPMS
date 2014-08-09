using System;
using MITD.Core;
using MITD.Core.RuleEngine.Model;
using MITD.PMS.Presentation.Contracts;
using MITD.PMSAdminReport.Domain.Model;

namespace MITD.PMS.Interface
{
    public class RuleDTOWithPlicyMapper : BaseMapper<RuleWithPolicyData, RuleDTO>, IMapper<RuleWithPolicyData, RuleDTO>
    {

        public override RuleDTO MapToModel(RuleWithPolicyData entity)
        {
            var res = new RuleDTO
            {
                Id = entity.Rule.Id.Id,
                Name = entity.Rule.Name,
                RuleText = entity.Rule.RuleText,
                ExcuteTime = Convert.ToInt32(entity.Rule.RuleType.Value),
                PolicyId=entity.Policy.Id.Id,
                ExcuteOrder = entity.Rule.ExecuteOrder,
            };
            return res;

        }

        public override RuleWithPolicyData MapToEntity(RuleDTO model)
        {
            throw new Exception("Can not Create RuleFunction At this point");

        }

    }

}
