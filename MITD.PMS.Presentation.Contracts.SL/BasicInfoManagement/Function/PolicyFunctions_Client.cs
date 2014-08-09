using System.Collections.ObjectModel;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class PolicyFunctions : ViewModelBase
    {
        private ObservableCollection<FunctionDTODescriptionWithActions> functions;
        public ObservableCollection<FunctionDTODescriptionWithActions> Functions
        {
            get { return functions; }
            set { this.SetField(p => p.Functions, ref functions, value); }
        }
    }


}
