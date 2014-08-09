using MITD.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class UnitDTOWithActions : UnitDTO, IActionDTO 
    {
        
        public List<int> ActionCodes { get; set; }
    }

}
