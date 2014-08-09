using MITD.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class PolicyRules : DTOBase
    {
        private List<RuleDTOWithAction> rules;
        public List<RuleDTOWithAction> Rules
        {
            get { return rules; }
            set { this.SetField(p => p.Rules, ref rules, value); }
        }
    }
}
