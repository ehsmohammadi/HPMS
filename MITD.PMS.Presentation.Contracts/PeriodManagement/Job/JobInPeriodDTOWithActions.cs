using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class JobInPeriodDTOWithActions : JobInPeriodDTO,IActionDTO
    {
        public List<int> ActionCodes
        {
            get;
            set;
        }
    }
}
