using System.Collections.Generic;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class JobPositionInPeriodDTOWithActions:JobPositionInPeriodDTO,IActionDTO
    {
        public List<int> ActionCodes { get; set; }
       

    }
}
