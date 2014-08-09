using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class CalculationDTOWithAction : CalculationDTO, IActionDTO
    {
        public List<int> ActionCodes { get; set; }
    }

}
