using MITD.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class EmployeeJobPositionAssignmentDTO  : DTOBase
    {
        private List<CustomFieldValueDTO> customFieldValueList;
        public List<CustomFieldValueDTO> CustomFieldValueList
        {
            get { return customFieldValueList; }
            set { this.SetField(p => p.CustomFieldValueList, ref customFieldValueList, value); }
        }
    }
}
