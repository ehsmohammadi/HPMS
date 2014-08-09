using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class RuleContentDTO
    {
        private string ruleContent;
        public string RuleContent
        {
            get { return ruleContent; }
            set { this.SetField(p => p.RuleContent, ref ruleContent, value); }
        }
    }
}
