using MITD.Presentation;
using System.Collections.Generic;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class JobDTO 
    {
        private List<CustomFieldDTO> customFields;
        public List<CustomFieldDTO> CustomFields
        {
            get { return customFields ?? (customFields = new List<CustomFieldDTO>()); }
            set { this.SetField(p => p.CustomFields, ref customFields, value); }
        }
    }
}
