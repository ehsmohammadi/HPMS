using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class PartyDTOWithActions:PartyDTO,IActionDTO
    {
        public List<int> ActionCodes { get; set; }
    }
}
