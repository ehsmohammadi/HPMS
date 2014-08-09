using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public interface IActionDTO
    {
         List<int> ActionCodes { get; set; }
    }
}
