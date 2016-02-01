using System;
using System.Collections.Generic;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class UnitIndexDTO : AbstractIndex
    {

        private List<CustomFieldDTO> customFields = new List<CustomFieldDTO>();
        public List<CustomFieldDTO> CustomFields
        {
            get { return customFields; }
            set { this.SetField(p => p.CustomFields, ref customFields, value); }
        }

        private Guid transferId;
        public Guid TransferId
        {
            get { return transferId; }
            set { this.SetField(p => p.TransferId, ref transferId, value); }
        }
    }
}
