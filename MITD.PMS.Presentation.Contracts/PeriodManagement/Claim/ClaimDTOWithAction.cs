using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class ClaimDTOWithAction:ClaimDescriptionDTO,IActionDTO 
    {
        public List<int> ActionCodes { get; set; }
    }
}
