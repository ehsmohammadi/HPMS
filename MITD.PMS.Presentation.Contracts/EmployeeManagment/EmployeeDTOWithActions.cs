using System.Collections.Generic;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class EmployeeDTOWithActions:EmployeeDTO,IActionDTO
    {
        public List<int> ActionCodes { get; set; }
    }

}
