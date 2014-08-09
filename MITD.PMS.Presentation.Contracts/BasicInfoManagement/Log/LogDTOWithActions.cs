using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class LogDTOWithActions:LogDTO,IActionDTO
    {
        public List<int> ActionCodes { get; set; }
    }
}
