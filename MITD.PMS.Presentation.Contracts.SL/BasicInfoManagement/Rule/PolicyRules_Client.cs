using System.Collections.ObjectModel;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class PolicyRules : ViewModelBase
    {
        private ObservableCollection<RuleDTOWithAction> rules = new ObservableCollection<RuleDTOWithAction>();
        public ObservableCollection<RuleDTOWithAction> Rules
        {
            get { return rules; }
            set { this.SetField(p => p.Rules, ref rules, value); }
        }
    }


}
