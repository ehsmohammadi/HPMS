using System.Collections.ObjectModel;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class UnitDTO : ViewModelBase
    {
        private ObservableCollection<CustomFieldDTO> customFields;
        public ObservableCollection<CustomFieldDTO> CustomFields
        {
            get { return customFields; }
            set { this.SetField(p => p.CustomFields, ref customFields, value); }
        }
    }
}
