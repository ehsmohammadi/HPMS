using MITD.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class UnitDTO : DTOBase
    {
        private List<CustomFieldDTO> customFields;
        public List<CustomFieldDTO> CustomFields
        {
            get { return customFields ?? (customFields = new List<CustomFieldDTO>()); }
            set { this.SetField(p => p.CustomFields, ref customFields, value); }
        }
    }
}
