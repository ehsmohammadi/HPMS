using System.Collections.Generic;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class PolicyDTOWithActions : PolicyDTO , IActionDTO 
    {
        
        public List<int> ActionCodes { get; set; } 
    }

}
