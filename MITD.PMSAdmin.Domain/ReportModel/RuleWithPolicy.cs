using MITD.Core.RuleEngine.Model;
using MITD.PMSAdmin.Domain.Model.Policies;

namespace MITD.PMSAdminReport.Domain.Model
{
    public class RuleWithPolicyData
    {
        public RuleBase Rule { get; set; }
        public Policy Policy { get; set; }
    }

}
