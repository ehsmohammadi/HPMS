
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Presentation.Logic
{
    public class UpdateRuleTrailListArgs
    {
        public UpdateRuleTrailListArgs(RuleDTO rule)
        {
            RuleDto = rule;
        }

        public RuleDTO RuleDto
        {
            get; private set;
        }

    }
}