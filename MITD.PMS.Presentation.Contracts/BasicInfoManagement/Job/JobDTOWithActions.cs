using System.Collections.Generic;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class JobDTOWithActions : JobDTO , IActionDTO 
    {
        
        public List<int> ActionCodes { get; set; } 
    }

}
