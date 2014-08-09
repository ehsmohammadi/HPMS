using MITD.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class PolicyFunctions : DTOBase
    {
        private List<FunctionDTODescriptionWithActions> functions;
        public List<FunctionDTODescriptionWithActions> Functions
        {
            get { return functions; }
            set { this.SetField(p => p.Functions, ref functions, value); }
        }
    }
}
