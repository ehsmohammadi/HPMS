using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class PolicyRules
    {
        private long policyId;
        private string policyName;


        public long PolicyId
        {
            get { return policyId; }
            set { this.SetField(p => p.PolicyId, ref policyId, value); }
        }

        public string PolicyName
        {
            get { return policyName; }
            set { this.SetField(p => p.PolicyName, ref policyName, value); }
        }

       
    }

}
