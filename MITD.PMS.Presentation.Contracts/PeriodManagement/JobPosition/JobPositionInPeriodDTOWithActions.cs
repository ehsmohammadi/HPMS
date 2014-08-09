using System.Collections.Generic;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class JobPositionInPeriodDTOWithActions:JobPositionInPeriodDTO,IActionDTO
    {
        public List<int> ActionCodes { get; set; }
    }
}
