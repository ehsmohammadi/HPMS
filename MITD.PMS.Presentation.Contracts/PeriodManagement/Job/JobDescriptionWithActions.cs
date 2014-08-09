
using System.Collections.Generic;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class JobDescriptionWithActions:JobDescription,IActionDTO
    {
        public List<int> ActionCodes
        {
            get;
            set;
        }
       
    }
}
